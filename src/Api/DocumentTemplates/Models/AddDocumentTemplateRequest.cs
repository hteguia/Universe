namespace Api.DocumentTemplates.Models;

public record AddDocumentTemplateRequest
{
    public string Name { get; init; }
    public IFormFile Content { get; init; }
}