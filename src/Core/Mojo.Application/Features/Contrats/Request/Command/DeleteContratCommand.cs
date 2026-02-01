namespace Mojo.Application.Features.Contrats.Request.Command
{
    public class DeleteContratCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
