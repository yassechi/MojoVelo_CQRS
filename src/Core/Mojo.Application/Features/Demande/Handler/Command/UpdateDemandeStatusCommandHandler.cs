using MediatR;
using Microsoft.Extensions.Logging;
using Mojo.Application.Features.Demandes.Request.Command;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Demandes.Handlers.Command
{
    public class UpdateDemandeStatusCommandHandler : IRequestHandler<UpdateDemandeStatusCommand, BaseResponse>
    {
        private readonly IGenericRepository<Demande> _demandeRepository;
        private readonly ILogger<UpdateDemandeStatusCommandHandler> _logger;

        public UpdateDemandeStatusCommandHandler(
            IGenericRepository<Demande> demandeRepository,
            ILogger<UpdateDemandeStatusCommandHandler> logger)
        {
            _demandeRepository = demandeRepository;
            _logger = logger;
        }

        public async Task<BaseResponse> Handle(UpdateDemandeStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var demande = await _demandeRepository.GetByIdAsync(request.Id);

            if (demande == null)
            {
                response.Success = false;
                response.Message = "Demande non trouvée";
                return response;
            }

            demande.Status = request.Status;

            await _demandeRepository.UpdateAsync(demande);

            response.Success = true;
            response.Message = "Statut de la demande mis à jour avec succès";

            return response;
        }
    }
}