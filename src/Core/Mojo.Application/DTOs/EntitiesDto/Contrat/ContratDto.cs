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

        // CHANGEZ INT EN STRING
        public string BeneficiaireId { get; set; } = null!;
        public string UserRhId { get; set; } = null!;
    }
}