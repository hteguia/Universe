using Domain.DocumentTemplates.Models;
using Domain.Interfaces;

namespace Domain.DocumentTemplates;

public interface IDocumentTemplateRepository : IAsyncRepository<DocumentTemplate>
{
    
}