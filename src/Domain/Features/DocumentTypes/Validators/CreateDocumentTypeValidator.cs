using Domain.Features.DocumentTypes.Entities;
using FluentValidation;

namespace Domain.Features.DocumentTypes.Validators;

public class CreateDocumentTypeValidator : AbstractValidator<DocumentType>
{
    public CreateDocumentTypeValidator(IDocumentTypeRepository repository)
    {
        RuleFor(x => x.Name).MustAsync(async (name, cancellationToken) =>
        {
            var documentType = await repository.GetByNameAsync(name);
            return documentType == null;
        }).WithMessage("Le nom du type de document doit être unique");
    }
}
