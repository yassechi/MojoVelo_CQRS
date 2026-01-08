namespace Mojo.Application.DTOs.EntitiesDto.Login.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("Le nom d'utilisateur est requis.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Le mot de passe est requis.");
        }
    }
}