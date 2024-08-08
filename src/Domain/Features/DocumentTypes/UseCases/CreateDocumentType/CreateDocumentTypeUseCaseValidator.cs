using Domain.Interfaces.Repositories;
using FluentValidation;

namespace Domain.Features.DocumentTypes.UseCases.CreateDocumentType;

public class CreateDocumentTypeUseCaseValidator : AbstractValidator<CreateDocumentTypeUseCaseCommand>
{
    public CreateDocumentTypeUseCaseValidator(IDocumentTypeRepository repository)
    {
        RuleFor(x => x.Name).MustAsync(async (name, cancellationToken) =>
        {
            var documentType = await repository.GetByNameAsync(name);
            return documentType == null;
        }).WithMessage("Le nom du type de document doit être unique");
    }
}