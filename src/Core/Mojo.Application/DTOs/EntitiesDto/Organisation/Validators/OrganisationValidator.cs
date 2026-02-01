namespace Mojo.Application.DTOs.EntitiesDto.Organisation.Validators
{
    public class OrganisationValidator : AbstractValidator<OrganisationDto>
    {
        public OrganisationValidator()
        {
            RuleFor(o => o.Name)
                .NotEmpty().WithMessage("Le nom de l'organisation est obligatoire.")
                .MaximumLength(100).WithMessage("Le nom ne doit pas dépasser 100 caractères.");

            RuleFor(o => o.Code)
                .NotEmpty().WithMessage("Le code est obligatoire.")
                .MaximumLength(10).WithMessage("Le code ne doit pas dépasser 10 caractères.");

            RuleFor(o => o.Address)
                .NotEmpty().WithMessage("L'adresse physique est requise.");

            RuleFor(o => o.ContactEmail)
                .NotEmpty().WithMessage("L'email de contact est obligatoire.")
                .EmailAddress().WithMessage("Le format de l'adresse email n'est pas valide.");

            RuleFor(o => o.IsActif)
                .NotNull().WithMessage("Le statut d'activité doit être défini.");
        }
    }
}
