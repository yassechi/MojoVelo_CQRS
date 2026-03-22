using System.ComponentModel.DataAnnotations;

// ✅ CORRECTION : Déplacé dans Mojo.Domain.AI pour cohérence
// (RagService et MDbContext utilisent using Mojo.Domain.AI)
namespace Mojo.Domain.AI
{
    public class AiLog
    {
        [Key]
        public int Id { get; set; }

        public string Collection { get; set; } = string.Empty;

        public string Question { get; set; } = string.Empty;

        public string Reponse { get; set; } = string.Empty;

        public long DureeMs { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string UserId { get; set; }
    }
}