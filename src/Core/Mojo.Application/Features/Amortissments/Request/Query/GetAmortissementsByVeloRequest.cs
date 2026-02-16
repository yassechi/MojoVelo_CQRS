using Mojo.Application.DTOs.EntitiesDto.Amortissement;

namespace Mojo.Application.Features.Amortissements.Request.Query
{
    public class GetAmortissementsByVeloRequest : IRequest<List<AmortissmentDto>>
    {
        public int VeloId { get; set; }
    }
}
