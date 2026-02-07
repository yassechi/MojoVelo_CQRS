using Mojo.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mojo.Domain.Entities
{
    public class Demande : BaseEntity<Demande>  
    {

        public DemandeStatus Status { get; set; }

        [Required]
        public string IdUser { get; set; } = null!;
        [ForeignKey(nameof(IdUser))]
        public virtual User User { get; set; } = null!;

        [Required]
        public int IdVelo { get; set; }
        [ForeignKey(nameof(IdVelo))]
        public virtual Velo Velo { get; set; } = null!;

        public int DiscussionId { get; set; }
        [ForeignKey(nameof(DiscussionId))]
        public virtual Discussion? Discussion { get; set; }
    }
}