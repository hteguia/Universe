using Domain.ServiceRequests.Enums;

namespace Domain.ServiceRequests.Models;

public class ServiceRequestStatus
{
    public ServiceRequest ServiceRequest { get; internal set; }
    public DateTime CreateAt { get; internal set; }
    public Status Status { get; internal set; }

    public ServiceRequestStatus(ServiceRequest serviceRequest, Status status)
    {
        ServiceRequest = serviceRequest;
        Status = status;
        CreateAt = DateTime.UtcNow;
    }
}