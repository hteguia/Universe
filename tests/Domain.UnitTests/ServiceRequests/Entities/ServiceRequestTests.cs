using Domain.Features.ServiceRequests.Entities;
using FluentAssertions;

namespace Domain.UnitTests.ServiceRequests.Entities;

public class ServiceRequestTests
{
    [Fact]
    public void ServiceRequest_WithValidData_ShouldCreateServiceRequest()
    {
        //Act
        var serviceRequest = new ServiceRequest("C:\\Document de thèse.pdf", "7 Jours", 1);

        
        //Assert
        serviceRequest.Path.Should().Be("C:\\Document de thèse.pdf");
        serviceRequest.DeadLine.Should().Be("7 Jours");
        serviceRequest.DocumentTypeId.Should().Be(1);
        serviceRequest.CreateAt.Should().NotBe(default);
        serviceRequest.ServiceRequestStatuses.Should().NotBeNull();
        serviceRequest.ServiceRequestStatuses.First().Status.Should().Be(Domain.ServiceRequests.Enums.Status.WAITING_FOR_TREATMENT);
    }

    [Fact]
    public void ServiceRequest_WithEmptyPath_ShouldThrowArgumentException()
    {
        //Act
        var act = () => new ServiceRequest(string.Empty, "7 Jours", 1);

        //Assert
        act.Should().Throw<ArgumentException>().WithMessage("Path must not be null or empty");
    }

    [Fact]
    public void ServiceRequest_WithEmptyDeadLine_ShouldThrowArgumentException()
    {
        //Act
        var act = () => new ServiceRequest("C:\\Document de thèse.pdf", string.Empty, 1);

        //Assert
        act.Should().Throw<ArgumentException>().WithMessage("DeadLine must not be null or empty");
    }

    [Fact]
    public void ServiceRequest_WithInvalidDocumentTypeId_ShouldThrowArgumentException()
    {
        //Act
        var act = () => new ServiceRequest("C:\\Document de thèse.pdf", "7 Jours", 0);

        //Assert
        act.Should().Throw<ArgumentException>().WithMessage("DocumentTypeId must be greater than 0");
    }

}
