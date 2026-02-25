using System.ComponentModel.DataAnnotations.Schema;

namespace Mojo.Domain.Entities
{
    public class OrganisationLogo : BaseEntity<OrganisationLogo>
    {
        [ForeignKey(nameof(Organisation))]
        public int OrganisationId { get; set; }
        public Organisation Organisation { get; set; } = null!;

        public byte[] Fichier { get; set; } = null!;

        public string NomFichier { get; set; } = string.Empty;

        public string TypeFichier { get; set; } = string.Empty;
    }
}
