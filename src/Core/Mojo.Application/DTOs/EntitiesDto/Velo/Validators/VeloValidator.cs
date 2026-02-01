namespace Mojo.Application.DTOs.EntitiesDto.Velo.Validators
{
    public class VeloValidator : AbstractValidator<VeloDto>
    {
        private readonly IVeloRepository _veloRepository;

        public VeloValidator(IVeloRepository veloRepository)
        {
            _veloRepository = veloRepository;

            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("L'identifiant est obligatoire.")
                .GreaterThan(0).WithMessage("L'identifiant doit être supérieur à 0.")
                .MustAsync(async (id, token) => await _veloRepository.Exists(id))
                .WithMessage("Le vélo avec cet identifiant n'existe pas.");

            RuleFor(v => v.NumeroSerie)
                .NotEmpty().WithMessage("Le numéro de série est obligatoire.")
                .MaximumLength(50).WithMessage("Le numéro de série ne doit pas dépasser 50 caractères.")
                .MustAsync(async (velo, numero, token) =>
                {
                    return !await _veloRepository.NumeroSerieExists(numero, velo.Id);
                }).WithMessage("Ce numéro de série est déjà enregistré.");

            RuleFor(v => v.Marque)
                .NotEmpty().WithMessage("La marque est obligatoire.");

            RuleFor(v => v.Modele)
                .NotEmpty().WithMessage("Le modèle est obligatoire.");

            RuleFor(v => v.PrixAchat)
                .GreaterThan(0).WithMessage("Le prix d'achat doit être supérieur à 0.");

            RuleFor(v => v.Status)
                .NotNull().WithMessage("Le statut est obligatoire.");
        }
    }
}