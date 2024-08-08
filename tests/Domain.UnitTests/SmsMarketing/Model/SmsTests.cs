using Domain.Features.SmsMarketings.Models;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Domain.UnitTests.SmsMarketing.Model;

public class SmsTests
{
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;
    public SmsTests()
    {
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        _dateTimeProviderMock.Setup(p => p.UtcNow()).Returns(new DateTime(2024, 12, 31));
    }
    
    [Fact]
    public void CreateSms_WithValidData_ShouldCreateSmsProperly()
    {
        // Arrange
        const string phoneNumber = "237679799607";
        const string senderName = "BMR-AFRICA";
        const string message = "Hello";
        DateTime currentDate = _dateTimeProviderMock.Object.UtcNow();
        
        //Act
        var sms = new Sms(phoneNumber, senderName, message,  _dateTimeProviderMock.Object.UtcNow());

        // Assert
        string expectedSnapshot =  $"PhoneNumber: {phoneNumber}, " +
               $"SenderName: {senderName}, " +
               $"Message: {message}, " +
               $"Status: PENDING " +
               $"CreatedAt: 31/12/2024 " +
               $"ScheduledDate:  " +
               $"SendingDate:  " +
               $"sendCount: 0";
        sms.ToString().Should().Be(expectedSnapshot);
    }
    
    [Fact]
    public void CreateSms_WithScheduledDate_ShouldCreateSmsProperly()
    {
        // Arrange
        const string phoneNumber = "237679799607";
        const string senderName = "BMR-AFRICA";
        const string message = "Hello";
        DateTime currentDate = _dateTimeProviderMock.Object.UtcNow();
        
        //Act
        var sms = new Sms(phoneNumber, senderName, message, new DateTime(2025, 12, 31), _dateTimeProviderMock.Object.UtcNow());

        // Assert
        string expectedSnapshot =  $"PhoneNumber: {phoneNumber}, " +
               $"SenderName: {senderName}, " +
               $"Message: {message}, " +
               $"Status: PENDING " +
               $"CreatedAt: 31/12/2024 " +
               $"ScheduledDate: 31/12/2025 " +
               $"SendingDate:  " +
               $"sendCount: 0";
        sms.ToString().Should().Be(expectedSnapshot);
    }
    
    [Fact]
    public void Sms_Send_ShouldSendSms()
    {
        // Arrange
        var sms = new Sms("237679799607", "BMR-AFRICA", "Hello", _dateTimeProviderMock.Object.UtcNow());
        
        //Act
        sms.Send(_dateTimeProviderMock.Object.UtcNow());
        
        //Assert
        string expectedSnapshot =  $"PhoneNumber: 237679799607, " +
                                   $"SenderName: BMR-AFRICA, " +
                                   $"Message: Hello, " +
                                   $"Status: SEND " +
                                   $"CreatedAt: 31/12/2024 " +
                                   $"ScheduledDate:  " +
                                   $"SendingDate: 31/12/2024 " +
                                   $"sendCount: 1";
        sms.ToString().Should().Be(expectedSnapshot);
    }
    
    [Fact]
    public void Sms_Send_ShouldThrowInvalidOperationException_WhenSmsIsAlreadySent()
    {
        // Arrange
        var sms = new Sms("237679799607", "BMR-AFRICA", "Hello", _dateTimeProviderMock.Object.UtcNow());
        sms.Send(_dateTimeProviderMock.Object.UtcNow());
        
        //Act
        var act = () => sms.Send(_dateTimeProviderMock.Object.UtcNow());
        
        //Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Sms is already sent");
    }
    
    [Fact]
    public void Sms_Send_ShouldThrowInvalidOperationException_WhenSmsIsScheduledForLater()
    {
        // Arrange
        var sms = new Sms("237679799607", "BMR-AFRICA", "Hello", new DateTime(2025, 12, 31), _dateTimeProviderMock.Object.UtcNow());
        
        //Act
        var act = () => sms.Send(_dateTimeProviderMock.Object.UtcNow());
        
        //Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Sms is scheduled for later");
    }
    
    [Fact]
    public void Sms_Resend_ShouldResendSms()
    {
        // Arrange
        var sms = new Sms("237679799607", "BMR-AFRICA", "Hello", _dateTimeProviderMock.Object.UtcNow());
        sms.Send(_dateTimeProviderMock.Object.UtcNow());
        
        //Act
        sms.Resend();
        
        //Assert
        string expectedSnapshot =  $"PhoneNumber: 237679799607, " +
                                   $"SenderName: BMR-AFRICA, " +
                                   $"Message: Hello, " +
                                   $"Status: RESEND " +
                                   $"CreatedAt: 31/12/2024 " +
                                   $"ScheduledDate:  " +
                                   $"SendingDate: 31/12/2024 " +
                                   $"sendCount: 2";
        sms.ToString().Should().Be(expectedSnapshot);
    }
    
    [Fact]
    public void Sms_Resend_ShouldThrowInvalidOperationException_WhenSmsIsNotSentYet()
    {
        // Arrange
        var sms = new Sms("237679799607", "BMR-AFRICA", "Hello", _dateTimeProviderMock.Object.UtcNow());
        
        //Act
        var act = () => sms.Resend();
        
        //Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Sms is not sent yet");
    }
}