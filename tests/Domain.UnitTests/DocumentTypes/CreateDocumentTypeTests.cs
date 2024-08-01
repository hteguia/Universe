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
    private readonly CreateDocumentType _createDocumentType;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IDocumentTypeRepository> _documentRepositoryMock;
    
    public CreateDocumentTypeTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _documentRepositoryMock = new Mock<IDocumentTypeRepository>();
        _unitOfWorkMock.Setup(p => p.AsyncRepository<DocumentType>()).Returns(_documentRepositoryMock.Object);
        _createDocumentType = new CreateDocumentType(_unitOfWorkMock.Object);
    }
    
    [Fact]
    public async void execute_shouldCreateDocumentTypeProperly()
    {
        //Arrange
        const string name = "Analyse de donnée";
        const string description = "Description analyse de donnée";
        
        //Act
        var documentType = await _createDocumentType.Create(name, description);

        //Assert
        _unitOfWorkMock.Verify(p => p.SaveChangesAsync(), Times.Once);
        _documentRepositoryMock.Verify(p=>p.AddAsync(It.IsAny<DocumentType>()), Times.Once);
        
        documentType.Should().NotBeNull();
        documentType.Name.Should().Be(name);
        documentType.Description.Should().Be(description);
    }
    
    [Fact]
    public async void execute_shouldThrowValidationException_WhenNameIsExist()
    {
        //Arrange
        const string name = "Analyse de donnée";
        const string description = "Description analyse de donnée";
        
        _documentRepositoryMock.Setup(p => p.GetByNameAsync("Analyse de donnée")).ReturnsAsync(new DocumentType(1, "Analyse de donnée", "Description analyse de donnée"));
        
        //Act
        var act = async () => { await _createDocumentType.Create(name, description); };
        
        //Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async  void execute_shouldThrowValidationException_WhenNameIsEmpty()
    {
        //Arrange
        const string description = "Description analyse de donnée";
        
        //Act
        var act = async () => { await _createDocumentType.Create(string.Empty, description); };
        
        //Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
    
    [Fact]
    public async  void execute_shouldThrowValidationException_WhenDescriptionIsEmpty()
    {
        //Arrange
        const string name = "Analyse de donnée";
        
        //Act
        var act = async () => { await _createDocumentType.Create(name, string.Empty); };
        
        //Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}