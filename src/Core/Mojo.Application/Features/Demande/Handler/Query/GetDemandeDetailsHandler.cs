using MediatR;
using Microsoft.Extensions.Logging;
using Mojo.Application.DTOs.EntitiesDto.Demande;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.Demandes.Request.Query;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.Features.Demandes.Handlers.Query
{
    public class GetDemandeDetailsRequestHandler : IRequestHandler<GetDemandeDetailsRequest, DemandeDto>
    {
        private readonly IGenericRepository<Domain.Entities.Demande> _demandeRepository;
        private readonly ILogger<GetDemandeDetailsRequestHandler> _logger;

        public GetDemandeDetailsRequestHandler(
            IGenericRepository<Domain.Entities.Demande> demandeRepository,
            ILogger<GetDemandeDetailsRequestHandler> logger)
        {
            _demandeRepository = demandeRepository;
            _logger = logger;
        }

        public async Task<DemandeDto> Handle(GetDemandeDetailsRequest request, CancellationToken cancellationToken)
        {
            var demande = await _demandeRepository.GetByIdAsync(request.Id);

            if (demande == null || !demande.IsActif)
            {
                throw new NotFoundException(nameof(Domain.Entities.Demande), request.Id);
            }

            return new DemandeDto
            {
                Id = demande.Id,
                Status = demande.Status,
                IdUser = demande.IdUser,
                IdVelo = demande.IdVelo,
                DiscussionId = demande.DiscussionId
            };
        }
    }
}