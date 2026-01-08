using Mojo.Application.DTOs.EntitiesDto.Organisation;

namespace Mojo.Application.Features.Organisations.Request.Command
{
    public class CreateOrganisationCommand : IRequest<BaseResponse>
    {
        public OrganisationDto dto { get; set; }
    }
}
