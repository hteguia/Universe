using Domain.Features.SmsMarketings.Enums;

namespace Domain.Features.SmsMarketings.Models;

public class SmsStatus
{
    public Sms Sms { get; internal set; }
    public Status Status { get; internal set; }
    public DateTime CreatedAt { get; internal set; }
    public DateTime? EndAt { get; internal set; }

    public SmsStatus()
    {
        
    }

    public SmsStatus(Sms sms, Status status)
    {
        Sms = sms;
        Status = status;
        CreatedAt = DateTime.UtcNow;
        EndAt = null;
    }
}
