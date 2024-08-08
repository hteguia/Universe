using System.Text;
using Domain.Contracts;
using Domain.Features.DocumentTypes;
using Domain.Features.DocumentTypes.Entities;
using Domain.Features.ServiceRequests;
using Domain.Features.ServiceRequests.Entities;
using Domain.Features.ServiceRequests.Models;
using Domain.Features.ServiceRequests.UseCases;
using Domain.Interfaces.Repositories.Base;
using Domain.ServiceRequests.Enums;
using FluentAssertions;
using Moq;

namespace Domain.UnitTests.ServiceRequests.UseCases;

public class CreateServiceRequestTests
{
    private readonly CreateServiceRequest _createServiceRequest;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IServiceRequestRepository> _serviceRequestRepositoryMock;
    private readonly Mock<IFileRepository> _fileRepositoryMock;
    private readonly Mock<IDocumentTypeRepository> _documentRepositoryMock;
    
    public CreateServiceRequestTests()
    {
        _fileRepositoryMock = new Mock<IFileRepository>();
        _serviceRequestRepositoryMock = new Mock<IServiceRequestRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _documentRepositoryMock = new Mock<IDocumentTypeRepository>();
        
        _unitOfWorkMock.Setup(p => p.AsyncRepository<ServiceRequest>()).Returns(_serviceRequestRepositoryMock.Object);
        _unitOfWorkMock.Setup(p => p.DocumentTypeRepository).Returns(_documentRepositoryMock.Object);
        
        _createServiceRequest = new CreateServiceRequest(_unitOfWorkMock.Object, _fileRepositoryMock.Object);
    }

    [Fact]
    public async void CreateServiceRequest_WithValidData_ShouldCreateServiceRequest()
    {
        //Arrange
        _fileRepositoryMock.Setup(p => p.SaveFile(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(@"C:\Document de thèse.pdf");
        _documentRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new DocumentType(1,"Document de thèse", "Description"));
        
        //Act
        var serviceRequest = await _createServiceRequest.Create(new CreateServiceRequestVM()
        {
            Name = "Document de thèse.pdf",
            DeadLine = "7 Jours",
            DocumentTypeId = 1,
            FileContent = Encoding.UTF8.GetBytes("content")
        });
        
        //Assert
        _unitOfWorkMock.Verify(p => p.SaveChangesAsync(), Times.Once);
        _serviceRequestRepositoryMock.Verify(p=>p.AddAsync(It.IsAny<ServiceRequest>()), Times.Once);
        
        serviceRequest.Should().NotBeNull();
        serviceRequest.ServiceRequestStatuses.Should().NotBeNullOrEmpty();
        serviceRequest.DocumentTypeId.Should().Be(1);
        serviceRequest.ServiceRequestStatuses.First().Status.Should().Be(Status.WAITING_FOR_TREATMENT);
        serviceRequest.DeadLine.Should().Be("7 Jours");
        serviceRequest.CreateAt.Should().NotBe(default); 
        serviceRequest.Path.Should().NotBeNullOrEmpty();
    }
    
    [Fact]
    public async void CreateServiceRequest_shouldThrowValidationException_WhenDocumentTypeNotExist()
    {
        //Arrange
        _documentRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((DocumentType?)null);
        
        //Act
        var act = async () => { await _createServiceRequest.Create(new CreateServiceRequestVM()
        {
            Name = "Document de thèse.pdf",
            DeadLine = "7 Jours",
            DocumentTypeId = 1,
            FileContent = Encoding.UTF8.GetBytes("content")
        }); };
        
        //Assert
        await act.Should().ThrowAsync<Domain.Exceptions.ValidationException>();
    }
}