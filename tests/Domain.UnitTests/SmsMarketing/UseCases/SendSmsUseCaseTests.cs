using Domain.Features.SmsMarketings.Models;
using Domain.Features.SmsMarketings.Repositories;
using Domain.Features.SmsMarketings.UseCases.SendSms;
using Domain.Interfaces;
using Domain.Interfaces.Providers;
using Domain.Interfaces.Repositories.Base;
using Moq;

namespace Domain.UnitTests.SmsMarketing.UseCases;

public class SendSmsUseCaseTests
{
    private readonly SendSmsUseCase _sendSmsUseCase;
    private readonly Mock<ISmsRepository> _smsRepositoryMock;
    private readonly Mock<ISmsProvider> _smsProviderMock;
    private readonly Mock<IUnitOfWork> _unitOfworkMock;
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;

    public SendSmsUseCaseTests()
    {
        _smsRepositoryMock = new Mock<ISmsRepository>();
        _smsProviderMock = new Mock<ISmsProvider>();
        _unitOfworkMock = new Mock<IUnitOfWork>();
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        _unitOfworkMock.Setup(p=>p.AsyncRepository<Sms>()).Returns(_smsRepositoryMock.Object);
        _sendSmsUseCase = new SendSmsUseCase(_unitOfworkMock.Object, _smsProviderMock.Object, _dateTimeProviderMock.Object);
    }
    
    [Fact]
    public async void SendSms_WithValidData_ShouldSendSms()
    {
        // Arrange
        const string phoneNumber = "237679799607";
        const string senderName = "BMR-AFRICA";
        const string message = "Hello";
        var sendSmsCommand = new SendSmsUseCaseCommand(phoneNumber, senderName, message);
        
        //Act
        await _sendSmsUseCase.Send(sendSmsCommand);

        // Assert
        _smsRepositoryMock.Verify(p => p.AddAsync(It.IsAny<Sms>()), Times.Once);
        _unitOfworkMock.Verify(p => p.SaveChangesAsync(), Times.Once);
        _smsProviderMock.Verify(p=>p.Send(It.IsAny<string>(), It.IsAny<string>()));
    }
}
