using Microsoft.AspNetCore.Mvc;

namespace Mojo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly long _maxFileSize = 5 * 1024 * 1024; // 5 MB
        //private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

        public FileController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("upload-logo")]
        public async Task<IActionResult> UploadLogo(IFormFile file)
        {
            try
            {
                // Validation : fichier présent
                if (file == null || file.Length == 0)
                {
                    return BadRequest(new { message = "Aucun fichier fourni" });
                }

                // Validation : taille
                if (file.Length > _maxFileSize)
                {
                    return BadRequest(new { message = "Le fichier est trop volumineux (max 5 MB)" });
                }

                //Validation : extension
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                //if (!_allowedExtensions.Contains(extension))
                //{
                //    return BadRequest(new { message = "Format de fichier non autorisé (jpg, jpeg, png, gif uniquement)" });
                //}

                // Génération nom unique
                var fileName = $"{Guid.NewGuid()}{extension}";
                var uploadPath = Path.Combine(_environment.WebRootPath, "uploads", "logos");

                // Créer le dossier s'il n'existe pas
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var filePath = Path.Combine(uploadPath, fileName);

                // Sauvegarde du fichier
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Retourner l'URL relative
                var fileUrl = $"/uploads/logos/{fileName}";

                return Ok(new { url = fileUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur lors de l'upload", error = ex.Message });
            }
        }
    }
}