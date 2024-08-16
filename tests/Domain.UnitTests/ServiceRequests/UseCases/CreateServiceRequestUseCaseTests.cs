using System.Text;
using Domain.Features.DocumentTypes.Models;
using Domain.Features.ServiceRequests;
using Domain.Features.ServiceRequests.Entities;
using Domain.Features.ServiceRequests.UseCases.CreateServiceRequestUseCase;
using Domain.Interfaces;
using Domain.Interfaces.Providers;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.Base;
using Domain.ServiceRequests.Enums;
using FluentAssertions;
using Moq;

namespace Domain.UnitTests.ServiceRequests.UseCases;

public class CreateServiceRequestUseCaseTests
{
    private readonly CreateServiceRequestUseCase _createServiceRequestUseCase;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IServiceRequestRepository> _serviceRequestRepositoryMock;
    private readonly Mock<IFileProvider> _fileRepositoryMock;
    private readonly Mock<IDocumentTypeRepository> _documentRepositoryMock;
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;
    private readonly DateTime CurrentDate = new(2024, 12, 31);

    public CreateServiceRequestUseCaseTests()
    {
        _fileRepositoryMock = new Mock<IFileProvider>();
        _serviceRequestRepositoryMock = new Mock<IServiceRequestRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _documentRepositoryMock = new Mock<IDocumentTypeRepository>();
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        _dateTimeProviderMock.Setup(p => p.UtcNow).Returns(CurrentDate);

        _unitOfWorkMock.Setup(p => p.AsyncRepository<ServiceRequest>()).Returns(_serviceRequestRepositoryMock.Object);
        _unitOfWorkMock.Setup(p => p.DocumentTypeRepository).Returns(_documentRepositoryMock.Object);
        
        _createServiceRequestUseCase = new CreateServiceRequestUseCase(_unitOfWorkMock.Object, _fileRepositoryMock.Object, _dateTimeProviderMock.Object);
    }

    [Fact]
    public async void CreateServiceRequest_WithValidData_ShouldCreateServiceRequest()
    {
        //Arrange
        _fileRepositoryMock.Setup(p => p.SaveFile(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(@"C:\Document de thèse.pdf");
        _documentRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new DocumentType(1,"Document de thèse", "Description"));
        
        //Act
        var serviceRequest = await _createServiceRequestUseCase.Create(new CreateServiceRequestUseCaseCommand("Document de thèse.pdf", Encoding.UTF8.GetBytes("content"), "7 Jours",1));
        
        //Assert
        _unitOfWorkMock.Verify(p => p.SaveChangesAsync(), Times.Once);
        _serviceRequestRepositoryMock.Verify(p=>p.AddAsync(It.IsAny<ServiceRequest>()), Times.Once);
        
        serviceRequest.Should().NotBeNull();
        serviceRequest.ServiceRequestStatuses.Should().NotBeNullOrEmpty();
        serviceRequest.DocumentTypeId.Should().Be(1);
        serviceRequest.ServiceRequestStatuses.First().Status.Should().Be(Status.WAITING_FOR_TREATMENT);
        serviceRequest.DeadLine.Should().Be("7 Jours");
        serviceRequest.CreateAt.Should().Be(CurrentDate); 
        serviceRequest.Path.Should().NotBeNullOrEmpty();
    }
    
    [Fact]
    public async void CreateServiceRequest_shouldThrowValidationException_WhenDocumentTypeNotExist()
    {
        //Arrange
        _documentRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((DocumentType?)null);
        
        //Act
        var act = async () => { await _createServiceRequestUseCase.Create(new CreateServiceRequestUseCaseCommand("Document de thèse.pdf", Encoding.UTF8.GetBytes("content"), "7 Jours", 1 )); };
        
        //Assert
        await act.Should().ThrowAsync<Domain.Exceptions.ValidationException>();
    }
}