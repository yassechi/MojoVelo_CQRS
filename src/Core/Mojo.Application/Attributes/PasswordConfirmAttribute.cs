using System.ComponentModel.DataAnnotations;

namespace Mojo.API.Attributes;

public class PasswordConfirmAttribute : ValidationAttribute
{
    public void Validate(object request)
    {
        var type = request.GetType();

        var password = type.GetProperty("Password")?.GetValue(request)?.ToString();
        var confirm = type.GetProperty("ConfirmPassword")?.GetValue(request)?.ToString();

        if (password != confirm)
            throw new System.ComponentModel.DataAnnotations.ValidationException("Les mots de passe ne correspondent pas.");
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var type = value.GetType();

        var password = type.GetProperty("Password")?.GetValue(value)?.ToString();
        var confirm = type.GetProperty("ConfirmPassword")?.GetValue(value)?.ToString();

        if (password != confirm)
            return new ValidationResult("Les mots de passe ne correspondent pas.");
        return ValidationResult.Success;
    }
}