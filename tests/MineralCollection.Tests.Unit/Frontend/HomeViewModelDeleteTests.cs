using MineralCollection.Domain;
using MineralCollection.Frontend.ViewModels;
using MineralCollection.Tests.Unit.TestDoubles;

namespace MineralCollection.Tests.Unit.Frontend;

public class HomeViewModelDeleteTests
{
    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-DELETE-001")]
    [Trait("Requirement", "R9.6")]
    public async Task DeleteMineralAsync_WhenDeletionIsCancelled_DoesNotRemoveMineral()
    {
        var jsRuntime = new ConfigurableJSRuntime { ConfirmResult = false };
        var httpHandler = new FakeHttpMessageHandler();
        var viewModel = CreateViewModel(httpHandler, jsRuntime);
        var mineral = new Mineral { Id = 7, Name = "Quarz" };
        viewModel.Minerals = [mineral];

        await viewModel.DeleteMineralAsync(mineral);

        Assert.Single(viewModel.Minerals);
        Assert.Same(mineral, viewModel.Minerals[0]);
        Assert.Empty(httpHandler.Requests);
    }

    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-DELETE-002")]
    [Trait("Requirement", "R9.4")]
    [Trait("Requirement", "R9.7")]
    [Trait("Requirement", "R9.8")]
    public async Task DeleteMineralAsync_WhenPersistedMineralIsConfirmed_SendsDeleteRequestAndRemovesMineral()
    {
        var jsRuntime = new ConfigurableJSRuntime { ConfirmResult = true };
        var httpHandler = new FakeHttpMessageHandler();
        var viewModel = CreateViewModel(httpHandler, jsRuntime);
        var mineral = new Mineral { Id = 7, Name = "Quarz" };
        viewModel.Minerals = [mineral];

        await viewModel.DeleteMineralAsync(mineral);

        Assert.Empty(viewModel.Minerals);
        var request = Assert.Single(httpHandler.Requests);
        Assert.Equal(HttpMethod.Delete, request.Method);
        Assert.Equal("/api/minerals/7", request.RequestUri?.PathAndQuery);
    }

    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-DELETE-003")]
    [Trait("Requirement", "R9.4")]
    [Trait("Requirement", "R9.7")]
    [Trait("Requirement", "R9.10")]
    public async Task DeleteMineralAsync_WhenUnsavedMineralWithUploadedImageIsConfirmed_DeletesImageAndRemovesMineral()
    {
        var jsRuntime = new ConfigurableJSRuntime { ConfirmResult = true };
        var httpHandler = new FakeHttpMessageHandler();
        var viewModel = CreateViewModel(httpHandler, jsRuntime);
        var mineral = new Mineral
        {
            Id = 0,
            Name = "Neues Mineral",
            Images = [new MineralImage { FileName = "temp-image.jpg" }]
        };
        viewModel.Minerals = [mineral];

        await viewModel.DeleteMineralAsync(mineral);

        Assert.Empty(viewModel.Minerals);
        var request = Assert.Single(httpHandler.Requests);
        Assert.Equal(HttpMethod.Delete, request.Method);
        Assert.Equal("/api/image/temp-image.jpg", request.RequestUri?.PathAndQuery);
    }

    private static HomeViewModel CreateViewModel(FakeHttpMessageHandler httpHandler, ConfigurableJSRuntime jsRuntime)
    {
        var httpClient = new HttpClient(httpHandler)
        {
            BaseAddress = new Uri("http://localhost/")
        };

        return new HomeViewModel(httpClient, jsRuntime);
    }
}
