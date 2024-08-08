using Domain.Features.DocumentTemplates.Entities;
using Domain.Interfaces.Repositories.Base;

namespace Domain.Features.DocumentTemplates;

public interface IDocumentTemplateRepository : IAsyncRepository<DocumentTemplate>
{
    Task<DocumentTemplate> GetByNameAsync(string name);
}