using Domain.Interfaces.Repositories;
using FluentValidation;

namespace Domain.Features.DocumentTemplates.UseCases.CreateDocumentTemplate;

public class CreateDocumentTemplateUseCaseValidator : AbstractValidator<CreateDocumentTemplateUseCaseCommand>
{
    public CreateDocumentTemplateUseCaseValidator(IDocumentTemplateRepository repository)
    {
        When(x => !string.IsNullOrWhiteSpace(x.Name), () => {
            RuleFor(x => x.Name).Matches(@"^[\w,\s-]+\.(pdf|docx)$").WithMessage("Le nom du template de document doit être un nom de fichier valide avec une extension");
        });
        RuleFor(x=>x.Name).MustAsync(async (name, cancellationToken) => {
            var documentTemplate = await repository.GetByNameAsync(name);
            return documentTemplate == null;
        }).WithMessage("Le nom du template de document doit être unique");
    }
}