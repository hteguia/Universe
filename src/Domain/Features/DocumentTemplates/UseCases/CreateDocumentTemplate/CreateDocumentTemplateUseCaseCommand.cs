namespace Domain.Features.DocumentTemplates.UseCases.CreateDocumentTemplate;

public record CreateDocumentTemplateUseCaseCommand(string Name, byte[] FileContent);