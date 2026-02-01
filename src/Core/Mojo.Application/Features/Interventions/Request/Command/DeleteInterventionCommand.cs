namespace Mojo.Application.Features.Interventions.Request.Command
{
    public class DeleteInterventionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
