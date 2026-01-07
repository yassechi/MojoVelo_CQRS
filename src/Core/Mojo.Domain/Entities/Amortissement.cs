using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Mojo.Domain.Entities

{
    public class Amortissement : BaseEntity<Amortissement>
    {
        public DateOnly DateDebut { get; set; }
        public decimal ValeurInit { get; set; }
        public int DureeMois { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValeurResiduelleFinale { get; set; }
        [ForeignKey(nameof(Velo))]
        public int VeloId { get; set; }
        public Velo Velo { get; set; } 
    }
}