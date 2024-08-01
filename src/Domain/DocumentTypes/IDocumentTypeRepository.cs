using Domain.DocumentTypes.Models;
using Domain.Interfaces;

namespace Domain.DocumentTypes;

public interface IDocumentTypeRepository : IAsyncRepository<DocumentType>
{
    Task<DocumentType> GetByNameAsync(string name);
}