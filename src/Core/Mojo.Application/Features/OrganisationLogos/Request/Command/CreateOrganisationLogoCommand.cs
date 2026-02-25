using Mojo.Application.DTOs.EntitiesDto.OrganisationLogo;

namespace Mojo.Application.Features.OrganisationLogos.Request.Command
{
    public class CreateOrganisationLogoCommand : IRequest<BaseResponse>
    {
        public OrganisationLogoDto dto { get; set; } = null!;
    }
}
