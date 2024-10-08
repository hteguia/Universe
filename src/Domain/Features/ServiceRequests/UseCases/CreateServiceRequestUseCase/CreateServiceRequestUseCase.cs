﻿using Domain.Contracts;
using Domain.Exceptions;
using Domain.Features.ServiceRequests.Entities;
using Domain.Interfaces.Repositories.Base;

namespace Domain.Features.ServiceRequests.UseCases.CreateServiceRequestUseCase;

public class CreateServiceRequestUseCase(IUnitOfWork unitOfWorkMock, IFileRepository fileRepository)
{
    public async Task<ServiceRequest> Create(CreateServiceRequestUseCaseCommand model)
    {
        var repository = unitOfWorkMock.AsyncRepository<ServiceRequest>();
        
        CreateServiceRequestUseCaseValidator typeUseCaseValidator = new(unitOfWorkMock.DocumentTypeRepository);
        var validationResut = await typeUseCaseValidator.ValidateAsync(model);
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