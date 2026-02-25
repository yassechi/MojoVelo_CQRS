namespace Mojo.Application.Features.OrganisationLogos.Request.Command
{
    public class DeleteOrganisationLogoCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
