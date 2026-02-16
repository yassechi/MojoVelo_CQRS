namespace Mojo.Application.DTOs.EntitiesDto.MoisAmortissement.Validators
{
    public class MoisAmortissementValidator : AbstractValidator<MoisAmortissementDto>
    {
        private readonly IAmortissementRepository _amortissementRepository;
        private readonly IMoisAmortissementRepository _moisRepository;

        public MoisAmortissementValidator(
            IAmortissementRepository amortissementRepository,
            IMoisAmortissementRepository moisRepository)
        {
            _amortissementRepository = amortissementRepository;
            _moisRepository = moisRepository;

            RuleFor(m => m.AmortissementId)
                .GreaterThan(0)
                .WithMessage("L'amortissement est requis.")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var amortissement = await _amortissementRepository.GetByIdAsync(id);
                    return amortissement != null;
                })
                .WithMessage("L'amortissement avec l'Id {PropertyValue} n'existe pas.");

            RuleFor(m => m.NumeroMois)
                .GreaterThan(0)
                .WithMessage("Le numéro du mois doit être supérieur à 0.");

            RuleFor(m => m.Montant)
                .GreaterThan(0)
                .WithMessage("Le montant d'amortissement doit être supérieur à 0.");

            RuleSet("Create", () =>
            {
                RuleFor(m => m)
                    .MustAsync(async (dto, cancellationToken) =>
                    {
                        return !await _moisRepository.ExistsForMonthAsync(dto.AmortissementId, dto.NumeroMois);
                    })
                    .WithMessage("Ce mois existe déjà pour cet amortissement.");

                RuleFor(m => m)
                    .MustAsync(async (dto, cancellationToken) =>
                    {
                        var amortissement = await _amortissementRepository.GetByIdAsync(dto.AmortissementId);
                        return amortissement == null || dto.NumeroMois <= amortissement.DureeMois;
                    })
                    .WithMessage("Le numéro du mois dépasse la durée de l'amortissement.");
            });

            RuleSet("Update", () =>
            {
                RuleFor(m => m.Id)
                    .GreaterThan(0)
                    .WithMessage("L'ID du mois d'amortissement est requis pour la mise à jour.");

                RuleFor(m => m)
                    .MustAsync(async (dto, cancellationToken) =>
                    {
                        return !await _moisRepository.ExistsForMonthAsync(dto.AmortissementId, dto.NumeroMois, dto.Id);
                    })
                    .WithMessage("Ce mois existe déjà pour cet amortissement.");
            });
        }
    }
}
