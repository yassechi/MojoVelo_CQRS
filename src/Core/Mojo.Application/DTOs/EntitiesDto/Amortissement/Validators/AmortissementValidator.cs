namespace Mojo.Application.DTOs.EntitiesDto.Amortissement.Validators
{
    public class AmortissementValidator : AbstractValidator<AmortissmentDto>
    {
        private readonly IVeloRepository repository;

        public AmortissementValidator(IVeloRepository repository)
        {
            this.repository = repository;


            RuleFor(a => a.ValeurInit)
                .GreaterThan(0).WithMessage("{PropertyName} doit être supérieur à 0");

            RuleFor(a => a.DateDebut)
                .NotEmpty()
                .GreaterThan(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("La date de début doit être dans le futur");

            RuleFor(a => a.VeloId)
                .GreaterThan(0).WithMessage("{PropertyName} doit être plus {PropertyComparaison}")
                .MustAsync((id, tocken) =>
                {
                    var veoIdExists = repository.Exists(id);
                    return veoIdExists;
                });
        }
    }
}
