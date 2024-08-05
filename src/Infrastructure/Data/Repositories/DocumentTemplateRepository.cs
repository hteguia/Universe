using Domain.Features.DocumentTemplates;
using Domain.Features.DocumentTemplates.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class DocumentTemplateRepository : RepositoryBase<DocumentTemplate>, IDocumentTemplateRepository
{
    public DocumentTemplateRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }

    public async Task<DocumentTemplate> GetByNameAsync(string name)
    {
        return  await _dbSet.FirstOrDefaultAsync(x =>   x.Name == name);
    }
}