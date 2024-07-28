using Domain.DocumentTypes.Models;
using Domain.DocumentTypes.Validations;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.DocumentTypes.Features;

public class CreateDocumentType(IUnitOfWork unitOfWork)
{
    public async Task<DocumentType> Create(string name, string description)
    {
        var repository = unitOfWork.AsyncRepository<DocumentType>();

        var documentType = new DocumentType(name, description);
        
        DocumentValidator validator = new DocumentValidator();
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