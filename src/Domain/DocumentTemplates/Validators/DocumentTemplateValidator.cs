using Domain.DocumentTemplates.Models;
using FluentValidation;

namespace Domain.DocumentTemplates.Validators;

public class DocumentTemplateValidator : AbstractValidator<DocumentTemplate>
{
    public DocumentTemplateValidator(IDocumentTemplateRepository repository)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Le nom du template de document est obligatoire");
        When(x => !string.IsNullOrWhiteSpace(x.Name), () => {
            RuleFor(x => x.Name).Matches(@"^[\w,\s-]+\.(pdf|docx)$").WithMessage("Le nom du template de document doit être un nom de fichier valide avec une extension");
        });
        RuleFor(x=>x.Name).MustAsync(async (name, cancellationToken) => {
            var documentTemplate = await repository.GetByNameAsync(name);
            return documentTemplate == null;
        }).WithMessage("Le nom du template de document doit être unique");
        
       
        
        
    }
}