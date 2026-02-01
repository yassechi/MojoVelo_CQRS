using FluentValidation;
using Mojo.Application.DTOs.EntitiesDto.Amortissement;

namespace Mojo.Application.DTOs.EntitiesDto.Amortissement.Validators
{
    public class AmortissementValidator : AbstractValidator<AmortissmentDto>
    {
        private readonly IVeloRepository _veloRepository;
        private readonly IAmortissementRepository _amortissementRepository;

        public AmortissementValidator(
            IVeloRepository veloRepository,
            IAmortissementRepository amortissementRepository)
        {
            _veloRepository = veloRepository;
            _amortissementRepository = amortissementRepository;

            // RÈGLES POUR LA MISE À JOUR UNIQUEMENT
            RuleSet("Update", () =>
            {
                RuleFor(a => a.Id)
                    .GreaterThan(0).WithMessage("L'identifiant doit être supérieur à 0.")
                    .MustAsync(async (id, token) => await _amortissementRepository.Exists(id))
                    .WithMessage("L'amortissement spécifié n'existe pas.");
            });

            // RÈGLES COMMUNES (CREATE ET UPDATE)

            RuleFor(a => a.ValeurInit)
                .GreaterThan(0)
                .WithMessage("La valeur initiale doit être supérieure à 0.");

            RuleFor(a => a.DateDebut)
                .NotEmpty()
                .WithMessage("La date de début est obligatoire.");

            RuleFor(a => a.DureeMois)
                .GreaterThan(0)
                .WithMessage("La durée en mois doit être supérieure à 0.");

            RuleFor(a => a.ValeurResiduelleFinale)
                .GreaterThanOrEqualTo(0)
                .WithMessage("La valeur résiduelle finale ne peut pas être négative.")
                .LessThan(a => a.ValeurInit)
                .WithMessage("La valeur résiduelle finale doit être inférieure à la valeur initiale.");

            RuleFor(a => a.VeloId)
                .GreaterThan(0)
                .WithMessage("L'identifiant du vélo doit être supérieur à 0.")
                .MustAsync(async (id, token) => await _veloRepository.Exists(id))
                .WithMessage("Le vélo spécifié n'existe pas.");
        }
    }
}