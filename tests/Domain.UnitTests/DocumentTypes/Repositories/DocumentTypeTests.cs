using Domain.Features.DocumentTypes.Repositories;
using FluentAssertions;

namespace Domain.UnitTests.DocumentTypes.Repositories;

public class DocumentTypeTests
{
    [Fact]
    public void DocumentType_WithValidData_ShouldCreateDocumentType()
    {
        //Act
        var documentType = new DocumentType("Analyse de donnée", "Description analyse de donnée");
        
        //Assert
        documentType.Name.Should().Be("Analyse de donnée");
        documentType.Description.Should().Be("Description analyse de donnée");
    }
    
    [Fact]
    public void DocumentType_WithEmptyName_ShouldThrowArgumentException()
    {
        //Act
        var act = () => new DocumentType(string.Empty, "Description analyse de donnée");
        
        //Assert
        act.Should().Throw<ArgumentException>().WithMessage("Name must not be null or empty");
    }
    
    [Fact]
    public void DocumentType_WithEmptyDescription_ShouldThrowArgumentException()
    {
        //Act
        var act = () => new DocumentType("Analyse de donnée", string.Empty);
        
        //Assert
        act.Should().Throw<ArgumentException>().WithMessage("Description must not be null or empty");
    }
    
    [Fact]
    public void UpdateDescription_WithEmptyDescription_ShouldThrowArgumentException()
    {
        //Arrange
        var documentType = new DocumentType("Analyse de donnée", "Description analyse de donnée");
        
        //Act
        var act = () => documentType.UpdateDescription(string.Empty);
        
        //Assert
        act.Should().Throw<ArgumentException>().WithMessage("Description must not be null or empty");
    }
    
    [Fact]
    public void UpdateName_WithEmptyName_ShouldThrowArgumentException()
    {
        //Arrange
        var documentType = new DocumentType("Analyse de donnée", "Description analyse de donnée");
        
        //Act
        var act = () => documentType.UpdateName(string.Empty);
        
        //Assert
        act.Should().Throw<ArgumentException>().WithMessage("Name must not be null or empty");
    }
}