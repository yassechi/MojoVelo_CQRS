#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0070
#pragma warning disable SKEXP0020
#pragma warning disable SKEXP0050

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Text;
using Mojo.Domain.AI;
using Mojo.Persistence.DatabaseContext;
using UglyToad.PdfPig;

namespace Mojo.Infrastructure.AI;

public class RagService : IRagService
{
    private readonly VectorStore _vectorStore;
    private readonly IEmbeddingGenerator<string, Embedding<float>> _embeddingService;
    private readonly IChatCompletionService _chatService;
    private readonly IChatCompletionService _jugeService;
    private readonly IMemoryCache _cache;
    private readonly MDbContext _dbContext;
    private readonly string _adminPdfsDir;
    private readonly string _clientPdfsDir;
    private readonly long _maxFileSizeBytes;
    private readonly int _cacheDurationHours;

    private const string PROMPT_ADMIN = """
        Tu es un assistant analytique pour l'administration de MojoVelo.
        Réponds uniquement en te basant sur les données fournies dans le contexte.
        Si la réponse n'est pas trouvée, réponds : JE NE SAIS PAS
        <contexte>{context}</contexte>
        <question>{question}</question>
        """;

    private const string PROMPT_CLIENT = """
        Tu es un assistant sympathique pour les clients de MojoVelo.
        Réponds uniquement en te basant sur les informations du contexte.
        Si la réponse n'est pas trouvée, réponds : Je ne dispose pas de cette information.
        <contexte>{context}</contexte>
        <question>{question}</question>
        """;

    private const string PROMPT_JUGE = """
        Évaluez cette réponse IA (score 1 à 5).
        1 = Hors contexte | 3 = Partiellement | 5 = Entièrement basée sur le contexte
        ###Question : {question}
        ###Contexte : {context}
        ###Réponse : {answer}
        Score et justification :
        """;

    public RagService(
        VectorStore vectorStore,
        IEmbeddingGenerator<string, Embedding<float>> embeddingService,
        IChatCompletionService chatService,
        IChatCompletionService jugeService,
        IMemoryCache cache,
        MDbContext dbContext,
        IConfiguration configuration)
    {
        _vectorStore = vectorStore;
        _embeddingService = embeddingService;
        _chatService = chatService;
        _jugeService = jugeService;
        _cache = cache;
        _dbContext = dbContext;
        _adminPdfsDir = configuration["AI:AdminPdfsDir"] ?? "./pdfs/admin";
        _clientPdfsDir = configuration["AI:ClientPdfsDir"] ?? "./pdfs/client";
        _maxFileSizeBytes = configuration.GetValue<long>("AI:MaxFileSizeMB", 10) * 1024 * 1024;
        _cacheDurationHours = configuration.GetValue<int>("AI:CacheDurationHours", 1);
    }

    // ── Synchronisation ──────────────────────────────────────────────────────

    public Task SyncAdminVectorStoreAsync() => SyncVectorStoreAsync(_adminPdfsDir, RagCollections.ADMIN);
    public Task SyncClientVectorStoreAsync() => SyncVectorStoreAsync(_clientPdfsDir, RagCollections.CLIENT);

    private async Task SyncVectorStoreAsync(string pdfsDir, string collectionName)
    {
        Directory.CreateDirectory(pdfsDir);
        var collection = _vectorStore.GetCollection<string, DocumentChunk>(collectionName);
        await collection.EnsureCollectionExistsAsync();

        var pdfFiles = Directory.GetFiles(pdfsDir, "*.pdf");
        foreach (var pdfPath in pdfFiles)
        {
            var fileName = Path.GetFileName(pdfPath);
            var firstChunkId = $"{fileName}_chunk_0";

            var existing = await collection.GetAsync(firstChunkId);
            if (existing != null)
            {
                Console.WriteLine($"[{collectionName}] Déjà indexé : {fileName}");
                continue;
            }

            var text = LirePdf(pdfPath);
            var chunks = TextChunker.SplitPlainTextLines(text, 500);

            foreach (var (chunk, i) in chunks.Select((c, i) => (c, i)))
            {
                var embeddings = await _embeddingService.GenerateAsync(new List<string> { chunk });
                await collection.UpsertAsync(new DocumentChunk
                {
                    Id = $"{fileName}_chunk_{i}",
                    Text = chunk,
                    SourceFile = fileName,
                    Collection = collectionName,
                    Embedding = embeddings[0].Vector
                });
            }
            Console.WriteLine($"[{collectionName}] Indexé : {fileName} ({chunks.Count} chunks)");
        }
    }

    // ── RAG ──────────────────────────────────────────────────────────────────

    public Task<string> AdminRAGAsync(string query, string? userId = null)
        => RAGAsync(query, RagCollections.ADMIN, PROMPT_ADMIN, userId);

    public Task<string> ClientRAGAsync(string query, string? userId = null)
        => RAGAsync(query, RagCollections.CLIENT, PROMPT_CLIENT, userId);

