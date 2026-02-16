namespace Mojo.Application.Features.MoisAmortissements.Request.Command
{
    public class DeleteMoisAmortissementCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
