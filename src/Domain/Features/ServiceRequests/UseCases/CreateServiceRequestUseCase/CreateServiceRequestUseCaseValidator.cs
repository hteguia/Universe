using Domain.Interfaces.Repositories;
using FluentValidation;

namespace Domain.Features.ServiceRequests.UseCases.CreateServiceRequestUseCase;

public class CreateServiceRequestUseCaseValidator : AbstractValidator<CreateServiceRequestUseCaseCommand>
{
    public CreateServiceRequestUseCaseValidator(IDocumentTypeRepository repository)
    {

        RuleFor(x => x.DocumentTypeId).MustAsync(async (DocumentTypeId, cancellationToken) =>
        {
            var type = await repository.GetByIdAsync(DocumentTypeId);
            return type != null;
        }).WithMessage("Le type de document n'existe pas.");
    }
}