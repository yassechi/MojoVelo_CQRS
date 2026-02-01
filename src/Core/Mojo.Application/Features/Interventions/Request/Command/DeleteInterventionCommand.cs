namespace Mojo.Application.Features.Interventions.Request.Command
{
    public class DeleteInterventionCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
