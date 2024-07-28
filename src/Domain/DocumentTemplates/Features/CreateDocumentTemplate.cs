using Domain.Contrats;
using Domain.DocumentTemplates.Models;
using Domain.Interfaces;

namespace Domain.DocumentTemplates.Features;

public class CreateDocumentTemplate(IUnitOfWork unitOfWorkMock, IFileRepository fileRepository)
{
    public async Task<DocumentTemplate> Create(string name, byte[] fileContent)
    {
        var repository = unitOfWorkMock.AsyncRepository<DocumentTemplate>();
        var path = fileRepository.SaveFile(name, fileContent);
        var documentTemplate = new DocumentTemplate(name, path);
        await repository.AddAsync(documentTemplate);
        await unitOfWorkMock.SaveChangesAsync();
        return documentTemplate;
    }
}