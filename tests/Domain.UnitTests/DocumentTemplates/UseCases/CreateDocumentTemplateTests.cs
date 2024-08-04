using System.Text;
using Domain.Contracts;
using Domain.Features.DocumentTemplates.Models;
using Domain.Features.DocumentTemplates.Repositories;
using Domain.Features.DocumentTemplates.UseCases;
using Domain.Interfaces;
using Domain.Interfaces.Repositories.Base;
using FluentAssertions;
using Moq;

namespace Domain.UnitTests.DocumentTemplates.UseCases;

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
        _unitOfWorkMock.Setup(p => p.DocumentTemplateRepository).Returns(_documentTemplateRepositoryMock.Object);
        _unitOfWorkMock.Setup(p => p.AsyncRepository<DocumentTemplate>()).Returns(_documentTemplateRepositoryMock.Object);
        _createDocumentTemplate = new CreateDocumentTemplate(_unitOfWorkMock.Object, _fileRepositoryMock.Object);
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
        var documentTemplate = await _createDocumentTemplate.Create(new CreateDocumentTemplateModel()
        {
            Name = name,
            FileContent = fileContent
        });
        
        //Assert
        _fileRepositoryMock.Verify(p=>p.SaveFile(It.IsAny<string>(), It.IsAny<byte[]>()), Times.Once);
        _documentTemplateRepositoryMock.Verify(p=>p.AddAsync(It.IsAny<DocumentTemplate>()), Times.Once);
        
        documentTemplate.Name.Should().Be(name);
        documentTemplate.Path.Should().Be(path);
    }
    
    
    [Fact]
    public void execute_shouldThrowValidationException_WhenNameIsExist()
    {
        //Arrange
        const string name = "Analyse de donnée.pdf";
        byte[] fileContent = Encoding.UTF8.GetBytes("content");
        
        _documentTemplateRepositoryMock.Setup(p => p.GetByNameAsync("Analyse de donnée.pdf")).ReturnsAsync(new DocumentTemplate(1, "Analyse de donnée.pdf", "C:\\Analyse de donnée.pdf"));
        
        //Act
        var act = async () => { await _createDocumentTemplate.Create(new CreateDocumentTemplateModel()
        {
            Name = name,
            FileContent = fileContent
        }); };
        
        //Assert
        act.Should().ThrowAsync<Domain.Exceptions.ValidationException>();
    }
    
    [Fact]
    public void excute_shouldThrowValidationException_WhenFileContentIsEmpty()
    {
        //Arrange
        const string name = "Analyse de donnée.pdf";
        
        //Act
        var act = async () => { await _createDocumentTemplate.Create(new CreateDocumentTemplateModel()
        {
            Name = name,
            FileContent = new byte[0]
        }); };
        
        //Assert
        act.Should().ThrowAsync<Domain.Exceptions.ValidationException>();
    }
}