namespace Domain.Features.SmsMarketings.Models;

public class CreateSmsModel
{
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
    public string SenderName { get; set; }
    public bool IsScheduled { get; set; }
    public DateTime SentDate { get; set; }
    

}
