namespace Domain.Features.SmsMarketings.UseCases.SendSms;

public record SendSmsUseCaseCommand(string PhoneNumber, string SenderName, string Message)
{
    public DateTime? scheduledDate;
    public  SendSmsUseCaseCommand(string phoneNumber, string senderName, string message, DateTime scheduledDate): this(phoneNumber, senderName, message)
    {
        this.scheduledDate = scheduledDate;
    }
}


