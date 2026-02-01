using Mojo.Application.DTOs.EntitiesDto.Amortissement;

namespace Mojo.Application.Features.Amortissments.Request.Query
{
    public class GetAmortissementDetailsRequest : IRequest<AmortissmentDto>
    {
        public int Id { get; set; }
    }
}
 