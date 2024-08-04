using Domain.Exceptions;
using Domain.Features.DocumentTypes.Repositories;
using Domain.Features.DocumentTypes.UseCases;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.Base;
using FluentAssertions;
using Moq;

namespace Domain.UnitTests.DocumentTypes.UseCases;

public class CreateDocumentTypeTests
{
    private readonly CreateDocumentType _createDocumentType;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IDocumentTypeRepository> _documentRepositoryMock;
    
    public CreateDocumentTypeTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _documentRepositoryMock = new Mock<IDocumentTypeRepository>();
        _unitOfWorkMock.Setup(p => p.DocumentTypeRepository).Returns(_documentRepositoryMock.Object);
        _unitOfWorkMock.Setup(p => p.AsyncRepository<DocumentType>()).Returns(_documentRepositoryMock.Object);
        _createDocumentType = new CreateDocumentType(_unitOfWorkMock.Object);
    }
    
    [Fact]
    public async void CreateDocumentType_WithValidData_ShouldCreateDocumentType()
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
    public async void CreateDocumentType_shouldThrowValidationException_WhenNameIsExist()
    {
        //Arrange
        _documentRepositoryMock.Setup(p => p.GetByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(new DocumentType(1, "Analyse de donnée", "Description analyse de donnée"));
        
        //Act
        var act = async () => { await _createDocumentType.Create("Analyse de donnée", "New Description analyse de donnée"); };
        
        //Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}