using Microsoft.AspNetCore.Http;
using Mojo.Domain.Entities;
using Mojo.Domain.AI;

namespace Mojo.Infrastructure.AI
{
    public interface IRagService
    {
        Task SyncAdminVectorStoreAsync();
        Task SyncClientVectorStoreAsync();
        Task<string> AdminRAGAsync(string query, string? userId = null);
        Task<string> ClientRAGAsync(string query, string? userId = null);
        Task UploadAdminPdfAsync(IFormFile file);
        Task UploadClientPdfAsync(IFormFile file);
        Task<List<RagPdfInfo>> ListAdminPdfsAsync();
        Task<List<RagPdfInfo>> ListClientPdfsAsync();
        Task DeleteAdminPdfAsync(string fileName);
        Task DeleteClientPdfAsync(string fileName);
        Task<string> EvaluateAsync(string question, string collection);
        Task<List<AiLog>> GetLogsAsync(string? collection = null);
    }
}