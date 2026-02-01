namespace Mojo.Application.Features.Velos.Request.Command
{
    public class DeleteVeloCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
