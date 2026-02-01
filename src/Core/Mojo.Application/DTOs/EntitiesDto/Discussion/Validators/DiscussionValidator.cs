namespace Mojo.Application.DTOs.EntitiesDto.Discussion.Validators
{
    public class DiscussionValidator : AbstractValidator<DiscussionDto>
    {
        private readonly IUserRepository _userRepository;

        public DiscussionValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(d => d.MojoId)
                .NotEmpty()
                .WithMessage("{PropertyName} est requis.")
                .MustAsync(async (mojoId, cancellationToken) =>
                {
                    return await _userRepository.UserExists(mojoId);
                })
                .WithMessage("L'utilisateur Mojo avec l'Id {PropertyValue} n'existe pas.");

            RuleFor(d => d.ClientId)
                .NotEmpty()
                .WithMessage("{PropertyName} est requis.")
                .MustAsync(async (clientId, cancellationToken) =>
                {
                    return await _userRepository.UserExists(clientId);
                })
                .WithMessage("Le client avec l'Id {PropertyValue} n'existe pas.");

            RuleFor(d => d.Objet)
                .NotEmpty()
                .WithMessage("L'objet de la discussion est obligatoire.")
                .MaximumLength(100)
                .WithMessage("L'objet ne doit pas dépasser 100 caractères.");

            RuleFor(d => d.DateCreation)
                .NotEmpty()
                .WithMessage("La date de création est obligatoire.")
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("La date de création ne peut pas être dans le futur.");

            RuleSet("Create", () =>
            {
            });

            RuleSet("Update", () =>
            {
                RuleFor(d => d.Id)
                    .GreaterThan(0)
                    .WithMessage("L'ID de la discussion est requis pour la mise à jour.");
            });
        }
    }
}