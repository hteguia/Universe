using Api.Controllers.Models;
using AutoMapper;
using Domain.DocumentTypes.Features;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentTypeController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private readonly CreateDocumentType _createDocumentType = new(unitOfWork);

    [HttpPost(Name = "CreateDocumentType")]
    [ProducesResponseType(typeof(AddDocumentTypeResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateDocumentType([FromBody] AddDocumentTypeRequest model)
    {
        var result = await _createDocumentType.Create(model.Name, model.Description);
        var response = mapper.Map<AddDocumentTypeResponse>(result);
        return Ok(response);
    }
}