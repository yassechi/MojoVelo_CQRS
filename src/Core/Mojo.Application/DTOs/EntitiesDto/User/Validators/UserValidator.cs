using FluentValidation;
using Mojo.Application.DTOs.EntitiesDto.User;
using Mojo.Application.Persistance.Contracts;

namespace Mojo.Application.DTOs.EntitiesDto.User.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        private readonly IOrganisationRepository _organisationRepository;

        public UserValidator(IOrganisationRepository organisationRepository)
        {
            _organisationRepository = organisationRepository;

            RuleFor(u => u.FirstName)
                .NotEmpty()
                .WithMessage("Le prénom est obligatoire.")
                .MaximumLength(50)
                .WithMessage("Le prénom ne peut pas dépasser 50 caractères.");

            RuleFor(u => u.LastName)
                .NotEmpty()
                .WithMessage("Le nom est obligatoire.")
                .MaximumLength(50)
                .WithMessage("Le nom ne peut pas dépasser 50 caractères.");

            RuleFor(u => u.UserName)
                .NotEmpty()
                .WithMessage("Le nom d'utilisateur est obligatoire.")
                .MaximumLength(20)
                .WithMessage("Le nom d'utilisateur ne doit pas dépasser 20 caractères.");

            // ✅ UNE SEULE règle Email avec TOUTES les validations
            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("L'adresse email est obligatoire.")
                .EmailAddress()
                .WithMessage("Le format de l'email est invalide.")
                .MustAsync(async (dto, email, cancellationToken) =>
                    await EmailDomainMatchesOrganisation(dto.OrganisationId, email, cancellationToken))
                .WithMessage(u => $"L'organisation sélectionnée n'autorise pas le domaine @{u.Email?.Split('@').LastOrDefault()}. Veuillez contacter votre administrateur.");

            RuleFor(u => u.Role)
                .NotNull()
                .WithMessage("Le rôle est obligatoire.");

            RuleFor(u => u.OrganisationId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} doit être supérieur à 0.")
                .MustAsync(async (organisationId, cancellationToken) =>
                {
                    var organisation = await _organisationRepository.GetByIdAsync(organisationId);
                    return organisation != null;
                })
                .WithMessage("L'organisation avec l'Id {PropertyValue} n'existe pas.");

            RuleFor(u => u.TailleCm)
                .InclusiveBetween(100, 250)
                .When(u => u.TailleCm.HasValue)
                .WithMessage("La taille doit être comprise entre 100 et 250 cm.");

            RuleSet("Create", () =>
            {
                RuleFor(u => u.Password)
                    .NotEmpty()
                    .WithMessage("Le mot de passe est obligatoire.")
                    .MinimumLength(8)
                    .WithMessage("Le mot de passe doit contenir au moins 8 caractères.")
                    .Matches(@"[A-Z]")
                    .WithMessage("Le mot de passe doit contenir au moins une majuscule.")
                    .Matches(@"[0-9]")
                    .WithMessage("Le mot de passe doit contenir au moins un chiffre.");
            });

            RuleSet("Update", () =>
            {
                RuleFor(u => u.Id)
                    .NotEmpty()
                    .WithMessage("L'ID de l'utilisateur est requis pour la mise à jour.");

                RuleFor(u => u.Password)
                    .MinimumLength(8)
                    .When(u => !string.IsNullOrEmpty(u.Password))
                    .WithMessage("Le mot de passe doit contenir au moins 8 caractères.")
                    .Matches(@"[A-Z]")
                    .When(u => !string.IsNullOrEmpty(u.Password))
                    .WithMessage("Le mot de passe doit contenir au moins une majuscule.")
                    .Matches(@"[0-9]")
                    .When(u => !string.IsNullOrEmpty(u.Password))
                    .WithMessage("Le mot de passe doit contenir au moins un chiffre.");
            });
        }

        private async Task<bool> EmailDomainMatchesOrganisation(int organisationId, string email, CancellationToken cancellationToken)
        {
            Console.WriteLine($"🔍 VALIDATION EMAIL APPELÉE - OrgId: {organisationId}, Email: {email}");

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                Console.WriteLine("❌ Email vide ou sans @");
                return false;
            }

            var emailDomain = "@" + email.Split('@')[1];
            Console.WriteLine($"🔍 Domain extrait: {emailDomain}");

            var organisation = await _organisationRepository.GetByIdAsync(organisationId);
            Console.WriteLine($"🔍 Organisation trouvée: {organisation?.Name}, EmailAutorise: {organisation?.EmailAutorise}");

            if (organisation == null || !organisation.IsActif)
            {
                Console.WriteLine("❌ Organisation null ou inactive");
                return false;
            }

            var result = organisation.EmailAutorise == emailDomain;
            Console.WriteLine($"🔍 Résultat validation: {result}");
            return result;
        }
    }
}