using Microsoft.AspNetCore.Identity; // C'est le SEUL namespace Identity nécessaire
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mojo.Domain.Entities
{
    // L'héritage de IdentityUser est maintenant correct pour .NET 9
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = null!;

        // Correction de la faute de frappe : LasttName -> LastName
        public string LastName { get; set; } = null!;

        public int Role { get; set; }
        public float TailleCm { get; set; }
        public bool IsActif { get; set; }

        [ForeignKey(nameof(Organisation))]
        public int OrganisationId { get; set; }
        public Organisation? Organisation { get; set; }

        public virtual List<Contrat> ContratsRecus { get; set; } = new();
        public virtual List<Contrat> ContratsGeres { get; set; } = new();

        // Ajout de { get; set; } pour que Entity Framework puisse mapper la liste
        public virtual List<Discussion> Discussions { get; set; } = new();
    }
}