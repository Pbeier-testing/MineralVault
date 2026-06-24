using MineralCollection.Domain;
using MineralCollection.Frontend.ViewModels;
using MineralCollection.Tests.Unit.TestDoubles;

namespace MineralCollection.Tests.Unit.Frontend;

public class HomeViewModelCoordinateTests
{
    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-COORD-001")]
    [Trait("Requirement", "R8.3")]
    public void UpdateActiveCoordinates_WhenCoordinatesAreSelected_DoesNotImmediatelyChangeMineral()
    {
        var mineral = new Mineral
        {
            Name = "Quarz",
            Breitengrad = 50.0,
            Laengengrad = 10.0
        };
        var viewModel = CreateViewModel();
        viewModel.ActiveMineral = mineral;

        viewModel.UpdateActiveCoordinates("51.123456", "11.654321");

        Assert.Equal(50.0, mineral.Breitengrad);
        Assert.Equal(10.0, mineral.Laengengrad);
        Assert.Equal(51.123456, viewModel.PendingBreitengrad);
        Assert.Equal(11.654321, viewModel.PendingLaengengrad);
    }

    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-COORD-002")]
    [Trait("Requirement", "R8.5")]
    public void CloseCoordPicker_WhenCoordinatesWereSelected_KeepsOriginalMineralCoordinates()
    {
        var mineral = new Mineral
        {
            Name = "Quarz",
            Breitengrad = 50.0,
            Laengengrad = 10.0
        };
        var viewModel = CreateViewModel();
        viewModel.ActiveMineral = mineral;
        viewModel.ShowCoordPicker = true;

        viewModel.UpdateActiveCoordinates("51.123456", "11.654321");
        viewModel.CloseCoordPicker();

        Assert.False(viewModel.ShowCoordPicker);
        Assert.Equal(50.0, mineral.Breitengrad);
        Assert.Equal(10.0, mineral.Laengengrad);
    }

    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-COORD-003")]
    [Trait("Requirement", "R8.6")]
    public void ApplySelectedCoordinates_WhenCoordinatesWereSelected_UpdatesMineralCoordinates()
    {
        var mineral = new Mineral
        {
            Name = "Quarz",
            Breitengrad = 50.0,
            Laengengrad = 10.0
        };
        var viewModel = CreateViewModel();
        viewModel.ActiveMineral = mineral;
        viewModel.ShowCoordPicker = true;

        viewModel.UpdateActiveCoordinates("51.123456", "11.654321");
        viewModel.ApplySelectedCoordinates();

        Assert.False(viewModel.ShowCoordPicker);
        Assert.Equal(51.123456, mineral.Breitengrad);
        Assert.Equal(11.654321, mineral.Laengengrad);
    }

    private static HomeViewModel CreateViewModel()
    {
        return new HomeViewModel(new HttpClient(), new NoOpJSRuntime());
    }
}
