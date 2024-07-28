using Domain.DocumentTypes;
using Domain.DocumentTypes.Features;
using Domain.DocumentTypes.Models;
using Domain.Exceptions;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Domain.UnitTests.DocumentTypes;

public class CreateDocumentTypeTests
{
    private CreateDocumentType _createDocumentType;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IDocumentTypeRepository> _documentRepositoryMock;
    
    public CreateDocumentTypeTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _documentRepositoryMock = new Mock<IDocumentTypeRepository>();
        _unitOfWorkMock.Setup(p => p.AsyncRepository<DocumentType>()).Returns(_documentRepositoryMock.Object);
        _createDocumentType = new CreateDocumentType(_unitOfWorkMock.Object);
    }
    
    [Fact]
    public async void execute_mustCreateDocumentTypeProperly()
    {
        const string name = "Analyse de donnée";
        const string description = "Description analyse de donnée";
        
        var documentType = await _createDocumentType.Create(name, description);

        _unitOfWorkMock.Verify(p => p.SaveChangesAsync(), Times.Once);
        _documentRepositoryMock.Verify(p=>p.AddAsync(It.IsAny<DocumentType>()), Times.Once);
        documentType.Name.Should().Be(name);
        documentType.Description.Should().Be(description);
    }

    [Fact]
    public async  void execute_mustThrowsValidationException_IfNameEmpty()
    {
        const string name = "";
        const string description = "Description analyse de donnée";
        
        var act = async () => { await _createDocumentType.Create(name, description); };
        
        await act.Should().ThrowAsync<ValidationException>();
    }
    
    [Fact]
    public async  void execute_mustThrowsValidationException_IfDescriptionEmpty()
    {
        const string name = "Analyse de donnée";
        const string description = "";
        
        var act = async () => { await _createDocumentType.Create(name, description); };
        
        await act.Should().ThrowAsync<ValidationException>();
    }
}