using MediatR;
using Microsoft.Extensions.Logging;
using Mojo.Application.DTOs.EntitiesDto.Demande.Validators;
using Mojo.Application.Features.Demandes.Request.Command;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Demandes.Handlers.Command
{
    public class CreateDemandeCommandHandler : IRequestHandler<CreateDemandeCommand, BaseResponse>
    {
        private readonly IGenericRepository<Demande> _demandeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVeloRepository _veloRepository;
        private readonly IDiscussionRepository _discussionRepository;
        private readonly ILogger<CreateDemandeCommandHandler> _logger;

        public CreateDemandeCommandHandler(
            IGenericRepository<Demande> demandeRepository,
            IUserRepository userRepository,
            IVeloRepository veloRepository,
            IDiscussionRepository discussionRepository,
            ILogger<CreateDemandeCommandHandler> logger)
        {
            _demandeRepository = demandeRepository;
            _userRepository = userRepository;
            _veloRepository = veloRepository;
            _discussionRepository = discussionRepository;
            _logger = logger;
        }

        public async Task<BaseResponse> Handle(CreateDemandeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var validator = new DemandeValidator(_userRepository, _veloRepository, _discussionRepository);
            var validationResult = await validator.ValidateAsync(request.dto, options =>
            {
                options.IncludeRuleSets("Create");
            }, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la création de la demande : erreurs de validation.";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var demande = new Demande
            {
                Status = Domain.Enums.DemandeStatus.Encours,
                IdUser = request.dto.IdUser,
                IdVelo = request.dto.IdVelo,
                DiscussionId = request.dto.DiscussionId,
                IsActif = true
            };

            await _demandeRepository.CreateAsync(demande);

            response.Success = true;
            response.Message = "Demande créée avec succès";
            response.Id = demande.Id;
            return response;
        }
    }
}