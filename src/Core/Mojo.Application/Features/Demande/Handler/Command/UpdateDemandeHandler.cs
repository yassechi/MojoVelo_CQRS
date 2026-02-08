using MediatR;
using Microsoft.Extensions.Logging;
using Mojo.Application.DTOs.EntitiesDto.Demande.Validators;
using Mojo.Application.Features.Demandes.Request.Command;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Demandes.Handlers.Command
{
    public class UpdateDemandeCommandHandler : IRequestHandler<UpdateDemandeCommand, BaseResponse>
    {
        private readonly IGenericRepository<Demande> _demandeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVeloRepository _veloRepository;
        private readonly IDiscussionRepository _discussionRepository;
        private readonly ILogger<UpdateDemandeCommandHandler> _logger;

        public UpdateDemandeCommandHandler(
            IGenericRepository<Demande> demandeRepository,
            IUserRepository userRepository,
            IVeloRepository veloRepository,
            IDiscussionRepository discussionRepository,
            ILogger<UpdateDemandeCommandHandler> logger)
        {
            _demandeRepository = demandeRepository;
            _userRepository = userRepository;
            _veloRepository = veloRepository;
            _discussionRepository = discussionRepository;
            _logger = logger;
        }

        public async Task<BaseResponse> Handle(UpdateDemandeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var validator = new DemandeValidator(_userRepository, _veloRepository, _discussionRepository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Update");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la modification de la demande : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var demande = await _demandeRepository.GetByIdAsync(request.dto.Id);

            if (demande == null)
            {
                response.Success = false;
                response.Message = "Echec de la modification de la demande.";
                response.Errors.Add($"Aucune demande trouvée avec l'Id {request.dto.Id}.");
                return response;
            }

            demande.Status = request.dto.Status;
            demande.IdUser = request.dto.IdUser;
            demande.IdVelo = request.dto.IdVelo;
            demande.DiscussionId = request.dto.DiscussionId;

            await _demandeRepository.UpdateAsync(demande);

            response.Success = true;
            response.Message = "La demande a été modifiée avec succès.";
            response.Id = demande.Id;
            return response;
        }
    }
}