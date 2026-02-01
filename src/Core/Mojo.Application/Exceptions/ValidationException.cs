using FluentValidation.Results;

namespace Mojo.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public List<string> Errors { get; set; }

        public ValidationException(ValidationResult validationResult)
        {
            // ✅ CORRECTION : Initialiser la liste pour qu'elle ne soit plus null
            Errors = new List<string>();

            foreach (var error in validationResult.Errors)
            {
                Errors.Add(error.ErrorMessage);
            }
        }
    }
}
