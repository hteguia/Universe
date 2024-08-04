using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.IntegrationTests.Utilities;

public class WebFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureServices(services =>
        {
            var oldDatabaseContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DatabaseContext>));
            if(oldDatabaseContext != null)
            {
                services.Remove(oldDatabaseContext);
            }
            services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("InMemoryDbForTesting"));
        });
    }
}