using Domain.Base;
using Domain.ServiceRequests.Enums;

namespace Domain.Features.ServiceRequests.Entities;

public class ServiceRequestStatus : BaseEntity
{
    public ServiceRequest ServiceRequest { get; internal set; }
    public DateTime CreateAt { get; internal set; }
    public Status Status { get; internal set; }

    public ServiceRequestStatus()
    {
        
    }

    public ServiceRequestStatus(ServiceRequest serviceRequest, Status status)
    {
        ServiceRequest = serviceRequest;
        Status = status;
        CreateAt = DateTime.UtcNow;
    }
}