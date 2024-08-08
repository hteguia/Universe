using Domain.Features.DocumentTemplates.Entities;
using Domain.Features.DocumentTypes.Entities;
using Domain.Features.ServiceRequests.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<DocumentTemplate> DocumentTemplates { get; set; }
    public DbSet<ServiceRequest> ServiceRequests { get; set; }
    public DbSet<ServiceRequestStatus> ServiceRequestStatuses { get; set; }
}