using Domain.Features.DocumentTypes.Models;
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

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<DatabaseContext>();

                db.Database.EnsureCreated();

                try
                {
                    Console.WriteLine("Seeding...");
                    db.DocumentTypes.Add(new DocumentType("Analyse de donnée", "Description analyse de donnée")); // Clear the table
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        });
    }


}