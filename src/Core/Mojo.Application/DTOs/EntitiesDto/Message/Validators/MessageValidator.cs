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
                .NotEmpty().WithMessage("L'identifiant de l'utilisateur est obligatoire.")
                .MustAsync(async (id, token) => await _userRepository.UserExists(id))
                .WithMessage("L'utilisateur spécifié n'existe pas en base de données.");

            RuleFor(m => m.DiscussionId)
                .GreaterThan(0).WithMessage("L'identifiant de la discussion doit être supérieur à 0.")
                .MustAsync(async (id, token) => await _discussionRepository.Exists(id))
                .WithMessage("La discussion spécifiée n'existe pas.");

            RuleFor(m => m.Contenu)
                .NotEmpty().WithMessage("Le contenu du message ne peut pas être vide.")
                .MaximumLength(2000).WithMessage("Le contenu ne doit pas dépasser 2000 caractères.");

            RuleFor(m => m.DateEnvoi)
                .NotEmpty().WithMessage("La date d'envoi est obligatoire.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La date d'envoi ne peut pas être dans le futur.");
        }
    }
}