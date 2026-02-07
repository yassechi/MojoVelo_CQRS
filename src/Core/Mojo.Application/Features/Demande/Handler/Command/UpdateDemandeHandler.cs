using MediatR;
using Microsoft.Extensions.Logging;
using Mojo.Application.Features.Demandes.Request.Command;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Demandes.Handlers.Command
{
    public class UpdateDemandeCommandHandler : IRequestHandler<UpdateDemandeCommand, BaseResponse>
    {
        private readonly IGenericRepository<Demande> _demandeRepository;
        private readonly ILogger<UpdateDemandeCommandHandler> _logger;

        public UpdateDemandeCommandHandler(
            IGenericRepository<Demande> demandeRepository,
            ILogger<UpdateDemandeCommandHandler> logger)
        {
            _demandeRepository = demandeRepository;
            _logger = logger;
        }

        public async Task<BaseResponse> Handle(UpdateDemandeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var demande = await _demandeRepository.GetByIdAsync(request.dto.Id);

            if (demande == null)
            {
                response.Success = false;
                response.Message = "Demande non trouvée";
                return response;
            }

            demande.Status = request.dto.Status;
            demande.IdUser = request.dto.IdUser;
            demande.IdVelo = request.dto.IdVelo;
            demande.DiscussionId = request.dto.DiscussionId;

            await _demandeRepository.UpdateAsync(demande);

            response.Success = true;
            response.Message = "Demande mise à jour avec succès";

            return response;
        }
    }
}