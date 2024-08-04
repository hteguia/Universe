using Domain.Interfaces.Repositories.Base;

namespace Domain.Features.DocumentTypes.Repositories;

public interface IDocumentTypeRepository : IAsyncRepository<DocumentType>
{
    public Task<DocumentType?> GetByNameAsync(string name);
}