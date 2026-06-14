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
}
