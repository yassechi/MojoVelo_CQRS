using System.ComponentModel.DataAnnotations.Schema;

namespace Mojo.Domain.Entities
{
    public class MoisAmortissement : BaseEntity<MoisAmortissement>
    {
        [ForeignKey(nameof(Amortissement))]
        public int AmortissementId { get; set; }
        public Amortissement Amortissement { get; set; } = null!;

        public int NumeroMois { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Montant { get; set; }
    }
}
