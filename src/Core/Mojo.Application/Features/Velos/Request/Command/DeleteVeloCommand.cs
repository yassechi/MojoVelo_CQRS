namespace Mojo.Application.Features.Velos.Request.Command
{
    internal class DeleteVeloCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
