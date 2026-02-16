using Mojo.Application.DTOs.EntitiesDto.Demande;
using Mojo.Application.Exceptions;
using Mojo.Application.Features.Demandes.Request.Query;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Entities;

namespace Mojo.Application.Features.Demandes.Handlers.Query
{
    public class GetDemandeDetailViewHandler : IRequestHandler<GetDemandeDetailViewRequest, DemandeDetailDto>
    {
        private readonly IDemandeRepository _demandeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IVeloRepository _veloRepository;
        private readonly IDiscussionRepository _discussionRepository;
        private readonly IMessageRepository _messageRepository;

        public GetDemandeDetailViewHandler(
            IDemandeRepository demandeRepository,
            IUserRepository userRepository,
            IVeloRepository veloRepository,
            IDiscussionRepository discussionRepository,
            IMessageRepository messageRepository)
        {
            _demandeRepository = demandeRepository;
            _userRepository = userRepository;
            _veloRepository = veloRepository;
            _discussionRepository = discussionRepository;
            _messageRepository = messageRepository;
        }

        public async Task<DemandeDetailDto> Handle(GetDemandeDetailViewRequest request, CancellationToken cancellationToken)
        {
            var demande = await _demandeRepository.GetByIdAsync(request.Id);

            if (demande == null || !demande.IsActif)
            {
                throw new NotFoundException(nameof(Demande), request.Id);
            }

            var user = await _userRepository.GetUserByStringId(demande.IdUser);
            var velo = await _veloRepository.GetByIdAsync(demande.IdVelo);
            var discussion = demande.DiscussionId > 0
                ? await _discussionRepository.GetByIdAsync(demande.DiscussionId)
                : null;

            var messages = demande.DiscussionId > 0
                ? await _messageRepository.GetByDiscussionId(demande.DiscussionId)
                : new List<Message>();

            var userIds = messages
                .Select(message => NormalizeId(message.CreatedBy ?? message.ModifiedBy))
                .Where(id => !string.IsNullOrWhiteSpace(id))
                .Distinct()
                .ToList();

            var usersById = new Dictionary<string, User>();
            foreach (var id in userIds)
            {
                var dbUser = await _userRepository.GetUserByStringId(id);
                if (dbUser != null)
                {
                    usersById[NormalizeId(dbUser.Id)] = dbUser;
                }
            }

            var clientId = discussion?.ClientId;
            var mojoId = discussion?.MojoId;

            var messageDtos = messages.Select(message =>
            {
                var messageUserId = NormalizeId(message.CreatedBy ?? message.ModifiedBy);
                usersById.TryGetValue(messageUserId, out var author);

                return new DemandeMessageDto
                {
                    Id = message.Id,
                    Contenu = message.Contenu ?? string.Empty,
                    DateEnvoi = message.DateEnvoi,
                    CreatedDate = message.CreatedDate,
                    UserId = messageUserId,
                    RoleLabel = ResolveRoleLabel(messageUserId, author?.Role, clientId, mojoId)
                };
            }).ToList();

            var userName = user == null ? demande.IdUser : $"{user.FirstName} {user.LastName}".Trim();

            return new DemandeDetailDto
            {
                Id = demande.Id,
                Status = demande.Status,
                IdUser = demande.IdUser,
                UserName = userName,
                UserEmail = user?.Email ?? string.Empty,
                IdVelo = demande.IdVelo,
                VeloMarque = velo?.Marque ?? string.Empty,
                VeloModele = velo?.Modele ?? string.Empty,
                VeloType = velo?.Type,
                VeloPrixAchat = velo?.PrixAchat,
                DiscussionId = demande.DiscussionId,
                VeloCmsId = TryGetCmsId(velo?.NumeroSerie),
                Messages = messageDtos
            };
        }

        private static string NormalizeId(string? value)
        {
            return (value ?? string.Empty).Trim().Replace("{", string.Empty).Replace("}", string.Empty).ToLowerInvariant();
        }

        private static string ResolveRoleLabel(string messageUserId, int? role, string? clientId, string? mojoId)
        {
            var normalizedMessageUserId = NormalizeId(messageUserId);
            if (!string.IsNullOrWhiteSpace(clientId) && NormalizeId(clientId) == normalizedMessageUserId)
            {
                return "Client";
            }
            if (!string.IsNullOrWhiteSpace(mojoId) && NormalizeId(mojoId) == normalizedMessageUserId)
            {
                return "Mojo";
            }
            return role switch
            {
                1 => "Mojo",
                2 => "Manager",
                3 => "Client",
                _ => "Utilisateur"
            };
        }

        private static int? TryGetCmsId(string? numeroSerie)
        {
            if (string.IsNullOrWhiteSpace(numeroSerie))
            {
                return null;
            }
            if (!numeroSerie.StartsWith("CMS-"))
            {
                return null;
            }
            var raw = numeroSerie.Replace("CMS-", string.Empty);
            return int.TryParse(raw, out var id) ? id : null;
        }
    }
}
