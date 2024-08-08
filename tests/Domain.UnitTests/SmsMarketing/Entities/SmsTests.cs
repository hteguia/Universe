using Domain.Features.SmsMarketings.Entities;
using FluentAssertions;

namespace Domain.UnitTests.SmsMarketing.Entities;

public  class SmsTests
{
    [Fact]
    public void Sms_WithValidData_ShouldCreateSms()
    {
        //Act
        var sms = new Sms("Sender", "Message");
        
        //Assert
        sms.Should().NotBeNull();
        sms.SenderName.Should().Be("Sender");
        sms.Message.Should().Be("Message");
        sms.CreatedAt.Should().NotBe(default);
        sms.SmsStatuses.Should().NotBeNull();
        sms.SmsStatuses.First().Status.Should().Be(Domain.Features.SmsMarketings.Enums.Status.PENDING);
    }

    [Fact]
    public void Sms_WithEmptySenderName_ShouldThrowArgumentException()
    {
        //Act
        var act = () => new Sms(string.Empty, "Message");

        //Assert
        act.Should().Throw<ArgumentException>().WithMessage("SenderName must not be null or empty");
    }

    [Fact]
    public void Sms_WithEmptyMessage_ShouldThrowArgumentException()
    {
        //Act
        var act = () => new Sms("Sender", string.Empty);

        //Assert
        act.Should().Throw<ArgumentException>().WithMessage("Message must not be null or empty");
    }

    [Fact]
    public void Sms_AddSmsStatus_WithValidData_ShouldAddSmsStatus()
    {
        //Arrange
        var sms = new Sms("Sender", "Message");

        //Act
        sms.AddStatus(Domain.Features.SmsMarketings.Enums.Status.SEND);

        //Assert
        sms.SmsStatuses.Should().NotBeNull();
        sms.SmsStatuses.Count.Should().Be(2);
        sms.SmsStatuses.Last().Status.Should().Be(Domain.Features.SmsMarketings.Enums.Status.SEND);
        sms.SmsStatuses.First().EndAt.Should().NotBeNull();
    }
}
