using System.Text;
using Domain.Contracts;
using Domain.DocumentTemplates;
using Domain.DocumentTemplates.Features;
using Domain.DocumentTemplates.Models;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Domain.UnitTests.DocumentTemplates;

public class CreateDocumentTemplateTests
{
    private readonly CreateDocumentTemplate _createDocumentTemplate;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IDocumentTemplateRepository> _documentTemplateRepositoryMock;
    private readonly Mock<IFileRepository> _fileRepositoryMock;

    public CreateDocumentTemplateTests()
    {
        _fileRepositoryMock = new Mock<IFileRepository>();
        _documentTemplateRepositoryMock = new Mock<IDocumentTemplateRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _unitOfWorkMock.Setup(p => p.AsyncRepository<DocumentTemplate>()).Returns(_documentTemplateRepositoryMock.Object);
        _createDocumentTemplate = new CreateDocumentTemplate(_unitOfWorkMock.Object, _fileRepositoryMock.Object);
    }

    [Fact]
    public async void execute_shouldCreateDocumentTemplateProperly()
    {
        //Arrange
        const string path = @"C:\Analyse de donnée.pdf";
        
        const string name = "Analyse de donnée.pdf";
        byte[] fileContent = Encoding.UTF8.GetBytes("content");
        
        _fileRepositoryMock.Setup(p => p.SaveFile(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(path);
        
        //Act
        var documentTemplate = await _createDocumentTemplate.Create(name, fileContent);
        
        //Assert
        _fileRepositoryMock.Verify(p=>p.SaveFile(It.IsAny<string>(), It.IsAny<byte[]>()), Times.Once);
        _documentTemplateRepositoryMock.Verify(p=>p.AddAsync(It.IsAny<DocumentTemplate>()), Times.Once);
        
        documentTemplate.Name.Should().Be(name);
        documentTemplate.Path.Should().Be(path);
    }

    [Fact]
    public async void execute_shouldThrowValidationException_WhenNameIsEmpty()
    {
        //Arrange
        byte[] fileContent = Encoding.UTF8.GetBytes("content");
        
        //Act
        var act = async () => { await _createDocumentTemplate.Create(string.Empty, fileContent); };
        
        //Assert
        await act.Should().ThrowAsync<Domain.Exceptions.ValidationException>();
    }
    
    [Fact]
    public void execute_shouldThrowValidationException_WhenNameIsExist()
    {
        //Arrange
        const string name = "Analyse de donnée.pdf";
        byte[] fileContent = Encoding.UTF8.GetBytes("content");
        
        _documentTemplateRepositoryMock.Setup(p => p.GetByNameAsync("Analyse de donnée.pdf")).ReturnsAsync(new DocumentTemplate(1, "Analyse de donnée.pdf", "C:\\Analyse de donnée.pdf"));
        
        //Act
        var act = async () => { await _createDocumentTemplate.Create(name, fileContent); };
        
        //Assert
        act.Should().ThrowAsync<Domain.Exceptions.ValidationException>();
    }
    
    [Fact]
    public void execute_shouldThrowValidationException_WhenNameIsNotValid()
    {
        //Arrange
        const string name = "Analyse de donnée";
        byte[] fileContent = Encoding.UTF8.GetBytes("content");
        
        //Act
        var act = async () => { await _createDocumentTemplate.Create(name, fileContent); };
        
        //Assert
        act.Should().ThrowAsync<Domain.Exceptions.ValidationException>();
    }
}