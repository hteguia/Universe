using Domain.DocumentTypes;
using Domain.DocumentTypes.Features;
using Domain.DocumentTypes.Models;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Domain.UnitTests.DocumentTypes;

public class UpdateDocumentTypeTests
{
    private UpdateDocumentType _updateDocumentType;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IDocumentTypeRepository> _documentRepositoryMock;
    
    public UpdateDocumentTypeTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _documentRepositoryMock = new Mock<IDocumentTypeRepository>();
        _unitOfWorkMock.Setup(p => p.AsyncRepository<DocumentType>()).Returns(_documentRepositoryMock.Object);
        _updateDocumentType = new UpdateDocumentType(_unitOfWorkMock.Object);
    }

    [Fact]
    public async void execute_mustUpdateDocumentTypeProperly()
    {
        const string name = "New Analyse de donnée";
        const string description = "New Description analyse de donnée";

        DocumentType updated = new("Analyse de donnée", "Description analyse de donnée");

        _documentRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(updated);

        await _updateDocumentType.Update(1, name, description);

        _documentRepositoryMock.Verify(p=>p.GetByIdAsync(It.IsAny<long>()), Times.Once);
        _documentRepositoryMock.Verify(p=>p.UpdateAsync(It.IsAny<DocumentType>()), Times.Once);
        updated.Name.Should().Be(name);
        updated.Description.Should().Be(description);
    }
}