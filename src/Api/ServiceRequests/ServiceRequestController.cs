using Api.DocumentTemplates.Models;
using Api.ServiceRequests.Models;
using AutoMapper;
using Domain.Contracts;
using Domain.Features.ServiceRequests.Models;
using Domain.Features.ServiceRequests.UseCases;
using Domain.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Mvc;

namespace Api.ServiceRequests;

[ApiController]
[Route("[controller]")]
public class ServiceRequestController(IUnitOfWork unitOfWork, IFileRepository fileRepository, IMapper mapper) : ControllerBase
{

    private readonly CreateServiceRequest _createServiceRequest = new(unitOfWork, fileRepository);

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
        var result = await _createServiceRequest.Create(new CreateServiceRequestVM()
        {
            Name = model.Name,
            DeadLine = model.DeadLine,
            DocumentTypeId = model.DocumentTypeId,
            FileContent = fileContent
        });
        var response = mapper.Map<AddServiceRequestResponse>(result);
        return Ok(response);
    }
}
