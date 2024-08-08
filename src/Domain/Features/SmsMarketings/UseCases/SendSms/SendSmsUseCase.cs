using Domain.Features.SmsMarketings.Models;
using Domain.Interfaces;
using Domain.Interfaces.Providers;
using Domain.Interfaces.Repositories.Base;

namespace Domain.Features.SmsMarketings.UseCases.SendSms;

public class SendSmsUseCase(IUnitOfWork unitOfWork, ISmsProvider smsProvider, IDateTimeProvider dateTimeProvider)
{
    public async Task Send(SendSmsUseCaseCommand command)
    {
        var repository = unitOfWork.AsyncRepository<Sms>();
        var sms = new Sms(command.PhoneNumber, command.SenderName, command.Message, dateTimeProvider.UtcNow());
        await smsProvider.Send(command.PhoneNumber, command.Message);
        sms.Send(dateTimeProvider.UtcNow());
        await repository.AddAsync(sms);
        await unitOfWork.SaveChangesAsync();
    }
}
