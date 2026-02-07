using MediatR;
using Microsoft.Extensions.Logging;
using Mojo.Application.Features.Demandes.Request.Command;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Demandes.Handlers.Command
{
    public class DeleteDemandeCommandHandler : IRequestHandler<DeleteDemandeCommand, BaseResponse>
    {
        private readonly IGenericRepository<Demande> _demandeRepository;
        private readonly ILogger<DeleteDemandeCommandHandler> _logger;

        public DeleteDemandeCommandHandler(
            IGenericRepository<Demande> demandeRepository,
            ILogger<DeleteDemandeCommandHandler> logger)
        {
            _demandeRepository = demandeRepository;
            _logger = logger;
        }

        public async Task<BaseResponse> Handle(DeleteDemandeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var demande = await _demandeRepository.GetByIdAsync(request.Id);

            if (demande == null)
            {
                response.Success = false;
                response.Message = "Demande non trouvée";
                return response;
            }

            await _demandeRepository.DeleteAsync(demande.Id);

            response.Success = true;
            response.Message = "Demande supprimée avec succès";

            return response;
        }
    }
}