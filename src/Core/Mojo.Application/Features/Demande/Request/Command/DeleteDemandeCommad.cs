using MediatR;

namespace Mojo.Application.Features.Demandes.Request.Command
{
    public class DeleteDemandeCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}