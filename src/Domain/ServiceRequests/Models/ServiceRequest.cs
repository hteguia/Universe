using System.Collections.ObjectModel;
using Domain.Base;
using Domain.DocumentTypes.Models;

namespace Domain.ServiceRequests.Models;

public class ServiceRequest : BaseEntity
{
    public string Name { get; set; }
    public string Path { get; internal set; }
    public string DeadLine { get; internal set; }
    public long DocumentTypeId { get; internal set; }
    public DocumentType DocumentType { get; internal set; }
    public DateTime CreateAt { get; internal set; }
    public ICollection<ServiceRequestStatus> ServiceRequestStatuses { get; internal set; }

    public ServiceRequest()
    {
        CreateAt = DateTime.UtcNow;
        ServiceRequestStatuses = new Collection<ServiceRequestStatus>();
    }

    public ServiceRequest(string name, string deadLine, long documentTypeId) : this()
    {
        this.Name = name;
        this.DeadLine = deadLine;
        this.DocumentTypeId = documentTypeId;
    }
    
    public void UpdatePath(string path)
    {
        this.Path = path;
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