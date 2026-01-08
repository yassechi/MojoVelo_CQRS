namespace Mojo.Application.Features.Interventions.Request.Command
{
    internal class UpdateInterventionCommand : IRequest<Unit>
    {
        public InterventionDto dto { get; set; }
    }
}
