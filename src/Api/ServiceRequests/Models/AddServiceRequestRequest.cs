namespace Api.ServiceRequests.Models;

public class AddServiceRequestRequest
{
    public string Name { get; init; }
    public IFormFile Content { get; init; }
    public long DocumentTypeId { get; set; }
    public string DeadLine { get; set; }
}
