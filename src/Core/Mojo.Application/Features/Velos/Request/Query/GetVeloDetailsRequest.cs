using Mojo.Application.DTOs.EntitiesDto.Velo;

namespace Mojo.Application.Features.Velos.Request.Query
{
    public class GetVeloDetailsRequest : IRequest<VeloDto>
    {
        public int Id { get; set; }
    }
}
