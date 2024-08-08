namespace Domain.Features.DocumentTemplates.Models;

public class CreateDocumentTemplateModel
{
    public string Name { get; set; }
    public byte[] FileContent { get; set; }
}