namespace Domain.Features.ServiceRequests.Models;

public class CreateServiceRequestVM
{
    public string Name { get; set; }
    public byte[] FileContent { get; set; }
    public string DeadLine { get; set; } 
    public long DocumentTypeId { get; set; }
}
