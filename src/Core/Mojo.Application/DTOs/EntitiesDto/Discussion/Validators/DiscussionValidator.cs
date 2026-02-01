using FluentValidation;

namespace Mojo.Application.DTOs.EntitiesDto.Discussion.Validators
{
    public class DiscussionValidator : AbstractValidator<DiscussionDto>
    {
        private readonly IUserRepository _userRepository;

        public DiscussionValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(d => d.MojoId)
                .NotEmpty().WithMessage("L'identifiant Mojo est obligatoire.")
                .MustAsync(async (id, token) => await _userRepository.UserExists(id))
                .WithMessage("L'utilisateur Mojo spécifié n'existe pas.");

            RuleFor(d => d.ClientId)
                .NotEmpty().WithMessage("L'identifiant du client est obligatoire.")
                .MustAsync(async (id, token) => await _userRepository.UserExists(id))
                .WithMessage("Le client spécifié n'existe pas.");

            RuleFor(d => d.Objet)
                .NotEmpty().WithMessage("L'objet de la discussion est obligatoire.")
                .MaximumLength(100).WithMessage("L'objet ne doit pas dépasser 100 caractères.");

        }
    }
}