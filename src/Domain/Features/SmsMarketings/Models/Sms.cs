using Domain.Base;
using Domain.Features.SmsMarketings.Enums;

namespace Domain.Features.SmsMarketings.Models;

public class Sms : BaseEntity
{
    private string PhoneNumber;
    private string SenderName;
    private string Message;
    private DateTime CreatedAt;
    private DateTime? ScheduledDate;
    private DateTime? SendingDate;
    private ICollection<SmsStatus> SmsStatuses;
    private int SendCount;
    
    public Sms()
    {

    }

    public Sms(string phoneNumber, string senderName, string message, DateTime currentDate) : this()
    {
        this.Update(phoneNumber, senderName, message, currentDate);   
    }
    
    public Sms(string phoneNumber, string senderName, string message, DateTime scheduledDate, DateTime currentDate) : this(phoneNumber, senderName, message, currentDate)
    {
        ScheduledDate = scheduledDate;
    }

    public void Send(DateTime currentDate)
    {
        if(SmsStatuses.Any(p=>p.Status == Status.SEND))
        {
            throw new InvalidOperationException("Sms is already sent");
        }
        
        if(ScheduledDate != null && ScheduledDate > DateTime.UtcNow)
        {
            throw new InvalidOperationException("Sms is scheduled for later");
        }

        ChangeStatus(Status.SEND);
        SendingDate = currentDate;
        SendCount = 1;
    }
    
    public void Resend()
    {
        if(SmsStatuses.All(p => p.Status != Status.SEND))
        {
            throw new InvalidOperationException("Sms is not sent yet");
        }

        ChangeStatus(Status.RESEND);
        SendCount++;
    }
    
    private void Update(string phoneNumber, string senderName, string message, DateTime currentDate)
    {
        if(string.IsNullOrEmpty(senderName))
        {
            throw new ArgumentException("SenderName must not be null or empty");
        }

        if(string.IsNullOrEmpty(message))
        {
            throw new ArgumentException("Message must not be null or empty");
        }

        PhoneNumber = phoneNumber;
        SenderName = senderName;
        Message = message;
        
        CreatedAt  = currentDate;
        SmsStatuses = [new SmsStatus(this, Status.PENDING)];
    }
    
    private void ChangeStatus(Status status)
    { 
        var st = new SmsStatus(this, status);

        foreach (var item in SmsStatuses)
        {
            item.EndAt ??= DateTime.UtcNow;
        }

        SmsStatuses.Add(st);
    }
    
    private Status CurrentStatus()
    {
        return SmsStatuses.Where(p=>p.EndAt == null).Select(p=>p.Status).FirstOrDefault();
    }
    
    public override string ToString()
    {
        return $"PhoneNumber: {PhoneNumber}, " +
               $"SenderName: {SenderName}, " +
               $"Message: {Message}, " +
               $"Status: {CurrentStatus()} " +
               $"CreatedAt: {CreatedAt:dd/MM/yyyy} " +
               $"ScheduledDate: {ScheduledDate:dd/MM/yyyy} " +
               $"SendingDate: {SendingDate:dd/MM/yyyy} " +
               $"sendCount: {SendCount}";
    }
}
