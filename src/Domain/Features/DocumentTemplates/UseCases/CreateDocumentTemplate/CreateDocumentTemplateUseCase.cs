using Domain.Features.DocumentTemplates.Entities;
using Domain.Interfaces.Providers;
using Domain.Interfaces.Repositories.Base;

namespace Domain.Features.DocumentTemplates.UseCases.CreateDocumentTemplate;

public class CreateDocumentTemplateUseCase(IUnitOfWork unitOfWorkMock, IFileProvider fileRepository)
{
    public async Task<DocumentTemplate> Create(CreateDocumentTemplateUseCaseCommand model)
    {
        var repository = unitOfWorkMock.AsyncRepository<DocumentTemplate>();
        CreateDocumentTemplateUseCaseValidator useCaseValidator = new CreateDocumentTemplateUseCaseValidator(unitOfWorkMock.DocumentTemplateRepository);
        var validationResut = await useCaseValidator.ValidateAsync(model);
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