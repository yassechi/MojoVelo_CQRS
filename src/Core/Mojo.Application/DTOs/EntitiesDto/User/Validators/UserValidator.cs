using FluentValidation;

namespace Mojo.Application.DTOs.EntitiesDto.User.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        private readonly IOrganisationRepository _organisationRepository;
        
        public UserValidator(IOrganisationRepository organisationRepository)
        {
            _organisationRepository = organisationRepository;
            
            // ✅ RÈGLES POUR LA CRÉATION UNIQUEMENT
            RuleSet("Create", () =>
            {
                RuleFor(u => u.Password)
                   .NotEmpty().WithMessage("Le mot de passe est obligatoire !")
                   .MinimumLength(8).WithMessage("Le mot de passe doit contenir au moins 8 caractères !")
                   .Matches(@"[A-Z]").WithMessage("Le mot de passe doit contenir au moins une majuscule.")
                   .Matches(@"[0-9]").WithMessage("Le mot de passe doit contenir au moins un chiffre.");
            });
            
            // ✅ RÈGLES POUR LA MISE À JOUR UNIQUEMENT (Password optionnel, mais si fourni, doit être valide)
            RuleSet("Update", () =>
            {
                RuleFor(u => u.Password)
                   .MinimumLength(8).When(u => !string.IsNullOrEmpty(u.Password))
                   .WithMessage("Le mot de passe doit contenir au moins 8 caractères !")
                   .Matches(@"[A-Z]").When(u => !string.IsNullOrEmpty(u.Password))
                   .WithMessage("Le mot de passe doit contenir au moins une majuscule.")
                   .Matches(@"[0-9]").When(u => !string.IsNullOrEmpty(u.Password))
                   .WithMessage("Le mot de passe doit contenir au moins un chiffre.");
            });
            
            // ✅ RÈGLES COMMUNES (CREATE ET UPDATE)
            RuleFor(u => u.FirstName)
               .NotEmpty().WithMessage("Le prénom est obligatoire !")
               .MaximumLength(50).WithMessage("Le prénom ne peut pas dépasser 50 caractères !");
            
            RuleFor(u => u.LasttName)
               .NotEmpty().WithMessage("Le nom est obligatoire !")
               .MaximumLength(50).WithMessage("Le nom ne peut pas dépasser 50 caractères !");
            
            RuleFor(u => u.UserName)
                .NotEmpty().WithMessage("Le nom d'utilisateur est obligatoire !")
                .MaximumLength(20).WithMessage("Le nom d'utilisateur ne doit pas dépasser 20 caractères");
            
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("L'adresse email est obligatoire !")
                .EmailAddress().WithMessage("Le format de l'email est invalide !");
            
            RuleFor(u => u.Role)
               .NotNull().WithMessage("Le rôle est obligatoire !");
            
            RuleFor(u => u.OrganisationId)
               .NotEmpty().WithMessage("L'organisation est obligatoire !")
               .MustAsync(async (id, token) => await _organisationRepository.Exists(id))
               .WithMessage("L'organisation spécifiée n'existe pas.");
            
            RuleFor(u => u.TailleCm)
                .InclusiveBetween(100, 250).When(u => u.TailleCm.HasValue)
                .WithMessage("La taille doit être comprise entre 100 et 250 cm.");
        }
    }
}