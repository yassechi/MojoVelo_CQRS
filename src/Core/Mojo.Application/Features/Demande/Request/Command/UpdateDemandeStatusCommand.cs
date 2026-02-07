using MediatR;
using Mojo.Domain.Enums;

namespace Mojo.Application.Features.Demandes.Request.Command
{
    public class UpdateDemandeStatusCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
        public DemandeStatus Status { get; set; }
    }
}