using Domain.Interfaces;
using Domain.ServiceRequests.Models;

namespace Domain.ServiceRequests;

public interface IServiceRequestRepository : IAsyncRepository<ServiceRequest>
{
    
}