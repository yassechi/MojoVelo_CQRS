using Microsoft.Extensions.Logging;
using Mojo.Application.DTOs.EntitiesDto.Demande;
using Mojo.Application.DTOs.EntitiesDto.Demande.Validators;
using Mojo.Application.DTOs.EntitiesDto.Discussion;
using Mojo.Application.DTOs.EntitiesDto.Discussion.Validators;
using Mojo.Application.DTOs.EntitiesDto.Message;
using Mojo.Application.DTOs.EntitiesDto.Message.Validators;
using Mojo.Application.DTOs.EntitiesDto.Velo;
using Mojo.Application.DTOs.EntitiesDto.Velo.Validators;
using Mojo.Application.Features.Demandes.Request.Command;
using Mojo.Application.Persistance.Contracts;
using Mojo.Application.Reponses;
using Mojo.Domain.Entities;
using Mojo.Domain.Enums;

namespace Mojo.Application.Features.Demandes.Handlers.Command
{
    public class CreateDemandeWithBikeHandler : IRequestHandler<CreateDemandeWithBikeCommand, CreateDemandeWithBikeResponse>
    {
        private const string DefaultMojoId = "c5e095fa-2ce3-4e18-8d23-e66c6be1818c";

        private readonly IVeloRepository _veloRepository;
        private readonly IDemandeRepository _demandeRepository;
        private readonly IDiscussionRepository _discussionRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDemandeWithBikeHandler> _logger;

