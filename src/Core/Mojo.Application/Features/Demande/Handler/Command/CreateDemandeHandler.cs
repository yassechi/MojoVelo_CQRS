using MediatR;
using Microsoft.Extensions.Logging;
using Mojo.Application.Features.Demandes.Request.Command;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Demandes.Handlers.Command
{
    public class CreateDemandeCommandHandler : IRequestHandler<CreateDemandeCommand, BaseResponse>
    {
        private readonly IGenericRepository<Demande> _demandeRepository;
        private readonly ILogger<CreateDemandeCommandHandler> _logger;

        public CreateDemandeCommandHandler(
            IGenericRepository<Demande> demandeRepository,
            ILogger<CreateDemandeCommandHandler> logger)
        {
            _demandeRepository = demandeRepository;
            _logger = logger;
        }

        public async Task<BaseResponse> Handle(CreateDemandeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var demande = new Demande
            {
                Status = request.dto.Status,
                IdUser = request.dto.IdUser,
                IdVelo = request.dto.IdVelo,
                DiscussionId = request.dto.DiscussionId
            };

            await _demandeRepository.CreateAsync(demande);

            response.Success = true;
            response.Message = "Demande créée avec succès";

            return response;
        }
    }
}