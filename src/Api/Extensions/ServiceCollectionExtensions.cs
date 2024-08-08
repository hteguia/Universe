using Domain.Interfaces;
using Domain.Interfaces.Repositories.Base;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        return services
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }
    
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DBConnectionString")));
    }
}