        public CreateDemandeWithBikeHandler(
            IVeloRepository veloRepository,
            IDemandeRepository demandeRepository,
            IDiscussionRepository discussionRepository,
            IMessageRepository messageRepository,
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<CreateDemandeWithBikeHandler> logger)
        {
            _veloRepository = veloRepository;
            _demandeRepository = demandeRepository;
            _discussionRepository = discussionRepository;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateDemandeWithBikeResponse> Handle(CreateDemandeWithBikeCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateDemandeWithBikeResponse();

            if (request?.dto == null)
            {
                response.Success = false;
                response.Message = "Requete invalide.";
                response.Errors.Add("Le contenu de la demande est manquant.");
                return response;
            }

            var dto = request.dto;

            if (string.IsNullOrWhiteSpace(dto.IdUser))
            {
                response.Success = false;
                response.Message = "Echec de la creation de la demande.";
                response.Errors.Add("L'utilisateur est requis.");
                return response;
            }

            var user = await _userRepository.GetUserByStringId(dto.IdUser);
            if (user == null || !user.IsActif)
            {
                response.Success = false;
                response.Message = "Echec de la creation de la demande.";
                response.Errors.Add($"L'utilisateur avec l'Id '{dto.IdUser}' n'existe pas ou est inactif.");
                return response;
            }

            var mojoId = string.IsNullOrWhiteSpace(dto.MojoId) ? DefaultMojoId : dto.MojoId;
            var mojoUser = await _userRepository.GetUserByStringId(mojoId);
            if (mojoUser == null || !mojoUser.IsActif)
            {
                response.Success = false;
                response.Message = "Echec de la creation de la demande.";
                response.Errors.Add($"L'utilisateur Mojo avec l'Id '{mojoId}' n'existe pas ou est inactif.");
                return response;
            }

            if (dto.Bike == null)
            {
                response.Success = false;
                response.Message = "Echec de la creation de la demande.";
                response.Errors.Add("Les informations du velo sont requises.");
                return response;
            }

            if (dto.Bike.CmsId <= 0)
            {
                response.Success = false;
                response.Message = "Echec de la creation de la demande.";
                response.Errors.Add("L'identifiant du velo est invalide.");
                return response;
            }

            if (string.IsNullOrWhiteSpace(dto.Bike.Marque) || string.IsNullOrWhiteSpace(dto.Bike.Modele))
            {
                response.Success = false;
                response.Message = "Echec de la creation de la demande.";
                response.Errors.Add("La marque et le modele du velo sont requis.");
                return response;
            }

            if (dto.Bike.PrixAchat <= 0)
            {
                response.Success = false;
                response.Message = "Echec de la creation de la demande.";
                response.Errors.Add("Le prix d'achat du velo doit etre superieur a 0.");
                return response;
            }

            var veloDto = new VeloDto
            {
                NumeroSerie = $"CMS-{dto.Bike.CmsId}",
                Marque = dto.Bike.Marque.Trim(),
                Modele = dto.Bike.Modele.Trim(),
                Type = string.IsNullOrWhiteSpace(dto.Bike.Type) ? null : dto.Bike.Type.Trim(),
                PrixAchat = dto.Bike.PrixAchat,
                Status = true,
            };

            var veloValidator = new VeloValidator(_veloRepository);
            var veloValidation = await veloValidator.ValidateAsync(veloDto, options =>
            {
                options.IncludeRuleSets("Create");
            }, cancellationToken);

            if (!veloValidation.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la creation du velo : erreurs de validation.";
                response.Errors = veloValidation.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var velo = _mapper.Map<Velo>(veloDto);
            await _veloRepository.CreateAsync(velo);

            var now = DateTime.Now;
            var discussionDto = new DiscussionDto
            {
                Objet = $"Demande velo - {dto.Bike.Modele.Trim()}",
                Status = true,
                DateCreation = now,
                ClientId = dto.IdUser,
                MojoId = mojoId,
                CreatedBy = dto.IdUser,
                ModifiedBy = dto.IdUser,
                IsActif = true,
            };

            var discussionValidator = new DiscussionValidator(_userRepository);
            var discussionValidation = await discussionValidator.ValidateAsync(discussionDto, options =>
            {
                options.IncludeRuleSets("Create");
            }, cancellationToken);

            if (!discussionValidation.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la creation de la discussion : erreurs de validation.";
                response.Errors = discussionValidation.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var discussion = _mapper.Map<Mojo.Domain.Entities.Discussion>(discussionDto);
            await _discussionRepository.CreateAsync(discussion);

            var messageDto = new MessageDto
            {
                Contenu = $"Bienvenue On va parler du velo \"{dto.Bike.Modele.Trim()}\"",
                DateEnvoi = now,
                UserId = mojoId,
                DiscussionId = discussion.Id,
                CreatedBy = mojoId,
                ModifiedBy = mojoId,
                IsActif = true,
            };

            var messageValidator = new MessageValidator(_userRepository, _discussionRepository);
            var messageValidation = await messageValidator.ValidateAsync(messageDto, options =>
            {
                options.IncludeRuleSets("Create");
            }, cancellationToken);

            if (!messageValidation.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la creation du message : erreurs de validation.";
                response.Errors = messageValidation.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var message = _mapper.Map<Message>(messageDto);
            await _messageRepository.CreateAsync(message);

            var demandeDto = new DemandeDto
            {
                Status = DemandeStatus.Encours,
                IdUser = dto.IdUser,
                IdVelo = velo.Id,
                DiscussionId = discussion.Id,
            };

            var demandeValidator = new DemandeValidator(_userRepository, _veloRepository, _discussionRepository);
            var demandeValidation = await demandeValidator.ValidateAsync(demandeDto, options =>
            {
                options.IncludeRuleSets("Create");
            }, cancellationToken);

            if (!demandeValidation.IsValid)
            {
                response.Success = false;
                response.Message = "Echec de la creation de la demande : erreurs de validation.";
                response.Errors = demandeValidation.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

            var demande = new Demande
            {
                Status = DemandeStatus.Encours,
                IdUser = dto.IdUser,
                IdVelo = velo.Id,
                DiscussionId = discussion.Id,
                IsActif = true,
            };

            await _demandeRepository.CreateAsync(demande);

            response.Success = true;
            response.Message = "Demande creee avec succes.";
            response.Id = demande.Id;
            response.DemandeId = demande.Id;
            response.VeloId = velo.Id;
            response.DiscussionId = discussion.Id;
            response.MessageId = message.Id;
            return response;
        }
    }
}
