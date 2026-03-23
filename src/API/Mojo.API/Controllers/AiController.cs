using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mojo.API.Attributes;
using Mojo.Domain.Enums;
using Mojo.Infrastructure.AI;
using System.IO;
using System.Security.Claims;

namespace Mojo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AiController : ControllerBase
{
    private readonly IRagService _ragService;

    public AiController(IRagService ragService)
        => _ragService = ragService;

    private string? GetUserId()
        => User.FindFirstValue(ClaimTypes.NameIdentifier);

    // ── Admin uniquement ──────────────────────────────────────────────────────

    [HttpPost("admin/ask")]
    [AuthorizeRole(UserRole.Admin)]
    public async Task<IActionResult> AdminAsk([FromBody] AskRequest request)
    {
        var response = await _ragService.AdminRAGAsync(request.Question, GetUserId());
        return Ok(new { response });
    }

    [HttpPost("admin/upload")]
    [AuthorizeRole(UserRole.Admin)]
    public async Task<IActionResult> AdminUpload(IFormFile file)
    {
        try
        {
            await _ragService.UploadAdminPdfAsync(file);
            return Ok(new { message = $"'{file.FileName}' uploadé et indexé !" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("admin/upload/multiple")]
    [AuthorizeRole(UserRole.Admin)]
    public async Task<IActionResult> AdminUploadMultiple(List<IFormFile> files)
    {
        var uploaded = new List<string>();
        var errors = new List<string>();

        foreach (var file in files)
        {
            try
            {
                await _ragService.UploadAdminPdfAsync(file);
                uploaded.Add(file.FileName);
            }
            catch (InvalidOperationException ex)
            {
                errors.Add($"{file.FileName}: {ex.Message}");
            }
        }
        return Ok(new { uploades = uploaded, erreurs = errors });
    }

    // ── Client docs gérés par admin ──────────────────────────────────────────

    [HttpPost("client/upload")]
    [AuthorizeRole(UserRole.Admin)]
    public async Task<IActionResult> ClientUpload(IFormFile file)
    {
        try
        {
            await _ragService.UploadClientPdfAsync(file);
            return Ok(new { message = $"'{file.FileName}' uploadé et indexé !" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("client/upload/multiple")]
    [AuthorizeRole(UserRole.Admin)]
    public async Task<IActionResult> ClientUploadMultiple(List<IFormFile> files)
    {
        var uploaded = new List<string>();
        var errors = new List<string>();

        foreach (var file in files)
        {
            try
            {
                await _ragService.UploadClientPdfAsync(file);
                uploaded.Add(file.FileName);
            }
            catch (InvalidOperationException ex)
            {
                errors.Add($"{file.FileName}: {ex.Message}");
            }
        }
        return Ok(new { uploades = uploaded, erreurs = errors });
    }

    [HttpGet("client/files")]
    [AuthorizeRole(UserRole.Admin)]
    public async Task<IActionResult> GetClientFiles()
    {
        var files = await _ragService.ListClientPdfsAsync();
        return Ok(files);
    }

    [HttpDelete("client/files/{fileName}")]
    [AuthorizeRole(UserRole.Admin)]
    public async Task<IActionResult> DeleteClientFile(string fileName)
    {
        try
        {
            await _ragService.DeleteClientPdfAsync(fileName);
            return Ok(new { message = $"'{fileName}' supprimé." });
        }
        catch (FileNotFoundException)
        {
            return NotFound(new { error = "Fichier introuvable." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("admin/files")]
    [AuthorizeRole(UserRole.Admin)]
    public async Task<IActionResult> GetAdminFiles()
    {
        var files = await _ragService.ListAdminPdfsAsync();
        return Ok(files);
    }

    [HttpDelete("admin/files/{fileName}")]
    [AuthorizeRole(UserRole.Admin)]
    public async Task<IActionResult> DeleteAdminFile(string fileName)
    {
        try
        {
            await _ragService.DeleteAdminPdfAsync(fileName);
            return Ok(new { message = $"'{fileName}' supprimé." });
        }
        catch (FileNotFoundException)
        {
            return NotFound(new { error = "Fichier introuvable." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("admin/evaluate")]
    [AuthorizeRole(UserRole.Admin)]
    public async Task<IActionResult> AdminEvaluate([FromBody] AskRequest request)
    {
        var evaluation = await _ragService.EvaluateAsync(
            request.Question, RagCollections.ADMIN);
        return Ok(new { evaluation });
    }

    [HttpPost("admin/sync")]
    [AuthorizeRole(UserRole.Admin)]
    public async Task<IActionResult> AdminSync()
    {
        await _ragService.SyncAdminVectorStoreAsync();
        return Ok("Synchronisation admin terminée !");
    }

    [HttpGet("admin/logs")]
    [AuthorizeRole(UserRole.Admin)]
    public async Task<IActionResult> GetAdminLogs()
    {
        var logs = await _ragService.GetLogsAsync(RagCollections.ADMIN);
        return Ok(logs);
    }

    [HttpGet("logs")]
    [AuthorizeRole(UserRole.Admin)]
    public async Task<IActionResult> GetAllLogs()
    {
        var logs = await _ragService.GetLogsAsync();
        return Ok(logs);
    }

    // ── Client (public) peut poser une question ──────────────────────────────

    [HttpPost("client/ask")]
    [AllowAnonymous]
    public async Task<IActionResult> ClientAsk([FromBody] AskRequest request)
    {
        var response = await _ragService.ClientRAGAsync(request.Question, GetUserId());
        return Ok(new { response });
    }
}

public record AskRequest(string Question);
