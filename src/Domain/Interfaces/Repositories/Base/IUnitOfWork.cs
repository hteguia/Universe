using Domain.Features.DocumentTemplates;
using Domain.Features.DocumentTypes;

namespace Domain.Interfaces.Repositories.Base;

public interface IUnitOfWork
{

    Task<int> SaveChangesAsync();

    IAsyncRepository<T> AsyncRepository<T>() where T : class;
    
    IDocumentTypeRepository DocumentTypeRepository { get; }
    IDocumentTemplateRepository DocumentTemplateRepository { get; }
    
}