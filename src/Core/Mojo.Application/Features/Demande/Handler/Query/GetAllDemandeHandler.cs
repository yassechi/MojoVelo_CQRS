using MediatR;
using Microsoft.Extensions.Logging;
using Mojo.Application.DTOs.EntitiesDto.Demande;
using Mojo.Application.Features.Demandes.Request.Query;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Demandes.Handlers.Query
{
    public class GetAllDemandeRequestHandler : IRequestHandler<GetAllDemandeRequest, List<DemandeDto>>
    {
        private readonly IGenericRepository<Domain.Entities.Demande> _demandeRepository;
        private readonly ILogger<GetAllDemandeRequestHandler> _logger;

        public GetAllDemandeRequestHandler(
            IGenericRepository<Domain.Entities.Demande> demandeRepository,
            ILogger<GetAllDemandeRequestHandler> logger)
        {
            _demandeRepository = demandeRepository;
            _logger = logger;
        }

        public async Task<List<DemandeDto>> Handle(GetAllDemandeRequest request, CancellationToken cancellationToken)
        {
            var demandes = await _demandeRepository.GetAllAsync();

            var demandesActives = demandes.Where(d => d.IsActif).ToList();

            return demandesActives.Select(d => new DemandeDto
            {
                Id = d.Id,
                Status = d.Status,
                IdUser = d.IdUser,
                IdVelo = d.IdVelo,
                DiscussionId = d.DiscussionId
            }).ToList();
        }
    }
}