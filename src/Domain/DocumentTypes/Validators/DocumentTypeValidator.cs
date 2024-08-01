using Domain.DocumentTypes.Models;
using FluentValidation;

namespace Domain.DocumentTypes.Validators;

public class DocumentTypeValidator : AbstractValidator<DocumentType>
{
    public DocumentTypeValidator(IDocumentTypeRepository repository)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Le nom du type de document est obligatoire");
        RuleFor(x => x.Name).MustAsync(async (name, cancellationToken) =>
        {
            var documentType = await repository.GetByNameAsync(name);
            return documentType == null;
        }).WithMessage("Le nom du type de document doit être unique");
        RuleFor(x => x.Description).NotEmpty().WithMessage("La description du type de document est obligatoire");
    }
}
