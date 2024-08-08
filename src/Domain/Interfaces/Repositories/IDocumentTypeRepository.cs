using Domain.Features.DocumentTypes.Models;
using Domain.Interfaces.Repositories.Base;

namespace Domain.Interfaces.Repositories;

public interface IDocumentTypeRepository : IAsyncRepository<DocumentType>
{
    public Task<DocumentType> GetByNameAsync(string name);
}