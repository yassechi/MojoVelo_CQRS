using Mojo.Application.DTOs.EntitiesDto.MoisAmortissement;

namespace Mojo.Application.Features.MoisAmortissements.Request.Query
{
    public class GetMoisAmortissementsByContratRequest : IRequest<List<MoisAmortissementDto>>
    {
        public int ContratId { get; set; }
    }
}
