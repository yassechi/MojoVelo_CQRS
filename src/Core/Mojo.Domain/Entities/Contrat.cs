using Mojo.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Mojo.Domain.Entities

{
    public class Contrat : BaseEntity<Contrat>
    {
        [Column(TypeName = "date")]
        public DateOnly DateDebut { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly DateFin { get; set; }
        public decimal LoyerMensuelHT { get; set; }
        public StatutContrat StatutContrat { get; set; }
        public int Duree { get; set; }
        [ForeignKey(nameof(Velo))]
        public int VeloId { get; set; }
        public Velo Velo { get; set; }
        public string Ref { get; set; }
        public string BeneficiaireId { get; set; } = null!;
        public virtual User Beneficiaire { get; set; } = null!;
        public string UserRhId { get; set; } = null!;
        public virtual User UserRH { get; set; } = null!;
        public virtual Documents Documents { get; set; } 
    }
}