using Domain.DocumentTypes;
using Domain.DocumentTypes.Models;
using Domain.ServiceRequests.Models;
using FluentValidation;

namespace Domain.ServiceRequests.Validotors;

public class ServiceRequestValidator : AbstractValidator<ServiceRequest>
{
    public ServiceRequestValidator(IDocumentTypeRepository repository)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Le nom de la demande de service est obligatoire");
        When(x => !string.IsNullOrWhiteSpace(x.Name), () => {
            RuleFor(x => x.Name).Matches(@"^[\w,\s-]+\.(pdf|docx)$").WithMessage("Le nom de la demande de service doit être un nom de fichier valide avec une extension");
        });
        RuleFor(p => p.DeadLine).NotEmpty().WithMessage("DeadLine is required");
        RuleFor(p => p.DocumentTypeId).GreaterThan(0).WithMessage("DocumentTypeId is required");
        RuleFor(x => x.DocumentTypeId).MustAsync(async (DocumentTypeId, cancellationToken) =>
        {
            var type = await repository.GetByIdAsync(DocumentTypeId);
            return type != null;
        }).WithMessage("Le type de document n'existe pas.");
    }
}