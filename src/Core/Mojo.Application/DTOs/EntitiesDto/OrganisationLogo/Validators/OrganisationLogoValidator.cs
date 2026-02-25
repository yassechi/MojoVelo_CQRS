using Mojo.Application.DTOs.EntitiesDto.OrganisationLogo;

namespace Mojo.Application.DTOs.EntitiesDto.OrganisationLogo.Validators
{
    public class OrganisationLogoValidator : AbstractValidator<OrganisationLogoDto>
    {
        private readonly IOrganisationRepository _organisationRepository;

        public OrganisationLogoValidator(IOrganisationRepository organisationRepository)
        {
            _organisationRepository = organisationRepository;

            RuleFor(l => l.OrganisationId)
                .GreaterThan(0)
                .WithMessage("OrganisationId must be greater than 0.")
                .MustAsync(async (organisationId, cancellationToken) =>
                {
                    var organisation = await _organisationRepository.GetByIdAsync(organisationId);
                    return organisation != null;
                })
                .WithMessage("Organisation with Id {PropertyValue} does not exist.");

            RuleFor(l => l.Fichier)
                .NotEmpty()
                .WithMessage("File is required.");

            RuleFor(l => l.NomFichier)
                .NotEmpty()
                .WithMessage("File name is required.")
                .MaximumLength(255)
                .WithMessage("File name cannot exceed 255 characters.");

            RuleFor(l => l.TypeFichier)
                .NotEmpty()
                .WithMessage("File type is required.")
                .Must(type =>
                {
                    var allowed = new[] { "image/png", "image/jpeg", "image/jpg", "image/gif", "image/webp" };
                    return allowed.Contains(type.ToLowerInvariant());
                })
                .WithMessage("File type not allowed. Allowed: image/png, image/jpeg, image/jpg, image/gif, image/webp.");

            RuleSet("Update", () =>
            {
                RuleFor(l => l.Id)
                    .GreaterThan(0)
                    .WithMessage("Logo Id is required for update.");
            });
        }
    }
}
