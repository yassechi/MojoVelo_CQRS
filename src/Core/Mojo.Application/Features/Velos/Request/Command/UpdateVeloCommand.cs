namespace Mojo.Application.Features.Velos.Request.Command
{
    internal class UpdateVeloCommand : IRequest<Unit>
    {
        public VeloDto dto { get; set; }
    }
}
