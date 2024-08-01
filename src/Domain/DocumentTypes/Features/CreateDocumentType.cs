using Domain.DocumentTemplates;
using Domain.DocumentTypes.Models;
using Domain.DocumentTypes.Validators;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.DocumentTypes.Features;

public class CreateDocumentType(IUnitOfWork unitOfWork)
{
    public async Task<DocumentType> Create(string name, string description)
    {
        var repository = unitOfWork.AsyncRepository<DocumentType>();
        var documentType = new DocumentType(name, description);
        DocumentTypeValidator typeValidator = new DocumentTypeValidator(repository as IDocumentTypeRepository);
        var validationResut = await typeValidator.ValidateAsync(documentType);
        if (validationResut.Errors.Count > 0)
        {
            throw new ValidationException(validationResut);
        }
        
        await repository.AddAsync(documentType);
        await unitOfWork.SaveChangesAsync();
        
        return documentType;
    }
}