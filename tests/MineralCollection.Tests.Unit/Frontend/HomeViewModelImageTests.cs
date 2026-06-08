using System.Net;
using System.Text;
using MineralCollection.Domain;
using MineralCollection.Frontend.ViewModels;
using MineralCollection.Tests.Unit.TestDoubles;

namespace MineralCollection.Tests.Unit.Frontend;

public class HomeViewModelImageTests
{
    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-IMAGE-001")]
    [Trait("Requirement", "R7.5")]
    public async Task HandleImageUploadAsync_WhenUploadSucceeds_ReplacesExistingImageWithUploadedImage()
    {
        var httpHandler = new FakeHttpMessageHandler();
        httpHandler.EnqueueResponse(JsonResponse("""{"fileName":"new-image.jpg"}"""));
        var viewModel = CreateViewModel(httpHandler);
        var mineral = new Mineral
        {
            Name = "Quarz",
            Images = [new MineralImage { FileName = "old-image.jpg" }]
        };
        var file = new FakeBrowserFile("quarz.jpg", "image/jpeg", [1, 2, 3]);

        await viewModel.HandleImageUploadAsync(file, mineral);

        Assert.Single(mineral.Images);
        Assert.Equal("new-image.jpg", mineral.Images[0].FileName);
        var request = Assert.Single(httpHandler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/image/upload", request.RequestUri?.PathAndQuery);
    }

    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-IMAGE-002")]
    [Trait("Requirement", "R7.5")]
    public async Task HandleImageUploadAsync_WhenUploadFails_KeepsExistingImage()
    {
        var httpHandler = new FakeHttpMessageHandler();
        httpHandler.EnqueueResponse(new HttpResponseMessage(HttpStatusCode.BadRequest));
        var viewModel = CreateViewModel(httpHandler);
        var mineral = new Mineral
        {
            Name = "Quarz",
            Images = [new MineralImage { FileName = "old-image.jpg" }]
        };
        var file = new FakeBrowserFile("quarz.jpg", "image/jpeg", [1, 2, 3]);

        await viewModel.HandleImageUploadAsync(file, mineral);

        Assert.Single(mineral.Images);
        Assert.Equal("old-image.jpg", mineral.Images[0].FileName);
    }

    private static HomeViewModel CreateViewModel(FakeHttpMessageHandler httpHandler)
    {
        var httpClient = new HttpClient(httpHandler)
        {
            BaseAddress = new Uri("http://localhost/")
        };

        return new HomeViewModel(httpClient, new ConfigurableJSRuntime());
    }

    private static HttpResponseMessage JsonResponse(string json)
    {
        return new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
    }
}
