using Mojo.Application.DTOs.EntitiesDto.Organisation;

namespace Mojo.Application.Features.Organisations.Request.Command
{
    public class UpdateOrganisationCommand : IRequest<BaseResponse>
    {
        public OrganisationDto dto { get; set; }
    }
}
