namespace Mojo.Application.Features.Velos.Request.Command
{
    public class DeleteVeloCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
