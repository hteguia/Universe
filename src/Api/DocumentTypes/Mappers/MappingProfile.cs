using Api.DocumentTypes.Models;
using AutoMapper;
using Domain.Features.DocumentTypes.Repositories;

namespace Api.DocumentTypes.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DocumentType, AddDocumentTypeResponse>();
    }
}