namespace Mojo.Application.DTOs.EntitiesDto.Message.Validators
{
    public class MessageValidator : AbstractValidator<MessageDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDiscussionRepository _discussionRepository;

        public MessageValidator(IUserRepository userRepository, IDiscussionRepository discussionRepository)
        {
            _userRepository = userRepository;
            _discussionRepository = discussionRepository;

            RuleFor(m => m.UserId)
                .NotEmpty()
                .WithMessage("{PropertyName} est requis.")
                .MustAsync(async (userId, cancellationToken) =>
                {
                    return await _userRepository.UserExists(userId);
                })
                .WithMessage("L'utilisateur avec l'Id {PropertyValue} n'existe pas.");

            RuleFor(m => m.DiscussionId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} doit être supérieur à 0.")
                .MustAsync(async (discussionId, cancellationToken) =>
                {
                    var discussion = await _discussionRepository.GetByIdAsync(discussionId);
                    return discussion != null;
                })
                .WithMessage("La discussion avec l'Id {PropertyValue} n'existe pas.");

            RuleFor(m => m.Contenu)
                .NotEmpty()
                .WithMessage("Le contenu du message ne peut pas être vide.")
                .MaximumLength(2000)
                .WithMessage("Le contenu ne doit pas dépasser 2000 caractères.");

            RuleFor(m => m.DateEnvoi)
                .NotEmpty()
                .WithMessage("La date d'envoi est obligatoire.")
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("La date d'envoi ne peut pas être dans le futur.");

            RuleSet("Create", () =>
            {
            });

            RuleSet("Update", () =>
            {
                RuleFor(m => m.Id)
                    .GreaterThan(0)
                    .WithMessage("L'ID du message est requis pour la mise à jour.");
            });
        }
    }
}