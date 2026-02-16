using Mojo.Application.DTOs.EntitiesDto.User;
using Mojo.Application.DTOs.EntitiesDto.Velo;

namespace Mojo.Application.DTOs.EntitiesDto.Contrat
{
    public class ContratEditDataDto
    {
        public ContratDto Contrat { get; set; } = new();
        public List<AdminUserListItemDto> Users { get; set; } = new();
        public List<VeloDto> Velos { get; set; } = new();
    }
}
