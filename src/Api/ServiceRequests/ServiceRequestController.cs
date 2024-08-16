using Api.ServiceRequests.Models;
using AutoMapper;
using Domain.Features.ServiceRequests.UseCases.CreateServiceRequestUseCase;
using Domain.Interfaces;
using Domain.Interfaces.Providers;
using Domain.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Mvc;

namespace Api.ServiceRequests;

[ApiController]
[Route("[controller]")]
public class ServiceRequestController(IUnitOfWork unitOfWork, IFileProvider fileRepository, IDateTimeProvider dateTimeProvider, IMapper mapper) : ControllerBase
{

    private readonly CreateServiceRequestUseCase _createServiceRequestUseCase = new(unitOfWork, fileRepository, dateTimeProvider);

    [HttpPost(Name = "CreateServiceRequest")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(AddServiceRequestResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateServiceRequest([FromForm] AddServiceRequestRequest model)
    {
        byte[] fileContent = Array.Empty<byte>();
        using (var memoryStream = new MemoryStream())
        {
            await model.Content.CopyToAsync(memoryStream);
            fileContent = memoryStream.ToArray();
        }
        var result = await _createServiceRequestUseCase.Create(new CreateServiceRequestUseCaseCommand(model.Name, fileContent, model.DeadLine, model.DocumentTypeId));
        var response = mapper.Map<AddServiceRequestResponse>(result);
        return Ok(response);
    }
}
