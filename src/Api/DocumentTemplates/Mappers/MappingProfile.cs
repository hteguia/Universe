using Api.DocumentTemplates.Models;
using AutoMapper;
using Domain.Features.DocumentTemplates.Repositories;

namespace Api.DocumentTemplates.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DocumentTemplate, AddDocumentTemplateResponse>();
    }
}