namespace Api.DocumentTemplates.Models;

public record AddDocumentTemplateResponse
{
    public long Id { get; init; }
    public string Name { get; init; }
    public string Path { get; init; }
}