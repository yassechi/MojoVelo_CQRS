namespace Mojo.Application.Features.Amortissments.Request.Command
{
    public class DeleteAmortissementCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