    private async Task<string> RAGAsync(
        string query, string collectionName, string promptTemplate, string? userId = null)
    {
        var cacheKey = $"rag_{collectionName}_{query.GetHashCode()}";
        if (_cache.TryGetValue(cacheKey, out string? cached)) return cached!;

        var sw = System.Diagnostics.Stopwatch.StartNew();
        var queryEmbeddings = await _embeddingService.GenerateAsync(new List<string> { query });

        var collection = _vectorStore.GetCollection<string, DocumentChunk>(collectionName);
        var searchResults = collection.SearchAsync(queryEmbeddings[0].Vector, top: 5);

        var contextList = new List<string>();
        await foreach (var result in searchResults)
            contextList.Add(result.Record.Text);

        var context = string.Join(". ", contextList);
        var prompt = promptTemplate
            .Replace("{context}", context)
            .Replace("{question}", query);

        var response = await _chatService.GetChatMessageContentAsync(prompt);
        var responseText = response.Content ?? "JE NE SAIS PAS";
        sw.Stop();

        _cache.Set(cacheKey, responseText, TimeSpan.FromHours(_cacheDurationHours));

        await _dbContext.AiLogs.AddAsync(new AiLog
        {
            Collection = collectionName,
            Question = query,
            Reponse = responseText,
            DureeMs = sw.ElapsedMilliseconds,
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        });
        await _dbContext.SaveChangesAsync();

        return responseText;
    }

    // ── Évaluation ───────────────────────────────────────────────────────────

    public async Task<string> EvaluateAsync(string question, string collectionName)
    {
        var queryEmbeddings = await _embeddingService.GenerateAsync(new List<string> { question });
        var collection = _vectorStore.GetCollection<string, DocumentChunk>(collectionName);
        var searchResults = collection.SearchAsync(queryEmbeddings[0].Vector, top: 5);

        var contextList = new List<string>();
        await foreach (var result in searchResults)
            contextList.Add(result.Record.Text);

        var context = string.Join(". ", contextList);
        var answer = collectionName == RagCollections.ADMIN
            ? await AdminRAGAsync(question)
            : await ClientRAGAsync(question);

        var prompt = PROMPT_JUGE
            .Replace("{question}", question)
            .Replace("{context}", context)
            .Replace("{answer}", answer);

        var jugeResponse = await _jugeService.GetChatMessageContentAsync(prompt);
        return jugeResponse.Content ?? string.Empty;
    }

    // ── Logs ─────────────────────────────────────────────────────────────────

    public async Task<List<AiLog>> GetLogsAsync(string? collection = null)
    {
        var query = _dbContext.AiLogs.AsQueryable();
        if (collection != null)
            query = query.Where(l => l.Collection == collection);
        return await query.OrderByDescending(l => l.CreatedAt).ToListAsync();
    }

    // ── Upload ───────────────────────────────────────────────────────────────

    public Task UploadAdminPdfAsync(IFormFile file) => UploadPdfAsync(file, _adminPdfsDir, RagCollections.ADMIN);
    public Task UploadClientPdfAsync(IFormFile file) => UploadPdfAsync(file, _clientPdfsDir, RagCollections.CLIENT);

    private async Task UploadPdfAsync(IFormFile file, string pdfsDir, string collectionName)
    {
        if (file.Length > _maxFileSizeBytes)
            throw new InvalidOperationException(
                $"Fichier trop volumineux. Maximum : {_maxFileSizeBytes / 1024 / 1024} MB");

        if (!file.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Seuls les fichiers PDF sont acceptés.");

        Directory.CreateDirectory(pdfsDir);
        var filePath = Path.Combine(pdfsDir, file.FileName);
        using (var stream = File.Create(filePath))
            await file.CopyToAsync(stream);

        await SyncVectorStoreAsync(pdfsDir, collectionName);
    }

    // ── Gestion des PDFs ─────────────────────────────────────────────────────

    public Task<List<RagPdfInfo>> ListAdminPdfsAsync() => ListPdfsAsync(_adminPdfsDir);
    public Task<List<RagPdfInfo>> ListClientPdfsAsync() => ListPdfsAsync(_clientPdfsDir);

    public Task DeleteAdminPdfAsync(string fileName)
        => DeletePdfAsync(fileName, _adminPdfsDir, RagCollections.ADMIN);

    public Task DeleteClientPdfAsync(string fileName)
        => DeletePdfAsync(fileName, _clientPdfsDir, RagCollections.CLIENT);

    private static Task<List<RagPdfInfo>> ListPdfsAsync(string pdfsDir)
    {
        if (!Directory.Exists(pdfsDir))
            return Task.FromResult(new List<RagPdfInfo>());

        var files = Directory.GetFiles(pdfsDir, "*.pdf")
            .Select(path => new FileInfo(path))
            .OrderByDescending(info => info.LastWriteTimeUtc)
            .Select(info => new RagPdfInfo
            {
                FileName = info.Name,
                SizeBytes = info.Length,
                LastModifiedUtc = info.LastWriteTimeUtc
            })
            .ToList();

        return Task.FromResult(files);
    }

    private async Task DeletePdfAsync(string fileName, string pdfsDir, string collectionName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new InvalidOperationException("Nom de fichier invalide.");

        Directory.CreateDirectory(pdfsDir);
        var safeName = Path.GetFileName(fileName);
        var filePath = Path.Combine(pdfsDir, safeName);

        if (!File.Exists(filePath))
            throw new FileNotFoundException("Fichier introuvable.", safeName);

        File.Delete(filePath);

        // ✅ Resynchronise sans le fichier supprimé
        await SyncVectorStoreAsync(pdfsDir, collectionName);
    }

    // ── Helper ───────────────────────────────────────────────────────────────

    private static string LirePdf(string path)
    {
        using var pdf = PdfDocument.Open(path);
        return string.Join(" ", pdf.GetPages()
            .SelectMany(p => p.GetWords())
            .Select(w => w.Text));
    }
}