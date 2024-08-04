using Domain.Features.DocumentTemplates.Repositories;
using Domain.Features.DocumentTypes.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<DocumentTemplate> DocumentTemplates { get; set; }
}