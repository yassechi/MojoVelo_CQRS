using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Mojo.Domain.Entities
{
    public class User : IdentityUser
    {
        [Key]
        public string FirstName { get; set; } = null!;
        public string LasttName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Role { get; set; }
        public float TailleCm { get; set; }
        public bool IsActif { get; set; }

        [ForeignKey(nameof(Organisation))]
        public int OrganisationId { get; set; }
        public Organisation? Organisation { get; set; }
        
        public virtual List<Contrat> ContratsRecus { get; set; } = new();
        public virtual List<Contrat> ContratsGeres { get; set; } = new();
        public List<Discussion> Discussions = [];
    }
}