using Domain.Features.DocumentTypes;
using Domain.Features.DocumentTypes.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class DocumentTypeRepository : RepositoryBase<DocumentType>, IDocumentTypeRepository
{
    public DatabaseContext _dbContext;
    public DocumentTypeRepository(DatabaseContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DocumentType> GetByNameAsync(string name)
    {
        return  await _dbContext.DocumentTypes.FirstOrDefaultAsync(x =>   x.Name == name);
    }
}