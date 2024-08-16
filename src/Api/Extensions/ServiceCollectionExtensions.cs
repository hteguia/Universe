using Domain.Interfaces;
using Domain.Interfaces.Providers;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.Base;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Providers;
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


    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
        services.AddScoped<IDocumentTemplateRepository, DocumentTemplateRepository>();

        return services;
    }

    public static IServiceCollection AddProviders(this IServiceCollection services)
    {
        services.AddScoped<IFileProvider, FileProvider>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}