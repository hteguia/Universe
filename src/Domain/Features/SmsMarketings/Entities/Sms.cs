using Domain.Base;
using Domain.Features.SmsMarketings.Enums;

namespace Domain.Features.SmsMarketings.Entities;

public class Sms : BaseEntity
{
    public string PhoneNumber { get; internal set; }
    public string SenderName { get; internal set; }
    public string Message { get; internal set; }
    public DateTime CreatedAt { get; internal set; }
    public bool IsScheduled { get; set; }
    public DateTime SentDate { get; set; }
    public ICollection<SmsStatus> SmsStatuses { get; internal set; }

    public Sms()
    {
       CreatedAt = SentDate = DateTime.UtcNow;
       SmsStatuses = [new SmsStatus(this, Status.PENDING)];
    }

    public Sms(string senderName, string message) : this()
    {
        this.Update(senderName, message);   
    }

    private void Update(string senderName, string message)
    {
        if(string.IsNullOrEmpty(senderName))
        {
            throw new ArgumentException("SenderName must not be null or empty");
        }

        if(string.IsNullOrEmpty(message))
        {
            throw new ArgumentException("Message must not be null or empty");
        }

        SenderName = senderName;
        Message = message;
    }

    public void AddStatus(Status status)
    {
        var st = new SmsStatus(this, status);

        foreach (var item in SmsStatuses)
        {
            item.EndAt ??= DateTime.UtcNow;
        }

        SmsStatuses.Add(st);
    }
}
