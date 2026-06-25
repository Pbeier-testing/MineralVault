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
        Assert.False(viewModel.HasSaveError);
        Assert.Equal("Änderungen gespeichert.", viewModel.SaveMessage);
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
        viewModel.MarkMineralAsChanged(mineral);

        await viewModel.SaveChangesAsync();

        var request = Assert.Single(httpHandler.Requests);
        Assert.Equal(HttpMethod.Put, request.Method);
        Assert.Equal("/api/minerals/7", request.RequestUri?.PathAndQuery);
        Assert.False(viewModel.IsSaving);
        Assert.False(viewModel.HasSaveError);
        Assert.Equal("Änderungen gespeichert.", viewModel.SaveMessage);
    }

    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-SAVE-006")]
    [Trait("Requirement", "R9.1")]
    [Trait("Requirement", "R9.3")]
    public async Task SaveChangesAsync_WhenExistingMineralIsUnchanged_DoesNotSendRequest()
    {
        var httpHandler = new FakeHttpMessageHandler();
        var viewModel = CreateViewModel(httpHandler);
        viewModel.Minerals = [new Mineral { Id = 7, Name = "Quarz", Nummer = "1" }];

        await viewModel.SaveChangesAsync();

        Assert.Empty(httpHandler.Requests);
        Assert.False(viewModel.IsSaving);
        Assert.False(viewModel.HasSaveError);
        Assert.Equal("Keine Änderungen zum Speichern.", viewModel.SaveMessage);
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

    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-SAVE-005")]
    [Trait("Requirement", "R10.4")]
    [Trait("Requirement", "R10.5")]
    public async Task SaveChangesAsync_WhenApiReturnsValidationError_SetsSaveErrorMessage()
    {
        var httpHandler = new FakeHttpMessageHandler();
        httpHandler.EnqueueResponse(ValidationProblemResponse(
            """{"errors":{"Fundjahr":["Fundjahr muss zwischen 1801 und 2026 liegen."]}}"""));
        var viewModel = CreateViewModel(httpHandler);
        viewModel.Minerals = [new Mineral { Id = 0, Name = "Quarz", Fundjahr = 1800 }];

        await viewModel.SaveChangesAsync();

        Assert.False(viewModel.IsSaving);
        Assert.True(viewModel.HasSaveError);
        Assert.Contains("Quarz", viewModel.SaveMessage);
        Assert.Contains("Fundjahr muss zwischen", viewModel.SaveMessage);
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

    private static HttpResponseMessage ValidationProblemResponse(string json)
    {
        return new HttpResponseMessage(HttpStatusCode.BadRequest)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/problem+json")
        };
    }
}
