namespace Api.DocumentTypes.Models;

public record AddDocumentTypeResponse
{
    public long Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
}