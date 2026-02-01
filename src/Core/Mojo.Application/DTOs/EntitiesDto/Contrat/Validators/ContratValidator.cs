using FluentValidation;
using Mojo.Application.DTOs.EntitiesDto.Contrat;

public class ContratValidator : AbstractValidator<ContratDto>
{
    private readonly IVeloRepository _veloRepository;
    private readonly IUserRepository _userRepository;
    private readonly IContratRepository _contratRepository;

    public ContratValidator(
        IVeloRepository veloRepository,
        IUserRepository userRepository,
        IContratRepository contratRepository)
    {
        _veloRepository = veloRepository;
        _userRepository = userRepository;
        _contratRepository = contratRepository;

        // ✅ 1. Vérifier que le contrat existe (pour l'update)
        RuleFor(c => c.Id)
            .MustAsync(async (id, token) => id == 0 || await _contratRepository.Exists(id))
            .WithMessage("Le contrat spécifié n'existe pas.");

        // ✅ 2. Vérifier que le UserRh existe
        RuleFor(c => c.UserRhId)
            .NotEmpty().WithMessage("L'identifiant du responsable RH est obligatoire.")
            .MustAsync(async (id, token) => await _userRepository.UserExists(id))
            .WithMessage("Le responsable RH spécifié n'existe pas.");

        // ✅ 3. LA RÈGLE CRITIQUE : Bloquer si pas Négociateur
        RuleFor(c => c.UserRhId)
            .MustAsync(async (id, token) =>
            {
                if (string.IsNullOrEmpty(id)) return false;
                return await _userRepository.UserHasRole(id, "Negociateur");
            })
            .WithMessage("L'utilisateur spécifié n'a pas le rôle Négociateur.");

        // ✅ 4. Autres règles communes
        RuleFor(c => c.VeloId)
            .MustAsync(async (id, token) => await _veloRepository.Exists(id))
            .WithMessage("Le vélo spécifié n'existe pas.");

        RuleFor(c => c.LoyerMensuelHT)
            .GreaterThan(0).WithMessage("Le loyer doit être supérieur à 0.");
    }
}