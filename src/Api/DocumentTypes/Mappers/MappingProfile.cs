using Api.Controllers.Models;
using AutoMapper;

namespace Api.Controllers.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.DocumentTypes.Models.DocumentType, AddDocumentTypeResponse>();
    }
}