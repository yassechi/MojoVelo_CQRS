namespace Mojo.Application.DTOs.EntitiesDto.Intervention.Validators
{
    public class InterventionValidator : AbstractValidator<InterventionDto>
    {
        private readonly IVeloRepository _veloRepository;

        public InterventionValidator(IVeloRepository veloRepository)
        {
            _veloRepository = veloRepository;

            RuleFor(i => i.VeloId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} doit être supérieur à 0.")
                .MustAsync(async (veloId, cancellationToken) =>
                {
                    var velo = await _veloRepository.GetByIdAsync(veloId);
                    return velo != null;
                })
                .WithMessage("Le vélo avec l'Id {PropertyValue} n'existe pas.");

            RuleFor(i => i.Cout)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Le coût de l'intervention ne peut pas être négatif.");

            RuleFor(i => i.DateIntervention)
                .NotEmpty()
                .WithMessage("La date d'intervention est obligatoire.");

            RuleFor(i => i.TypeIntervention)
                .NotEmpty()
                .WithMessage("Le type d'intervention est obligatoire.")
                .MaximumLength(50)
                .WithMessage("Le type d'intervention ne doit pas dépasser 50 caractères.");

            RuleFor(i => i.Description)
                .NotEmpty()
                .WithMessage("La description est obligatoire.")
                .MinimumLength(10)
                .WithMessage("La description doit contenir au moins 10 caractères.");

            RuleSet("Create", () =>
            {
                RuleFor(i => i.DateIntervention)
                    .GreaterThanOrEqualTo(DateTime.Today)
                    .WithMessage("La date d'intervention ne peut pas être dans le passé.");
            });

            RuleSet("Update", () =>
            {
                RuleFor(i => i.Id)
                    .GreaterThan(0)
                    .WithMessage("L'ID de l'intervention est requis pour la mise à jour.");
            });
        }
    }
}