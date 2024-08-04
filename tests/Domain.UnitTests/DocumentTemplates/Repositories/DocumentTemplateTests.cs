using Domain.Features.DocumentTemplates.Repositories;
using FluentAssertions;

namespace Domain.UnitTests.DocumentTemplates.Repositories;

public class DocumentTemplateTests
{
    [Fact]
    public void DocumentTemplate_WithValidData_ShouldCreateDocumentTemplate()
    {
        //Act
        var documentTemplate = new DocumentTemplate("Analyse de donnée.pdf", "C:/Analyse de donnée.pdf");
        
        //Assert
        documentTemplate.Name.Should().Be("Analyse de donnée.pdf");
        documentTemplate.Path.Should().Be("C:/Analyse de donnée.pdf");
    }
    
    [Fact]
    public void DocumentTemplate_WithEmptyName_ShouldThrowArgumentException()
    {
        //Act
        var act = () => new DocumentTemplate(string.Empty, "C:/Analyse de donnée.pdf");
        
        //Assert
        act.Should().Throw<ArgumentException>().WithMessage("Name must not be null or empty");
    }
    
    [Fact]
    public void DocumentTemplate_WithEmptyPath_ShouldThrowArgumentException()
    {
        //Act
        var act = () => new DocumentTemplate("Analyse de donnée.pdf", string.Empty);
        
        //Assert
        act.Should().Throw<ArgumentException>().WithMessage("Path must not be null or empty");
    }
    
    [Fact]
    public void UpdatePath_WithEmptyPath_ShouldThrowArgumentException()
    {
        //Arrange
        var documentTemplate = new DocumentTemplate("Analyse de donnée.pdf", "C:/Analyse de donnée.pdf");
        
        //Act
        var act = () => documentTemplate.UpdatePath(string.Empty);
        
        //Assert
        act.Should().Throw<ArgumentException>().WithMessage("Path must not be null or empty");
    }
    
    [Fact]
    public void UpdateName_WithEmptyName_ShouldThrowArgumentException()
    {
        //Arrange
        var documentTemplate = new DocumentTemplate("Analyse de donnée.pdf", "C:/Analyse de donnée.pdf");
        
        //Act
        var act = () => documentTemplate.UpdateName(string.Empty);
        
        //Assert
        act.Should().Throw<ArgumentException>().WithMessage("Name must not be null or empty");
    }
    
    [Fact]
    public void DocumentTemplate_WithInvalidName_ShouldThrowArgumentException()
    {
        //Act
        var act = () => new DocumentTemplate("Analyse de donnée", "C:/Analyse de donnée.pdf");
        
        //Assert
        act.Should().Throw<ArgumentException>().WithMessage("Name must be a valid file name");
    }
}