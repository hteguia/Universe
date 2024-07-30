using Api.DocumentTemplates.Models;
using AutoMapper;

namespace Api.DocumentTemplates.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.DocumentTemplates.Models.DocumentTemplate, AddDocumentTemplateResponse>();
    }
}