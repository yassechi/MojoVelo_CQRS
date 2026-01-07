using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mojo.Domain.Entities
{
    public class Message : BaseEntity<Message>
    {
        public string Contenu { get; set; } = null!;
        public DateTime DateEnvoi { get; set; }

        [ForeignKey(nameof(Discussion))]
        public int DiscussionId { get; set; }
        public Discussion Discussion { get; set; }
    }
}