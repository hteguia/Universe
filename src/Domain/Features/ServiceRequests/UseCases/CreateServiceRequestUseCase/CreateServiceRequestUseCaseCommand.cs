namespace Domain.Features.ServiceRequests.UseCases.CreateServiceRequestUseCase;

public record CreateServiceRequestUseCaseCommand(string Name, byte[] FileContent, string DeadLine, long DocumentTypeId);
