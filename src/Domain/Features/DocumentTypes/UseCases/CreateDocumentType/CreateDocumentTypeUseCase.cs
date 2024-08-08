using Domain.Exceptions;
using Domain.Features.DocumentTypes.Models;
using Domain.Interfaces.Repositories.Base;

namespace Domain.Features.DocumentTypes.UseCases.CreateDocumentType;

public class CreateDocumentTypeUseCase(IUnitOfWork unitOfWork)
{
    public async Task<DocumentType> Create(CreateDocumentTypeUseCaseCommand command)
    {
        var repository = unitOfWork.AsyncRepository<DocumentType>();
        var documentType = new DocumentType(command.Name, command.Description);
        CreateDocumentTypeUseCaseValidator validator = new CreateDocumentTypeUseCaseValidator(unitOfWork.DocumentTypeRepository);
        var validationResut = await validator.ValidateAsync(command);
        if (validationResut.Errors.Count > 0)
        {
            throw new ValidationException(validationResut);
        }
        
        await repository.AddAsync(documentType);
        await unitOfWork.SaveChangesAsync();
        
        return documentType;
    }
}