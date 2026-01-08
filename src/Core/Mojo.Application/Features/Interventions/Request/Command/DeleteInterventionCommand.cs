namespace Mojo.Application.Features.Interventions.Request.Command
{
    internal class DeleteInterventionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
