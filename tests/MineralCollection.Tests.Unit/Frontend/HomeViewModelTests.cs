using MineralCollection.Domain;
using MineralCollection.Frontend.ViewModels;
using MineralCollection.Tests.Unit.TestDoubles;

namespace MineralCollection.Tests.Unit.Frontend;

public class HomeViewModelTests
{
    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-SEARCH-001")]
    [Trait("Requirement", "R4.3")]
    [Trait("Requirement", "R4.5")]
    public void FilteredMinerals_WhenSearchTermMatchesName_ReturnsMatchingMineral()
    {
        var viewModel = CreateViewModel();
        viewModel.Minerals = CreateMinerals();
        viewModel.ActiveTab = "admin";

        viewModel.SearchTerm = "fluss";

        var result = viewModel.FilteredMinerals.ToList();
        Assert.Single(result);
        Assert.Equal("Flussspat", result[0].Name);
    }

    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-SEARCH-002")]
    [Trait("Requirement", "R4.3")]
    [Trait("Requirement", "R4.5")]
    public void FilteredMinerals_WhenSearchTermMatchesFundort_ReturnsMatchingMineral()
    {
        var viewModel = CreateViewModel();
        viewModel.Minerals = CreateMinerals();
        viewModel.ActiveTab = "admin";

        viewModel.SearchTerm = "dres";

        var result = viewModel.FilteredMinerals.ToList();
        Assert.Single(result);
        Assert.Equal("Calcit", result[0].Name);
    }

    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-SEARCH-003")]
    [Trait("Requirement", "R4.6")]
    public void FilteredMinerals_WhenSearchTermDiffersByCase_ReturnsMatchingMineral()
    {
        var viewModel = CreateViewModel();
        viewModel.Minerals = CreateMinerals();
        viewModel.ActiveTab = "admin";

        viewModel.SearchTerm = "FREIBERG";

        var result = viewModel.FilteredMinerals.ToList();
        Assert.Single(result);
        Assert.Equal("Flussspat", result[0].Name);
    }

    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-SEARCH-004")]
    [Trait("Requirement", "R4.4")]
    public void FilteredMinerals_WhenSearchTermMatchesRegion_ReturnsMatchingMinerals()
    {
        var viewModel = CreateViewModel();
        viewModel.Minerals = CreateMinerals();
        viewModel.ActiveTab = "admin";

        viewModel.SearchTerm = "Sachsen";

        var result = viewModel.FilteredMinerals.ToList();
        Assert.Equal(2, result.Count);
        Assert.All(result, mineral => Assert.Equal("Sachsen", mineral.Region));
    }

    /*[Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-SEARCH-005")]
    [Trait("Requirement", "R4.4")]
    public void FilteredMinerals_WhenSearchTermMatchesLand_ReturnsMatchingMinerals()
    {
        var viewModel = CreateViewModel();
        viewModel.Minerals = CreateMinerals();
        viewModel.ActiveTab = "admin";

        viewModel.SearchTerm = "Deutschland";

        var result = viewModel.FilteredMinerals.ToList();
        Assert.Equal(3, result.Count);
    }*/

    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-SORT-001")]
    [Trait("Requirement", "R3.5")]
    [Trait("Requirement", "R4.11")]
    public void FilteredMinerals_WhenSortByIsDefault_SortsByName()
    {
        var viewModel = CreateViewModel();
        viewModel.Minerals = CreateMinerals();

        var result = viewModel.FilteredMinerals.Select(mineral => mineral.Name).ToList();

        Assert.Equal(["Amethyst", "Calcit", "Flussspat"], result);
    }

    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-SORT-002")]
    [Trait("Requirement", "R4.11")]
    public void FilteredMinerals_WhenSortByIsRegion_SortsByRegion()
    {
        var viewModel = CreateViewModel();
        viewModel.Minerals = CreateMinerals();
        viewModel.SortBy = "Region";

        var result = viewModel.FilteredMinerals.Select(mineral => mineral.Region).ToList();

        Assert.Equal(["Bayern", "Sachsen", "Sachsen"], result);
    }

    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-SORT-003")]
    [Trait("Requirement", "R4.11")]
    [Trait("Requirement", "R4.13")]
    public void FilteredMinerals_WhenSortByIsNummer_SortsNumerically()
    {
        var viewModel = CreateViewModel();
        viewModel.Minerals = CreateMinerals();
        viewModel.SortBy = "Nummer";

        var result = viewModel.FilteredMinerals.Select(mineral => mineral.Nummer).ToList();

        Assert.Equal(["1", "2", "10"], result);
    }

    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-CREATE-001")]
    [Trait("TestCase", "UTC-CREATE-002")]
    [Trait("Requirement", "R6.1")]
    [Trait("Requirement", "R6.2")]
    [Trait("Requirement", "R6.3")]
    public void CreateNewMineral_WhenMineralsExist_InsertsNewMineralAtTopWithDefaults()
    {
        var viewModel = CreateViewModel();
        viewModel.Minerals = CreateMinerals();
        viewModel.SearchTerm = "quarz";

        viewModel.CreateNewMineral();

        var newMineral = viewModel.Minerals[0];
        Assert.Equal(4, viewModel.Minerals.Count);
        Assert.Equal(0, newMineral.Id);
        Assert.Equal("Neues Mineral", newMineral.Name);
        Assert.Equal("0", newMineral.Nummer);
        Assert.Empty(newMineral.Images);
        Assert.Equal("", viewModel.SearchTerm);
    }

    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-CREATE-003")]
    [Trait("Requirement", "R6.1")]
    public void CreateNewMineral_WhenMineralsAreNotLoaded_DoesNothing()
    {
        var viewModel = CreateViewModel();

        viewModel.CreateNewMineral();

        Assert.Null(viewModel.Minerals);
    }

    private static HomeViewModel CreateViewModel()
    {
        return new HomeViewModel(new HttpClient(), new NoOpJSRuntime());
    }

    private static List<Mineral> CreateMinerals()
    {
        return
        [
            new Mineral
            {
                Id = 1,
                Name = "Amethyst",
                Fundort = "Anlass",
                Region = "Bayern",
                Land = "Deutschland",
                Nummer = "10"
            },
            new Mineral
            {
                Id = 2,
                Name = "Flussspat",
                Fundort = "Freiberg",
                Region = "Sachsen",
                Land = "Deutschland",
                Nummer = "2"
            },
            new Mineral
            {
                Id = 3,
                Name = "Calcit",
                Fundort = "Dresden",
                Region = "Sachsen",
                Land = "Deutschland",
                Nummer = "1"
            }
        ];
    }
}
