namespace Mojo.Application.Features.Organisations.Request.Command
{
    public class DeleteOrganisationCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
