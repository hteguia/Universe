using Api.DocumentTemplates.Models;
using AutoMapper;
using Domain.Features.DocumentTemplates.Entities;

namespace Api.DocumentTemplates.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DocumentTemplate, AddDocumentTemplateResponse>();
    }
}