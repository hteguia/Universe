using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Api.Controllers.Models;
using Api.IntegrationTests.Utilities;
using Domain.DocumentTypes.Models;
using FluentAssertions;

namespace Api.IntegrationTests;

public class DocumentTypeEndPointsTests
{
    [Fact]
    public async Task CreateDocumentType_ShouldReturnSuccessAndCorrectData()
    {
        // Arrange
        using var factory = new WebFactory();
        var client = factory.CreateClient();
        var documentType = new DocumentType("Facture", "Document de facturation");
        
        var content = new StringContent(JsonSerializer.Serialize(documentType), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/DocumentType", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var documentTypeResponse = await response.Content.ReadFromJsonAsync<AddDocumentTypeResponse>();
        documentTypeResponse.Should().NotBeNull();
        documentTypeResponse.Id.Should().BeGreaterThan(0);
        documentTypeResponse.Name.Should().Be(documentType.Name);
        documentTypeResponse.Description.Should().Be(documentType.Description);
    }
}