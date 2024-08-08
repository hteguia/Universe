using Api.DocumentTypes.Models;
using AutoMapper;
using Domain.Features.DocumentTypes.UseCases;
using Domain.Features.DocumentTypes.UseCases.CreateDocumentType;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Mvc;

namespace Api.DocumentTypes;

[ApiController]
[Route("[controller]")]
public class DocumentTypeController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    private readonly CreateDocumentTypeUseCase _createDocumentTypeUseCase = new(unitOfWork);

    [HttpPost(Name = "CreateDocumentType")]
    [ProducesResponseType(typeof(AddDocumentTypeResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateDocumentType([FromBody] AddDocumentTypeRequest model)
    {
        var result = await _createDocumentTypeUseCase.Create(new CreateDocumentTypeUseCaseCommand(model.Name, model.Description));
        var response = mapper.Map<AddDocumentTypeResponse>(result);
        return Ok(response);
    }
}