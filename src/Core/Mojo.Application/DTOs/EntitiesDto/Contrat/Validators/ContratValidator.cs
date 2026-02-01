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
                .GreaterThan(0).WithMessage("L'identifiant du vélo doit être supérieur à 0.")
                .MustAsync(async (id, token) => await _veloRepository.Exists(id))
                .WithMessage("Le vélo spécifié n'existe pas.");

            RuleFor(c => c.BeneficiaireId)
                .GreaterThan(0).WithMessage("L'identifiant du bénéficiaire doit être supérieur à 0.")
                .MustAsync(async (id, token) => await _userRepository.Exists(id))
                .WithMessage("Le bénéficiaire spécifié n'existe pas.");

            RuleFor(c => c.UserRhId)
                .GreaterThan(0).WithMessage("L'identifiant du responsable RH doit être supérieur à 0.")
                .MustAsync(async (id, token) => await _userRepository.Exists(id))
                .WithMessage("Le responsable RH spécifié n'existe pas.");

            RuleFor(c => c.LoyerMensuelHT)
                .GreaterThan(0).WithMessage("Le loyer mensuel HT doit être strictement supérieur à 0.");

            RuleFor(c => c.DateDebut)
                .NotEmpty().WithMessage("La date de début est obligatoire.")
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("La date de début ne peut pas être dans le passé.");

            RuleFor(c => c.DateFin)
                .NotEmpty().WithMessage("La date de fin est obligatoire.")
                .GreaterThan(c => c.DateDebut)
                .WithMessage("La date de fin doit être postérieure à la date de début.");
        }
    }
}