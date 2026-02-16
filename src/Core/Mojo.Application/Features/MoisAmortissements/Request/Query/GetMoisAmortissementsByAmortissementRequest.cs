using Mojo.Application.DTOs.EntitiesDto.MoisAmortissement;

namespace Mojo.Application.Features.MoisAmortissements.Request.Query
{
    public class GetMoisAmortissementsByAmortissementRequest : IRequest<List<MoisAmortissementDto>>
    {
        public int AmortissementId { get; set; }
    }
}
