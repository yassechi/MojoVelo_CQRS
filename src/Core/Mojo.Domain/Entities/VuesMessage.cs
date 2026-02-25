using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mojo.Domain.Entities
{
    public class VuesMessage : BaseEntity<VuesMessage>
    {
        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        [ForeignKey(nameof(Message))]
        public int MessageId { get; set; }
        public Message Message { get; set; } = null!;

        public DateTime ViewedAt { get; set; }
    }
}
