namespace Mojo.Application.Features.Organisations.Request.Command
{
    internal class UpdateOrganisationCommand : IRequest<Unit>
    {
        public OrganisationDto dto { get; set; }
    }
}
