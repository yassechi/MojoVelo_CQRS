using System.ComponentModel.DataAnnotations.Schema;

namespace Mojo.Domain.Entities
{
    public class Discussion : BaseEntity<int> // Assure-toi que BaseEntity utilise <int> ou <Guid>
    {
        public string Objet { get; set; } = null!;
        public bool Status { get; set; }
        public DateTime DateCreation { get; set; }

        public string ClientId { get; set; } = null!;
        [ForeignKey(nameof(ClientId))]
        public virtual User Client { get; set; } = null!;

        public string MojoId { get; set; } = null!;
        [ForeignKey(nameof(MojoId))]
        public virtual User Mojo { get; set; } = null!;

        public virtual List<Message> Messages { get; set; } = new();
    }
}