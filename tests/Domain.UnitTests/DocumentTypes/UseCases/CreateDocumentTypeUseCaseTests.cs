using Domain.Exceptions;
using Domain.Features.DocumentTypes.Models;
using Domain.Features.DocumentTypes.UseCases.CreateDocumentType;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.Base;
using FluentAssertions;
using Moq;

namespace Domain.UnitTests.DocumentTypes.UseCases;

public class CreateDocumentTypeUseCaseTests
{
    private readonly CreateDocumentTypeUseCase _createDocumentTypeUseCase;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IDocumentTypeRepository> _documentRepositoryMock;
    
    public CreateDocumentTypeUseCaseTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _documentRepositoryMock = new Mock<IDocumentTypeRepository>();
        _unitOfWorkMock.Setup(p => p.DocumentTypeRepository).Returns(_documentRepositoryMock.Object);
        _unitOfWorkMock.Setup(p => p.AsyncRepository<DocumentType>()).Returns(_documentRepositoryMock.Object);
        _createDocumentTypeUseCase = new CreateDocumentTypeUseCase(_unitOfWorkMock.Object);
    }
    
    [Fact]
    public async void CreateDocumentType_WithValidData_ShouldCreateDocumentType()
    {
        //Arrange
        const string name = "Analyse de donnée";
        const string description = "Description analyse de donnée";
        
        //Act
        var documentType = await _createDocumentTypeUseCase.Create(new CreateDocumentTypeUseCaseCommand(name, description));

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
        var act = async () => { await _createDocumentTypeUseCase.Create(new CreateDocumentTypeUseCaseCommand("Analyse de donnée", "New Description analyse de donnée")); };
        
        //Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}