using Domain.Contracts;
using Domain.Exceptions;
using Domain.Features.DocumentTypes.Repositories;
using Domain.Features.ServiceRequests.Models;
using Domain.Features.ServiceRequests.Validotors;
using Domain.Interfaces.Repositories.Base;
using Domain.ServiceRequests.Enums;
using Domain.ServiceRequests.Models;

namespace Domain.Features.ServiceRequests.UseCases;

public class CreateServiceRequest(IUnitOfWork unitOfWorkMock, IFileRepository fileRepository)
{
    public async Task<ServiceRequest> Create(string name, byte[] fileContent, string deadLine, long documentTypeId)
    {
        var repository = unitOfWorkMock.AsyncRepository<ServiceRequest>();
        var documentTypeRepository = unitOfWorkMock.AsyncRepository<DocumentType>();
        var serviceRequest = new ServiceRequest(name, deadLine, documentTypeId);
        ServiceRequestValidator typeValidator = new ServiceRequestValidator(documentTypeRepository as IDocumentTypeRepository);
        var validationResut = await typeValidator.ValidateAsync(serviceRequest);
        if (validationResut.Errors.Count > 0)
        {
            throw new ValidationException(validationResut);
        }     
        var path = fileRepository.SaveFile(name, fileContent);
        serviceRequest.UpdatePath(path);
        var serviceRequestStatus = new ServiceRequestStatus(serviceRequest, Status.WAITING_FOR_TREATMENT);
        serviceRequest.ServiceRequestStatuses.Add(serviceRequestStatus);

        await repository.AddAsync(serviceRequest);
        await unitOfWorkMock.SaveChangesAsync();
        return serviceRequest;
    }
}