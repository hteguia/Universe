using Domain.Features.DocumentTemplates.Repositories;
using Domain.Features.DocumentTypes.Repositories;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.Base;
using Infrastructure.Data.Repositories;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _dbContext;
    private readonly IDocumentTypeRepository _documentTypeRepository;
    private readonly IDocumentTemplateRepository _documentTemplateRepository;
    
    public UnitOfWork(DatabaseContext dbContext, IDocumentTypeRepository documentTypeRepository, IDocumentTemplateRepository documentTemplateRepository)
    {
        _dbContext = dbContext;
        _documentTypeRepository = documentTypeRepository;
        _documentTemplateRepository = documentTemplateRepository;
    }

    public IAsyncRepository<T> AsyncRepository<T>() where T : class
    {
        return new RepositoryBase<T>(_dbContext);
    }
    
    public Task<int> SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
    
    public IDocumentTypeRepository DocumentTypeRepository => _documentTypeRepository;
    public IDocumentTemplateRepository DocumentTemplateRepository => _documentTemplateRepository;
}