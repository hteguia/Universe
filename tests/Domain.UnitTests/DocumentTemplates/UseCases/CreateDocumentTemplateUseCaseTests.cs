using System.Text;
using Domain.Features.DocumentTemplates;
using Domain.Features.DocumentTemplates.Entities;
using Domain.Features.DocumentTemplates.UseCases.CreateDocumentTemplate;
using Domain.Interfaces.Providers;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.Base;
using FluentAssertions;
using Moq;

namespace Domain.UnitTests.DocumentTemplates.UseCases;

public class CreateDocumentTemplateUseCaseTests
{
    private readonly CreateDocumentTemplateUseCase _createDocumentTemplateUseCase;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IDocumentTemplateRepository> _documentTemplateRepositoryMock;
    private readonly Mock<IFileProvider> _fileRepositoryMock;

    public CreateDocumentTemplateUseCaseTests()
    {
        _fileRepositoryMock = new Mock<IFileProvider>();
        _documentTemplateRepositoryMock = new Mock<IDocumentTemplateRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _unitOfWorkMock.Setup(p => p.DocumentTemplateRepository).Returns(_documentTemplateRepositoryMock.Object);
        _unitOfWorkMock.Setup(p => p.AsyncRepository<DocumentTemplate>()).Returns(_documentTemplateRepositoryMock.Object);
        _createDocumentTemplateUseCase = new CreateDocumentTemplateUseCase(_unitOfWorkMock.Object, _fileRepositoryMock.Object);
    }

    [Fact]
    public async void CreateDocumentTemplate_WithValidData_ShouldCreateDocumentTemplate()
    {
        //Arrange
        const string path = @"C:\Analyse de donnée.pdf";
        
        const string name = "Analyse de donnée.pdf";
        byte[] fileContent = Encoding.UTF8.GetBytes("content");
        
        _fileRepositoryMock.Setup(p => p.SaveFile(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(path);
        
        //Act
        var documentTemplate = await _createDocumentTemplateUseCase.Create(new CreateDocumentTemplateUseCaseCommand(name, fileContent));
        
        //Assert
        _fileRepositoryMock.Verify(p=>p.SaveFile(It.IsAny<string>(), It.IsAny<byte[]>()), Times.Once);
        _documentTemplateRepositoryMock.Verify(p=>p.AddAsync(It.IsAny<DocumentTemplate>()), Times.Once);
        
        documentTemplate.Name.Should().Be(name);
        documentTemplate.Path.Should().Be(path);
    }
    
    
    [Fact]
    public void CreateDocumentTemplate_ShouldThrowValidationException_WhenNameIsExist()
    {
        //Arrange
        const string name = "Analyse de donnée.pdf";
        byte[] fileContent = Encoding.UTF8.GetBytes("content");
        
        _documentTemplateRepositoryMock.Setup(p => p.GetByNameAsync("Analyse de donnée.pdf")).ReturnsAsync(new DocumentTemplate(1, "Analyse de donnée.pdf", "C:\\Analyse de donnée.pdf"));
        
        //Act
        var act = async () => { await _createDocumentTemplateUseCase.Create(new CreateDocumentTemplateUseCaseCommand(name, fileContent)); };
        
        //Assert
        act.Should().ThrowAsync<Domain.Exceptions.ValidationException>();
    }
    
    [Fact]
    public void CreateDocumentTemplate_ShouldThrowValidationException_WhenFileContentIsEmpty()
    {
        //Arrange
        const string name = "Analyse de donnée.pdf";
        
        //Act
        var act = async () => { await _createDocumentTemplateUseCase.Create(new CreateDocumentTemplateUseCaseCommand(name, Array.Empty<byte>())); };
        
        //Assert
        act.Should().ThrowAsync<Domain.Exceptions.ValidationException>();
    }
}