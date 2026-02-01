using System.ComponentModel.DataAnnotations.Schema;

namespace Mojo.Application.DTOs.EntitiesDto.Contrat
{
    public class ContratDto : BaseEntity<int>
    {
        [Column(TypeName = "date")]
        public DateOnly DateDebut { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly DateFin { get; set; }
        public decimal LoyerMensuelHT { get; set; }
        public bool StatutContrat { get; set; }
        public int VeloId { get; set; }
        public string BeneficiaireId { get; set; } = null!;  // ? CHANGÉ EN STRING
        public string UserRhId { get; set; } = null!;        // ? CHANGÉ EN STRING
    }
}