using Api.ServiceRequests.Models;
using AutoMapper;
using Domain.Features.ServiceRequests.Entities;

namespace Api.ServiceRequests.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ServiceRequest, AddServiceRequestResponse>();
    }
}
