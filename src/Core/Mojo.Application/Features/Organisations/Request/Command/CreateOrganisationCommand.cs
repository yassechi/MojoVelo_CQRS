namespace Mojo.Application.Features.Organisations.Request.Command
{
    internal class CreateOrganisationCommand : IRequest<Unit>
    {
        public OrganisationDto dto { get; set; }
    }
}
