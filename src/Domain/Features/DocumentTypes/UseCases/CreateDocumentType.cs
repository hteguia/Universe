using Domain.Exceptions;
using Domain.Features.DocumentTypes.Repositories;
using Domain.Features.DocumentTypes.Validators;
using Domain.Interfaces.Repositories.Base;

namespace Domain.Features.DocumentTypes.UseCases;

public class CreateDocumentType(IUnitOfWork unitOfWork)
{
    public async Task<DocumentType> Create(string name, string description)
    {
        var repository = unitOfWork.AsyncRepository<DocumentType>();
        var documentType = new DocumentType(name, description);
        CreateDocumentTypeValidator validator = new CreateDocumentTypeValidator(unitOfWork.DocumentTypeRepository);
        var validationResut = await validator.ValidateAsync(documentType);
        if (validationResut.Errors.Count > 0)
        {
            throw new ValidationException(validationResut);
        }
        
        await repository.AddAsync(documentType);
        await unitOfWork.SaveChangesAsync();
        
        return documentType;
    }
}