namespace Mojo.Application.Features.Contrats.Request.Command
{
    public class DeleteContratCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
