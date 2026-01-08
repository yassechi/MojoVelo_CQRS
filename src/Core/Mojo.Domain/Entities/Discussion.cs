using System.ComponentModel.DataAnnotations.Schema;

namespace Mojo.Domain.Entities
{
    public class Discussion : BaseEntity<Discussion>
    {
        public string Objet { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime DateCreation { get; set; }

        [ForeignKey(nameof(User))]
        public string ClientId { get; set; } = null!;
        public User Client { get; set; } = null!;

        [ForeignKey(nameof(User))]
        public string MojoId { get; set; } = null!;
        public User Mojo { get; set; }

        public List<Message> Messages = [];
    }
}