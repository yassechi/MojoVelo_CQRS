using System.ComponentModel.DataAnnotations.Schema;

namespace Mojo.Application.DTOs.EntitiesDto.Amortissement
{
    public class AmortissmentDto : BaseEntity<int>
    {
        public DateOnly DateDebut { get; set; }
        public decimal ValeurInit { get; set; }
        public int DureeMois { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValeurResiduelleFinale { get; set; }
        public int VeloId { get; set; }

    }
}