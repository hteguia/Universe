using Domain.Contracts;
using Domain.Exceptions;
using Domain.Features.ServiceRequests.Entities;
using Domain.Features.ServiceRequests.Models;
using Domain.Features.ServiceRequests.Validotors;
using Domain.Interfaces.Repositories.Base;

namespace Domain.Features.ServiceRequests.UseCases;

public class CreateServiceRequest(IUnitOfWork unitOfWorkMock, IFileRepository fileRepository)
{
    public async Task<ServiceRequest> Create(CreateServiceRequestVM model)
    {
        var repository = unitOfWorkMock.AsyncRepository<ServiceRequest>();
        
        ServiceRequestValidator typeValidator = new(unitOfWorkMock.DocumentTypeRepository);
        var validationResut = await typeValidator.ValidateAsync(model);
        if (validationResut.Errors.Count > 0)
        {
            throw new ValidationException(validationResut);
        }     
        var path = fileRepository.SaveFile(model.Name, model.FileContent);
        var serviceRequest = new ServiceRequest(path, model.DeadLine, model.DocumentTypeId);

        await repository.AddAsync(serviceRequest);
        await unitOfWorkMock.SaveChangesAsync();
        return serviceRequest;
    }
}