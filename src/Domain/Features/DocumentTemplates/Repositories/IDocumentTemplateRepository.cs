using Domain.Interfaces.Repositories.Base;

namespace Domain.Features.DocumentTemplates.Repositories;

public interface IDocumentTemplateRepository : IAsyncRepository<DocumentTemplate>
{
    Task<DocumentTemplate> GetByNameAsync(string name);
}