using System.Net;
using System.Text;
using MineralCollection.Domain;
using MineralCollection.Frontend.ViewModels;
using MineralCollection.Tests.Unit.TestDoubles;

namespace MineralCollection.Tests.Unit.Frontend;

public class HomeViewModelSaveTests
{
    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-SAVE-001")]
    [Trait("Requirement", "R9.1")]
    [Trait("Requirement", "R9.2")]
    public async Task SaveChangesAsync_WhenMineralIsNew_SendsPostRequestAndUpdatesId()
    {
        var httpHandler = new FakeHttpMessageHandler();
        httpHandler.EnqueueResponse(JsonResponse("""{"id":42,"name":"Neues Mineral"}"""));
        var viewModel = CreateViewModel(httpHandler);
        var mineral = new Mineral { Id = 0, Name = "Neues Mineral", Nummer = "0" };
        viewModel.Minerals = [mineral];

        await viewModel.SaveChangesAsync();

        var request = Assert.Single(httpHandler.Requests);
        Assert.Equal(HttpMethod.Post, request.Method);
        Assert.Equal("/api/minerals", request.RequestUri?.PathAndQuery);
        Assert.Equal(42, mineral.Id);
        Assert.False(viewModel.IsSaving);
    }

    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-SAVE-002")]
    [Trait("Requirement", "R9.1")]
    [Trait("Requirement", "R9.3")]
    public async Task SaveChangesAsync_WhenMineralAlreadyExists_SendsPutRequest()
    {
        var httpHandler = new FakeHttpMessageHandler();
        httpHandler.EnqueueResponse(new HttpResponseMessage(HttpStatusCode.NoContent));
        var viewModel = CreateViewModel(httpHandler);
        var mineral = new Mineral { Id = 7, Name = "Quarz", Nummer = "1" };
        viewModel.Minerals = [mineral];

        await viewModel.SaveChangesAsync();

        var request = Assert.Single(httpHandler.Requests);
        Assert.Equal(HttpMethod.Put, request.Method);
        Assert.Equal("/api/minerals/7", request.RequestUri?.PathAndQuery);
        Assert.False(viewModel.IsSaving);
    }

    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-SAVE-003")]
    [Trait("Requirement", "R9.1")]
    public async Task SaveChangesAsync_WhenMineralsAreNotLoaded_DoesNotSendRequest()
    {
        var httpHandler = new FakeHttpMessageHandler();
        var viewModel = CreateViewModel(httpHandler);

        await viewModel.SaveChangesAsync();

        Assert.Empty(httpHandler.Requests);
        Assert.False(viewModel.IsSaving);
    }

    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-SAVE-004")]
    [Trait("Requirement", "R9.1")]
    public async Task SaveChangesAsync_WhenAlreadySaving_DoesNotSendRequest()
    {
        var httpHandler = new FakeHttpMessageHandler();
        var viewModel = CreateViewModel(httpHandler);
        viewModel.Minerals = [new Mineral { Id = 1, Name = "Quarz" }];
        viewModel.IsSaving = true;

        await viewModel.SaveChangesAsync();

        Assert.Empty(httpHandler.Requests);
        Assert.True(viewModel.IsSaving);
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
        return new HttpResponseMessage(HttpStatusCode.Created)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
    }
}
