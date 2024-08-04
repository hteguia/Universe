using Domain.Features.DocumentTemplates.Repositories;
using Domain.Features.DocumentTypes.Repositories;

namespace Domain.Interfaces.Repositories.Base;

public interface IUnitOfWork
{

    Task<int> SaveChangesAsync();

    IAsyncRepository<T> AsyncRepository<T>() where T : class;
    
    IDocumentTypeRepository DocumentTypeRepository { get; }
    IDocumentTemplateRepository DocumentTemplateRepository { get; }
    
}