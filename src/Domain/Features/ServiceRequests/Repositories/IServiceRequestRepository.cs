using Domain.Features.ServiceRequests.Models;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.Base;


namespace Domain.Features.ServiceRequests.Repositories;

public interface IServiceRequestRepository : IAsyncRepository<ServiceRequest>
{
    
}