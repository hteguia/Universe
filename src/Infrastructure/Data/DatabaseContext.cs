using Domain.DocumentTemplates.Models;
using Domain.DocumentTypes.Models;
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