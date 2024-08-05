using Domain.Features.ServiceRequests.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.Base;


namespace Domain.Features.ServiceRequests;

public interface IServiceRequestRepository : IAsyncRepository<ServiceRequest>
{

}