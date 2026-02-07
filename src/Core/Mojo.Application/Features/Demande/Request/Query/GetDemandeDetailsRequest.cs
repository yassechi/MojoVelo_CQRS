using MediatR;
using Mojo.Application.DTOs.EntitiesDto.Demande;

namespace Mojo.Application.Features.Demandes.Request.Query
{
    public class GetDemandeDetailsRequest : IRequest<DemandeDto>
    {
        public int Id { get; set; }
    }
}