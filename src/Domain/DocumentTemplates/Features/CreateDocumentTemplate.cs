using Domain.Contracts;
using Domain.DocumentTemplates.Models;
using Domain.DocumentTemplates.Validators;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.DocumentTemplates.Features;

public class CreateDocumentTemplate(IUnitOfWork unitOfWorkMock, IFileRepository fileRepository)
{
    public async Task<DocumentTemplate> Create(string name, byte[] fileContent)
    {
        var repository = unitOfWorkMock.AsyncRepository<DocumentTemplate>();
        var documentTemplate = new DocumentTemplate(name);
        DocumentTemplateValidator validator = new DocumentTemplateValidator(repository as IDocumentTemplateRepository);
        var validationResut = await validator.ValidateAsync(documentTemplate);
        if (validationResut.Errors.Count > 0)
        {
            throw new Domain.Exceptions.ValidationException(validationResut);
        }
        var path = fileRepository.SaveFile(name, fileContent);
        documentTemplate.UpdatePath(path);
        await repository.AddAsync(documentTemplate);
        await unitOfWorkMock.SaveChangesAsync();
        return documentTemplate;
    }
}