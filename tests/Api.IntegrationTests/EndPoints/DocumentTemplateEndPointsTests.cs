using System.Net.Http.Json;
using System.Text;
using Api.DocumentTemplates.Models;
using Api.IntegrationTests.Utilities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Api.IntegrationTests.EndPoints;

public class DocumentTemplateEndPointsTests
{
    [Fact]
    public async Task CreateDocumentTemplate_ShouldReturnSuccessAndCorrectData()
    {
        using var webFactory = new WebFactory();
        var client = webFactory.CreateClient();

        const string name = "Test Document Template.pdf";
        var file = GetMockFormFile().Object;

        using var content = new MultipartFormDataContent
        {
            { new StringContent(name), "Name" }
        };
        var fileContent = new StreamContent(file.OpenReadStream());
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data");
        content.Add(fileContent, "Content", file.FileName);

        var response = await client.PostAsync("/DocumentTemplate", content);

        response.EnsureSuccessStatusCode();
        var documentTemplateResponse = await response.Content.ReadFromJsonAsync<AddDocumentTemplateResponse>();
        documentTemplateResponse.Should().NotBeNull();
    }

    private static Mock<IFormFile> GetMockFormFile()
    {
        var fileName = "test.txt";
        var stream = new MemoryStream(Encoding.UTF8.GetBytes("Test Content"));
        var formFile = new Mock<IFormFile>();

        formFile.Setup(f => f.OpenReadStream()).Returns(stream);
        formFile.Setup(f => f.FileName).Returns(fileName);
        formFile.Setup(f => f.Length).Returns(stream.Length);
        return formFile;
    }
}