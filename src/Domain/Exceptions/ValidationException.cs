using FluentValidation.Results;

namespace Domain.Exceptions;

public class ValidationException : ApplicationException
{
    public Dictionary<string,string> ValidationErrors { get; set; }
    public ValidationException(ValidationResult validationResult)
    {
        ValidationErrors = new Dictionary<string, string>();

        foreach (var error in validationResult.Errors)
        {
            ValidationErrors.Add(error.PropertyName,error.ErrorMessage);
            ValidationErrors.Add("last",error.ErrorMessage);
        }
    }
}