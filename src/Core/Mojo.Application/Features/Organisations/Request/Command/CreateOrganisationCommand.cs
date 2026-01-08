using Mojo.Application.DTOs.EntitiesDto.Organisation;

namespace Mojo.Application.Features.Organisations.Request.Command
{
    public class CreateOrganisationCommand : IRequest<Unit>
    {
        public OrganisationDto dto { get; set; }
    }
}
