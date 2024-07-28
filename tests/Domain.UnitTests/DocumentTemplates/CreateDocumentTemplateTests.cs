using System.Text;
using Domain.Contrats;
using Domain.DocumentTemplates;
using Domain.DocumentTemplates.Features;
using Domain.DocumentTemplates.Models;
using Domain.DocumentTypes;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Domain.UnitTests.DocumentTemplates;

public class CreateDocumentTemplateTests
{
    private CreateDocumentTemplate _createDocumentTemplate;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IDocumentTemplateRepository> _documentTemplateRepositoryMock;
    private Mock<IFileRepository> _fileRepositoryMock;
    

    public CreateDocumentTemplateTests()
    {
        _fileRepositoryMock = new Mock<IFileRepository>();
        _documentTemplateRepositoryMock = new Mock<IDocumentTemplateRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    [Fact]
    public async void execute_mustCreateDocumentTypeProperly()
    {
        const string path = @"c:\myfile.txt";
        const string name = "Analyse de donnée.pdf";
        const string strContent = "content";
        byte[] fileContent = Encoding.UTF8.GetBytes(strContent);
        
        _fileRepositoryMock.Setup(p => p.SaveFile(It.IsAny<string>(), It.IsAny<byte[]>())).Returns(path);
        _unitOfWorkMock.Setup(p => p.AsyncRepository<DocumentTemplate>())
            .Returns(_documentTemplateRepositoryMock.Object);
        
        _createDocumentTemplate = new CreateDocumentTemplate(_unitOfWorkMock.Object, _fileRepositoryMock.Object);
        
        DocumentTemplate documentTemplate = await _createDocumentTemplate.Create(name, fileContent);
        
        _fileRepositoryMock.Verify(p=>p.SaveFile(It.IsAny<string>(), It.IsAny<byte[]>()), Times.Once);
        _documentTemplateRepositoryMock.Verify(p=>p.AddAsync(It.IsAny<DocumentTemplate>()), Times.Once);
        documentTemplate.Name.Should().Be(name);
        documentTemplate.Path.Should().Be(path);
    }
}