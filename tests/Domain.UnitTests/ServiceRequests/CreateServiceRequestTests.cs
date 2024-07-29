using System.Text;
using Domain.Contracts;
using Domain.DocumentTemplates;
using Domain.DocumentTypes;
using Domain.DocumentTypes.Models;
using Domain.Interfaces;
using Domain.ServiceRequests;
using Domain.ServiceRequests.Features;
using Domain.ServiceRequests.Models;
using FluentAssertions;
using Moq;

namespace Domain.UnitTests.ServiceRequest;

public class CreateServiceRequestTests
{
    private CreateServiceRequest _createServiceRequest;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IServiceRequestRepository> _ServiceRequestRepositoryMock;
    private Mock<IFileRepository> _fileRepositoryMock;
    private Mock<IDocumentTypeRepository> _documentRepositoryMock;
    
    public CreateServiceRequestTests()
    {
        _fileRepositoryMock = new Mock<IFileRepository>();
        _ServiceRequestRepositoryMock = new Mock<IServiceRequestRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _documentRepositoryMock = new Mock<IDocumentTypeRepository>();
    }

    [Fact]
    public async void execute_mustCreateDocumentTypeProperly()
    {
        const string path = @"c:\myfile.txt";
        const string name = "Document de thèse.pdf";
        const string strContent = "content";
        const string deadLine = "";
        const long documentTypeId = 1;
        byte[] fileContent = Encoding.UTF8.GetBytes(strContent);

        _fileRepositoryMock.Setup(p => p.SaveFile(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(path);
        _unitOfWorkMock.Setup(p => p.AsyncRepository<ServiceRequests.Models.ServiceRequest>())
            .Returns(_ServiceRequestRepositoryMock.Object);
        _unitOfWorkMock.Setup(p => p.AsyncRepository<DocumentType>()).Returns(_documentRepositoryMock.Object);
        _documentRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(new DocumentType());
        
        _createServiceRequest = new CreateServiceRequest(_unitOfWorkMock.Object, _fileRepositoryMock.Object);
        ServiceRequests.Models.ServiceRequest serviceRequest = await _createServiceRequest.Create(name, fileContent, deadLine, documentTypeId);

        serviceRequest.Path.Should().Be(path);
        serviceRequest.DeadLine.Should().Be(deadLine);
        serviceRequest.DocumentType.Should().NotBeNull();
    } 
}