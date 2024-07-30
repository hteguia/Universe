using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Api.Controllers.Models;
using Api.IntegrationTests.Utilities;
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
        const string name = "Test Document Type";
        const string description = "Test Document Type Description";
        var requestModel = new AddDocumentTypeRequest()
        {
            Name = name,
            Description = description
        };
        
        var content = new StringContent(JsonSerializer.Serialize(requestModel), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/DocumentType", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var documentTypeResponse = await response.Content.ReadFromJsonAsync<AddDocumentTypeResponse>();
        documentTypeResponse.Should().NotBeNull();
        documentTypeResponse.Id.Should().BeGreaterThan(0);
        documentTypeResponse.Name.Should().Be(name);
        documentTypeResponse.Description.Should().Be(description);
    }
}