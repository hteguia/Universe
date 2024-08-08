using Domain.Features.DocumentTypes.Entities;
using Domain.Interfaces.Repositories.Base;

namespace Domain.Features.DocumentTypes;

public interface IDocumentTypeRepository : IAsyncRepository<DocumentType>
{
    public Task<DocumentType> GetByNameAsync(string name);
}