namespace Mojo.Application.Features.Velos.Request.Command
{
    internal class CreateVeloCommand : IRequest<Unit>
    {
        public VeloDto dto { get; set; }
    }
}
