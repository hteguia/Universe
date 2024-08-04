using Domain.Contracts;
using Domain.Features.DocumentTemplates.Models;
using Domain.Features.DocumentTemplates.Repositories;
using Domain.Features.DocumentTemplates.Validators;
using Domain.Interfaces.Repositories.Base;

namespace Domain.Features.DocumentTemplates.UseCases;

public class CreateDocumentTemplate(IUnitOfWork unitOfWorkMock, IFileRepository fileRepository)
{
    public async Task<DocumentTemplate> Create(CreateDocumentTemplateModel model)
    {
        var repository = unitOfWorkMock.AsyncRepository<DocumentTemplate>();
        CreateDocumentTemplateValidator validator = new CreateDocumentTemplateValidator(unitOfWorkMock.DocumentTemplateRepository);
        var validationResut = await validator.ValidateAsync(model);
        if (validationResut.Errors.Count > 0)
        {
            throw new Domain.Exceptions.ValidationException(validationResut);
        }
        var path = fileRepository.SaveFile(model.Name, model.FileContent);
        var documentTemplate = new DocumentTemplate(model.Name, path);
        documentTemplate.UpdatePath(path);
        await repository.AddAsync(documentTemplate);
        await unitOfWorkMock.SaveChangesAsync();
        return documentTemplate;
    }
}