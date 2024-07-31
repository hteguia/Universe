using System.Collections.ObjectModel;
using Domain.Base;
using Domain.DocumentTypes.Models;

namespace Domain.ServiceRequests.Models;

public class ServiceRequest : BaseEntity
{
    public string Path { get; internal set; }
    public string DeadLine { get; internal set; }
    public DocumentType DocumentType { get; internal set; }
    public DateTime CreateAt { get; internal set; }
    public ICollection<ServiceRequestStatus> ServiceRequestStatuses { get; internal set; }

    public ServiceRequest()
    {
        CreateAt = DateTime.UtcNow;
        ServiceRequestStatuses = new Collection<ServiceRequestStatus>();
    }

    public ServiceRequest(string path, string deadLine, DocumentType documentType) : this()
    {
        this.Update(path, deadLine, documentType);
    }

    public void Update(string path, string deadLine, DocumentType documentType)
    {
        this.Path = path;
        this.DeadLine = deadLine;
        this.DocumentType = documentType;
    }
}