using System.IO.Abstractions;
using System.Reflection;
using Api.Extensions;
using Domain.Contracts;
using Domain.Features.DocumentTemplates;
using Domain.Features.DocumentTypes;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.Base;
using Infrastructure.Contracts;
using Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services
    .AddDatabase(builder.Configuration)
    .AddUnitOfWork();

builder.Services.AddScoped<IFileSystem, FileSystem>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
builder.Services.AddScoped<IDocumentTemplateRepository, DocumentTemplateRepository>();
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DefaultModelExpandDepth(-1);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }