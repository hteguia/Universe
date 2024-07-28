using Domain.Contrats;
using Domain.DocumentTemplates;
using Domain.DocumentTypes.Models;
using Domain.Interfaces;
using Domain.ServiceRequests.Models;

namespace Domain.ServiceRequests.Features;

public class CreateServiceRequest(IUnitOfWork unitOfWorkMock, IFileRepository fileRepository)
{
    public async Task<ServiceRequest> Create(string name, byte[] fileContent, string deadLine, long documentTypeId)
    {
        var repository = unitOfWorkMock.AsyncRepository<ServiceRequest>();
        var documentTypeRepository = unitOfWorkMock.AsyncRepository<DocumentType>();

        var documentType = await documentTypeRepository.GetByIdAsync(documentTypeId);
        var path = fileRepository.SaveFile(name, fileContent);
        var serviceRequest = new ServiceRequest(path, deadLine, documentType);

        await repository.AddAsync(serviceRequest);
        await unitOfWorkMock.SaveChangesAsync();
        return serviceRequest;
    }
}