using Mojo.Application.DTOs.Common;

namespace Mojo.Application.DTOs.EntitiesDto.Velo
{
    public class VeloDto : BaseDto<int>
    {
        public string NumeroSerie { get; set; } = null!;
        public string Marque { get; set; } = null!;
        public string Modele { get; set; } = null!;
        public decimal PrixAchat { get; set; }
        public bool Status { get; set; }
    }
}