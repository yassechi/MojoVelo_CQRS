namespace Mojo.Application.DTOs.EntitiesDto.Amortissement.Validators
{
    public class AmortissementValidator : AbstractValidator<AmortissmentDto>
    {
        private readonly IVeloRepository _repository;

        public AmortissementValidator(IVeloRepository repository)
        {
            _repository = repository;

            RuleFor(a => a.ValeurInit)
                .GreaterThan(0)
                .WithMessage("{PropertyName} doit être supérieur à 0.");

            RuleFor(a => a.DureeMois)
                .GreaterThan(0)
                .WithMessage("{PropertyName} doit être supérieur à 0.");

            RuleFor(a => a.DateDebut)
                .NotEmpty()
                .WithMessage("La date de début est requise.");

            RuleFor(a => a.VeloId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} doit être supérieur à 0.")
                .MustAsync(async (veloId, cancellationToken) =>
                {
                    var velo = await _repository.GetByIdAsync(veloId);
                    return velo != null;
                })
                .WithMessage("Le vélo avec l'Id {PropertyValue} n'existe pas.");

            RuleSet("Create", () =>
            {
            });

            RuleSet("Update", () =>
            {
                RuleFor(a => a.Id)
                    .GreaterThan(0)
                    .WithMessage("L'ID de l'amortissement est requis pour la mise à jour.");
            });
        }
    }
}