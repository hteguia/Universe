using Domain.DocumentTypes.Models;
using FluentValidation;

namespace Domain.DocumentTypes.Validations;

public class DocumentValidator : AbstractValidator<DocumentType>
{
    public DocumentValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Le nom est obligatoire");
        RuleFor(x => x.Description).NotEmpty().WithMessage("La description est obligatoire");
    }
}