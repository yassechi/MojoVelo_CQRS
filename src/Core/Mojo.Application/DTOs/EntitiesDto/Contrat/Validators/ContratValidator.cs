using FluentValidation;

namespace Mojo.Application.DTOs.EntitiesDto.Contrat.Validators
{
    public class ContratValidator : AbstractValidator<ContratDto>
    {
        private readonly IVeloRepository _veloRepository;
        private readonly IUserRepository _userRepository;

        public ContratValidator(IVeloRepository veloRepository, IUserRepository userRepository)
        {
            _veloRepository = veloRepository;
            _userRepository = userRepository;

            RuleFor(c => c.VeloId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} doit être supérieur à 0.")
                .MustAsync(async (veloId, cancellationToken) =>
                {
                    var velo = await _veloRepository.GetByIdAsync(veloId);
                    return velo != null;
                })
                .WithMessage("Le vélo avec l'Id {PropertyValue} n'existe pas.");

            RuleFor(c => c.BeneficiaireId)
                .NotEmpty()
                .WithMessage("{PropertyName} est requis.")
                .MustAsync(async (beneficiaireId, cancellationToken) =>
                {
                    var user = await _userRepository.GetUserByStringId(beneficiaireId);
                    return user != null;
                })
                .WithMessage("Le bénéficiaire avec l'Id {PropertyValue} n'existe pas.");

            RuleFor(c => c.UserRhId)
                .NotEmpty()
                .WithMessage("{PropertyName} est requis.")
                .MustAsync(async (userRhId, cancellationToken) =>
                {
                    var user = await _userRepository.GetUserByStringId(userRhId);
                    return user != null;
                })
                .WithMessage("Le responsable RH avec l'Id {PropertyValue} n'existe pas.")
                .MustAsync(async (userRhId, cancellationToken) =>
                {
                    var user = await _userRepository.GetUserByStringId(userRhId);
                    if (user == null) return false;
                    return (int)user.Role == 2 || (int)user.Role == 1;
                })
                .WithMessage("Le responsable RH doit avoir le rôle 'Négociateur' (Role = 2).");

            RuleFor(c => c.LoyerMensuelHT)
                .GreaterThan(0)
                .WithMessage("{PropertyName} doit être strictement supérieur à 0.");

            RuleFor(c => c.MontantAmortissementMensuel)
                .GreaterThan(0)
                .WithMessage("Le montant d'amortissement mensuel doit être strictement supérieur à 0.")
                .When(c => c.MontantAmortissementMensuel.HasValue);

            RuleFor(c => c.DateDebut)
                .NotEmpty()
                .WithMessage("La date de début est obligatoire.");

            RuleFor(c => c.DateFin)
                .GreaterThan(c => c.DateDebut)
                .WithMessage("La date de fin doit être postérieure à la date de début.");

            RuleSet("Create", () =>
            {
                RuleFor(c => c.DateDebut)
                    .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                    .WithMessage("La date de début ne peut pas être dans le passé.");
            });

            RuleSet("Update", () =>
            {
                RuleFor(c => c.Id)
                    .GreaterThan(0)
                    .WithMessage("L'ID du contrat est requis pour la mise à jour.");
            });
        }
    }
}