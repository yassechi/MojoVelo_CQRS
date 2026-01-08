namespace Mojo.Application.Features.Amortissments.Request.Command
{
    internal class UpdateAmortissementCommand : IRequest<Unit>
    {
        public AmortissmentDto dto { get; set; }
    }
}
