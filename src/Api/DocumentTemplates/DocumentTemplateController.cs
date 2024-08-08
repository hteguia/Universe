using Api.DocumentTemplates.Models;
using AutoMapper;
using Domain.Contracts;
using Domain.Features.DocumentTemplates.UseCases.CreateDocumentTemplate;
using Domain.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Mvc;

namespace Api.DocumentTemplates;

[ApiController]
[Route("[controller]")]
public class DocumentTemplateController(IUnitOfWork unitOfWork, IFileRepository fileRepository, IMapper mapper) : ControllerBase
{
    private readonly CreateDocumentTemplateUseCase _createDocumentTemplateUseCase = new(unitOfWork, fileRepository);
    
    [HttpPost(Name = "CreateDocumentTemplate")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(AddDocumentTemplateResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateDocumentTemplate([FromForm] AddDocumentTemplateRequest model)
    {
        byte[] fileContent = Array.Empty<byte>();
        using (var memoryStream = new MemoryStream())
        {
            await model.Content.CopyToAsync(memoryStream);
            fileContent = memoryStream.ToArray();
        }
        var result = await _createDocumentTemplateUseCase.Create(new CreateDocumentTemplateUseCaseCommand(model.Name, fileContent));
        var response = mapper.Map<AddDocumentTemplateResponse>(result);
        return Ok(response);
    }
}