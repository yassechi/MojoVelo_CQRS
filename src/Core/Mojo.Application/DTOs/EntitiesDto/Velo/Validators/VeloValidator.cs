namespace Mojo.Application.DTOs.EntitiesDto.Velo.Validators
{
    public class VeloValidator : AbstractValidator<VeloDto>
    {
        private readonly IVeloRepository _veloRepository;

        public VeloValidator(IVeloRepository veloRepository)
        {
            _veloRepository = veloRepository;

            RuleFor(v => v.NumeroSerie)
                .NotEmpty()
                .WithMessage("Le numéro de série est obligatoire.")
                .MaximumLength(50)
                .WithMessage("Le numéro de série ne doit pas dépasser 50 caractères.")
                .MustAsync(async (velo, numeroSerie, cancellationToken) =>
                {
                    return !await _veloRepository.NumeroSerieExists(numeroSerie, velo.Id);
                })
                .WithMessage("Ce numéro de série est déjà enregistré.");

            RuleFor(v => v.Marque)
                .NotEmpty()
                .WithMessage("La marque est obligatoire.");

            RuleFor(v => v.Modele)
                .NotEmpty()
                .WithMessage("Le modèle est obligatoire.");

            RuleFor(v => v.Type)
                .MaximumLength(100)
                .WithMessage("Le type ne doit pas dépasser 100 caractères.");

            RuleFor(v => v.PrixAchat)
                .GreaterThan(0)
                .WithMessage("Le prix d'achat doit être supérieur à 0.");

            RuleFor(v => v.Status)
                .NotNull()
                .WithMessage("Le statut est obligatoire.");

            RuleSet("Create", () =>
            {
            });

            RuleSet("Update", () =>
            {
                RuleFor(v => v.Id)
                    .GreaterThan(0)
                    .WithMessage("L'ID du vélo est requis pour la mise à jour.");
            });
        }
    }
}
