using Domain.Features.DocumentTypes;
using Domain.Features.ServiceRequests.Entities;
using Domain.Features.ServiceRequests.Models;
using FluentValidation;

namespace Domain.Features.ServiceRequests.Validotors;

public class ServiceRequestValidator : AbstractValidator<CreateServiceRequestVM>
{
    public ServiceRequestValidator(IDocumentTypeRepository repository)
    {

        RuleFor(x => x.DocumentTypeId).MustAsync(async (DocumentTypeId, cancellationToken) =>
        {
            var type = await repository.GetByIdAsync(DocumentTypeId);
            return type != null;
        }).WithMessage("Le type de document n'existe pas.");
    }
}