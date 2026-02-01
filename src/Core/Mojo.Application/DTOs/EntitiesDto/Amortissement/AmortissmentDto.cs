using System.ComponentModel.DataAnnotations.Schema;

namespace Mojo.Application.DTOs.EntitiesDto.Amortissement
{
    //public class AmortissmentAddDto : BaseEntity<int>
    public class AmortissmentDto : BaseEntity<int>
    {
        public DateOnly DateDebut { get; set; }
        public decimal ValeurInit { get; set; }
        public int DureeMois { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValeurResiduelleFinale { get; set; }
        public int VeloId { get; set; }

    }

    //public class AmortissmentUpdateDto : BaseEntity<int>
    //{
    //    public DateOnly DateDebut { get; set; }
    //    public decimal ValeurInit { get; set; }
    //    public int DureeMois { get; set; }
    //    [Column(TypeName = "decimal(10,2)")]
    //    public decimal ValeurResiduelleFinale { get; set; }
    //    public int VeloId { get; set; }
    //}
}