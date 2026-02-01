using FluentValidation;

namespace Mojo.Application.DTOs.EntitiesDto.Intervention.Validators
{
    public class InterventionValidator : AbstractValidator<InterventionDto>
    {
        private readonly IVeloRepository _veloRepository;

        public InterventionValidator(IVeloRepository veloRepository)
        {
            _veloRepository = veloRepository;

            RuleSet("Create", () =>
            {
                RuleFor(i => i.DateIntervention)
                    .NotEmpty().WithMessage("La date d'intervention est obligatoire.")
                    .GreaterThanOrEqualTo(DateTime.Today)
                    .WithMessage("La date d'intervention ne peut pas être dans le passé.");
            });

            RuleSet("Update", () =>
            {
                RuleFor(i => i.DateIntervention)
                    .NotEmpty().WithMessage("La date d'intervention est obligatoire.");
            });

            RuleFor(i => i.VeloId)
                .GreaterThan(0).WithMessage("L'identifiant du vélo doit être supérieur à 0.")
                .MustAsync(async (id, token) => await _veloRepository.Exists(id))
                .WithMessage("Le vélo spécifié n'existe pas dans la base de données.");

            RuleFor(i => i.Cout)
                .GreaterThanOrEqualTo(0).WithMessage("Le coût de l'intervention ne peut pas être négatif.");

            RuleFor(i => i.TypeIntervention)
                .NotEmpty().WithMessage("Le type d'intervention est obligatoire.")
                .MaximumLength(50).WithMessage("Le type d'intervention ne doit pas dépasser 50 caractères.");

            RuleFor(i => i.Description)
                .NotEmpty().WithMessage("La description est obligatoire.")
                .MinimumLength(10).WithMessage("La description doit contenir au moins 10 caractères pour être explicite.");
        }
    }
}