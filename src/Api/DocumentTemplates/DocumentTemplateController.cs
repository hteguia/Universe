using Api.DocumentTemplates.Models;
using AutoMapper;
using Domain.Contracts;
using Domain.Features.DocumentTemplates.Models;
using Domain.Features.DocumentTemplates.UseCases;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.Base;
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
        var result = await _createDocumentTemplate.Create(new CreateDocumentTemplateModel()
        {
            Name = model.Name,
            FileContent = fileContent
        });
        var response = mapper.Map<AddDocumentTemplateResponse>(result);
        return Ok(response);
    }
}