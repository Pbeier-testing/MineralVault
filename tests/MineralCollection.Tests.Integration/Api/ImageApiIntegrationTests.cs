using System.Net;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MineralCollection.API.Data;
using MineralCollection.Domain;
using MineralCollection.Tests.Integration.Infrastructure;

namespace MineralCollection.Tests.Integration.Api;

public class ImageApiIntegrationTests
{
    [Fact]
    [Trait("TestLevel", "Integration")]
    [Trait("TestCase", "ITC-IMG-001")]
    [Trait("Requirement", "R7.4")]
    public async Task UploadImage_WhenFileIsProvided_StoresImageFile()
    {
        await using var factory = new MineralApiFactory();
        var client = factory.CreateClient();
        using var content = new MultipartFormDataContent();
        using var fileContent = new ByteArrayContent([1, 2, 3]);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
        content.Add(fileContent, "file", "quarz.jpg");

        var response = await client.PostAsync("/api/image/upload", content);
        var uploadResult = await response.Content.ReadFromJsonAsync<UploadResult>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(uploadResult?.FileName);
        Assert.EndsWith("_quarz.jpg", uploadResult.FileName);
        Assert.True(File.Exists(Path.Combine(factory.ImagesPath, uploadResult.FileName)));
    }

    [Fact]
    [Trait("TestLevel", "Integration")]
    [Trait("TestCase", "ITC-IMG-002")]
    [Trait("Requirement", "R9.9")]
    public async Task DeleteMineral_WhenMineralHasImage_RemovesImageReference()
    {
        await using var factory = new MineralApiFactory();
        var client = factory.CreateClient();
        Directory.CreateDirectory(factory.ImagesPath);
        File.WriteAllBytes(Path.Combine(factory.ImagesPath, "test-image.jpg"), [1, 2, 3]);
        var mineral = new Mineral
        {
            Name = "Quarz",
            Images = [new MineralImage { FileName = "test-image.jpg" }]
        };
        var postResponse = await client.PostAsJsonAsync("/api/minerals", mineral);
        var createdMineral = await postResponse.Content.ReadFromJsonAsync<Mineral>();

        var deleteResponse = await client.DeleteAsync($"/api/minerals/{createdMineral!.Id}");
        using var scope = factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var imageReferenceExists = await dbContext.MineralImages.AnyAsync();

        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        Assert.False(imageReferenceExists);
    }

    [Fact]
    [Trait("TestLevel", "Integration")]
    [Trait("TestCase", "ITC-IMG-003")]
    [Trait("Requirement", "R9.10")]
    public async Task DeleteMineral_WhenMineralHasImage_RemovesImageFile()
    {
        await using var factory = new MineralApiFactory();
        var client = factory.CreateClient();
        Directory.CreateDirectory(factory.ImagesPath);
        var imagePath = Path.Combine(factory.ImagesPath, "test-image.jpg");
        File.WriteAllBytes(imagePath, [1, 2, 3]);
        var mineral = new Mineral
        {
            Name = "Quarz",
            Images = [new MineralImage { FileName = "test-image.jpg" }]
        };
        var postResponse = await client.PostAsJsonAsync("/api/minerals", mineral);
        var createdMineral = await postResponse.Content.ReadFromJsonAsync<Mineral>();

        var deleteResponse = await client.DeleteAsync($"/api/minerals/{createdMineral!.Id}");

        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        Assert.False(File.Exists(imagePath));
    }

    private sealed class UploadResult
    {
        public string? FileName { get; set; }
    }
}
