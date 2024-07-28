using System.ComponentModel;
using System.Reflection.Metadata;
using Domain.DocumentTypes.Models;
using Domain.DocumentTypes.Validations;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.DocumentTypes.Features;

public class UpdateDocumentType(IUnitOfWork unitOfWork)
{
    public async Task Update(int id, string name, string description)
    {
        var repository = unitOfWork.AsyncRepository<DocumentType>();

        var documentType = await repository.GetByIdAsync(id);
        if (documentType == null)
        {
            throw new NotFoundException("DocumentType", id);
        }
        
        documentType?.Update(name, description);
        
        DocumentValidator validator = new DocumentValidator();
        var validationResut = await validator.ValidateAsync(documentType);
        if (validationResut.Errors.Count > 0)
        {
            throw new ValidationException(validationResut);
        }
        
        await repository.UpdateAsync(documentType);
        await unitOfWork.SaveChangesAsync();
    }
}