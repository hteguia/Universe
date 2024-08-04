namespace Api.DocumentTypes.Models;

public record AddDocumentTypeRequest
{
    public string Name { get; init; }
    public string Description { get; init; }
}