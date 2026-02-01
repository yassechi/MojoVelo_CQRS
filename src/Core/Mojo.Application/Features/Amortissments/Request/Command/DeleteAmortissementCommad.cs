namespace Mojo.Application.Features.Amortissments.Request.Command
{
    public class DeleteAmortissementCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
