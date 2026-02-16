using Mojo.Application.DTOs.EntitiesDto.MoisAmortissement;

namespace Mojo.Application.Features.MoisAmortissements.Request.Query
{
    public class GetMoisAmortissementDetailsRequest : IRequest<MoisAmortissementDto>
    {
        public int Id { get; set; }
    }
}
