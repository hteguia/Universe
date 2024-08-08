using Domain.Features.ServiceRequests.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.Base;


namespace Domain.Interfaces.Repositories;

public interface IServiceRequestRepository : IAsyncRepository<ServiceRequest>
{

}