using MediatR;
using Mojo.Application.DTOs.EntitiesDto.Demande;

namespace Mojo.Application.Features.Demandes.Request.Command
{
    public class UpdateDemandeCommand : IRequest<BaseResponse>
    {
        public DemandeDto dto { get; set; } = null!;
    }
}