using Microsoft.Playwright;
using MineralCollection.Tests.E2E.Infrastructure;

namespace MineralCollection.Tests.E2E.Table;

public class TableTests
{
    [Fact]
    [Trait("TestLevel", "E2E")]
    [Trait("TestCase", "E2E-TABLE-001")]
    [Trait("Requirement", "R5.2")]
    public async Task TableView_WhenOpened_ShowsExpectedColumns()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await E2ETestContext.LaunchBrowserAsync(playwright);
        var page = await browser.NewPageAsync();

        await page.GotoAsync(E2ETestContext.BaseUrl);
        await page.WaitForSelectorAsync("[data-testid='map-view']");

        await page.ClickAsync("[data-testid='nav-table-view']");
        await page.WaitForSelectorAsync("[data-testid='minerals-table']");

        var columnHeaders = await page.Locator("[data-testid='minerals-table'] thead th").AllInnerTextsAsync();
        var normalizedHeaders = columnHeaders
            .Select(header => header.Trim())
            .ToList();

        var expectedHeaders = new[]
        {
            "Nr.",
            "Bild",
            "Hauptmineral",
            "Begleitmineral",
            "Fundort",
            "Region",
            "Land",
            "Fundjahr",
            "Erwerbjahr",
            "Koordinaten (Lat/Lon)",
            "Aktionen"
        };

        foreach (var expectedHeader in expectedHeaders)
        {
            Assert.Contains(expectedHeader, normalizedHeaders);
        }
    }

    [Fact]
    [Trait("TestLevel", "E2E")]
    [Trait("TestCase", "E2E-CREATE-001")]
    [Trait("Requirement", "R6.1")]
    [Trait("Requirement", "R6.2")]
    [Trait("Requirement", "R6.3")]
    [Trait("Requirement", "R6.4")]
    public async Task TableView_WhenNewMineralIsAdded_ShowsNewHighlightedRowAtTop()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await E2ETestContext.LaunchBrowserAsync(playwright);
        var page = await browser.NewPageAsync();

        await page.GotoAsync(E2ETestContext.BaseUrl);
        await page.WaitForSelectorAsync("[data-testid='map-view']");

        await page.ClickAsync("[data-testid='nav-table-view']");
        await page.WaitForSelectorAsync("[data-testid='minerals-table']");

        var rows = page.Locator("[data-testid='mineral-table-row']");
        var initialRowCount = await rows.CountAsync();

        await page.ClickAsync("[data-testid='add-mineral-button']");

        await page.WaitForFunctionAsync(
            @"expectedCount => document.querySelectorAll('[data-testid=""mineral-table-row""]').length === expectedCount",
            initialRowCount + 1);

        var firstRow = rows.First;
        var firstRowMineralId = await firstRow.GetAttributeAsync("data-mineral-id");
        var firstRowClass = await firstRow.GetAttributeAsync("class");
        var numberValue = await firstRow.Locator("[data-testid='mineral-number-input']").InputValueAsync();
        var nameValue = await firstRow.Locator("[data-testid='mineral-name-input']").InputValueAsync();

        Assert.Equal("0", firstRowMineralId);
        Assert.Contains("table-row-new", firstRowClass);
        Assert.Equal("0", numberValue);
        Assert.Equal("Neues Mineral", nameValue);
    }

    [Fact]
    [Trait("TestLevel", "E2E")]
    [Trait("TestCase", "E2E-VAL-001")]
    [Trait("Requirement", "R10.4")]
    [Trait("Requirement", "R10.5")]
    public async Task TableView_WhenInvalidDiscoveryYearIsSaved_ShowsValidationMessage()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await E2ETestContext.LaunchBrowserAsync(playwright);
        var page = await browser.NewPageAsync();

        await page.GotoAsync(E2ETestContext.BaseUrl);
        await page.WaitForSelectorAsync("[data-testid='map-view']");

        await page.ClickAsync("[data-testid='nav-table-view']");
        await page.WaitForSelectorAsync("[data-testid='minerals-table']");

        var firstRow = page.Locator("[data-testid='mineral-table-row']").First;
        await firstRow.Locator("[data-testid='mineral-discovery-year-input']").FillAsync("1800");
        await page.ClickAsync("[data-testid='save-minerals-button']");

        var saveStatus = page.Locator("[data-testid='save-status-message']");
        await saveStatus.WaitForAsync();

        var saveStatusText = await saveStatus.InnerTextAsync();

        Assert.Contains("Fundjahr muss zwischen", saveStatusText);
    }
}
