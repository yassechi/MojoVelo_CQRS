using Mojo.Application.DTOs.Common;
using Mojo.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mojo.Application.DTOs.EntitiesDto.Contrat
{
    public class ContratDto : BaseDto<int>  // ? Doit ï¿½tre BaseDto, pas BaseEntity
    {
        [Column(TypeName = "date")]
        public DateOnly DateDebut { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public DateOnly DateFin { get; set; }

        public decimal LoyerMensuelHT { get; set; }

        public decimal? MontantAmortissementMensuel { get; set; }

        public StatutContrat StatutContrat { get; set; }  // ? Doit être StatutContrat, pas bool

        public int Duree { get; set; }  // ? Ce champ doit ï¿½tre prï¿½sent

        public int VeloId { get; set; }

        public string Ref { get; set; } = string.Empty;  // ? Ce champ doit ï¿½tre prï¿½sent

        public string BeneficiaireId { get; set; } = null!;

        public string UserRhId { get; set; } = null!;
    }
}