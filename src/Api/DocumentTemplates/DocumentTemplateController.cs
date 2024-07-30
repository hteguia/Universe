using Api.DocumentTemplates.Models;
using AutoMapper;
using Domain.Contracts;
using Domain.DocumentTemplates.Features;
using Domain.DocumentTypes.Features;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.DocumentTemplates;

[ApiController]
[Route("[controller]")]
public class DocumentTemplateController(IUnitOfWork unitOfWork, IFileRepository fileRepository, IMapper mapper) : ControllerBase
{
    private readonly CreateDocumentTemplate _createDocumentTemplate = new(unitOfWork, fileRepository);
    
    [HttpPost(Name = "CreateDocumentTemplate")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(AddDocumentTemplateResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateDocumentTemplate([FromForm] AddDocumentTemplateRequest model)
    {
        byte[] fileContent = null;
        using (var memoryStream = new MemoryStream())
        {
            await model.Content.CopyToAsync(memoryStream);
            fileContent = memoryStream.ToArray();
        }
        var result = await _createDocumentTemplate.Create(model.Name, fileContent);
        var response = mapper.Map<AddDocumentTemplateResponse>(result);
        return Ok(response);
    }
}