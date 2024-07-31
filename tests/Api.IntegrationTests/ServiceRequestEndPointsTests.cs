using System.Net.Http.Json;
using System.Text;
using Api.IntegrationTests.Utilities;
using Api.ServiceRequests.Models;
using Domain.ServiceRequests.Features;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Api.IntegrationTests;

public class ServiceRequestEndPointsTests
{
    private CreateServiceRequest _createServiceRequest;

    [Fact]
    public async Task CreateServiceRequest_ShouldReturnSuccessAndCorrectData()
    {
        using var webFactory = new WebFactory();
        var client = webFactory.CreateClient();
        
        const string name = "Test Service Request";
        
        var file = GetMockFormFile().Object;
        
        using var content = new MultipartFormDataContent
        {
            { new StringContent(name), "Name" },
            { new StringContent("7 Jours"), "DeadLine" },
            { new StringContent("1"), "DocumentTypeId" } 
        };
        var fileContent = new StreamContent(file.OpenReadStream());
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data");
        content.Add(fileContent, "Content", file.FileName);
        
        var response = await client.PostAsync("/ServiceRequest", content);
        
        response.EnsureSuccessStatusCode();
        var serviceRequestResponse = await response.Content.ReadFromJsonAsync<AddServiceRequestResponse>();
        serviceRequestResponse.Should().NotBeNull();
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