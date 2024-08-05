using Domain.Base;
using Domain.Features.DocumentTypes.Entities;
using Domain.ServiceRequests.Enums;


namespace Domain.Features.ServiceRequests.Entities;

public class ServiceRequest : BaseEntity
{
    public string Path { get; internal set; }
    public string DeadLine { get; internal set; }
    public long DocumentTypeId { get; internal set; }
    public DocumentType DocumentType { get; internal set; }
    public DateTime CreateAt { get; internal set; }
    public ICollection<ServiceRequestStatus> ServiceRequestStatuses { get; internal set; }

    public ServiceRequest()
    {
        CreateAt = DateTime.UtcNow;
        ServiceRequestStatuses = [new ServiceRequestStatus(this, Status.WAITING_FOR_TREATMENT)];
    }

    public ServiceRequest(string path, string deadLine, long documentTypeId) : this()
    {
        this.Update(path, deadLine, documentTypeId);
    }

    private void Update(string path, string deadLine, long documentTypeId)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Path must not be null or empty");
        }

        if (string.IsNullOrWhiteSpace(deadLine))
        {
            throw new ArgumentException("DeadLine must not be null or empty");
        }

        if (documentTypeId <= 0)
        {
            throw new ArgumentException("DocumentTypeId must be greater than 0");
        }

        Path = path;
        DeadLine = deadLine;
        DocumentTypeId = documentTypeId;
    }
}