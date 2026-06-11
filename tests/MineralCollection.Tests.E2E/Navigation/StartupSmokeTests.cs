using Microsoft.Playwright;

namespace MineralCollection.Tests.E2E.Navigation;

public class StartupSmokeTests
{
    [Fact]
    [Trait("TestLevel", "E2E")]
    [Trait("TestCase", "E2E-NAV-001")]
    [Trait("Requirement", "R1.1")]
    [Trait("Requirement", "R1.2")]
    public async Task HomePage_WhenApplicationStarts_ShowsMapView()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await LaunchBrowserAsync(playwright);
        var page = await browser.NewPageAsync();

        await page.GotoAsync(GetBaseUrl());
        var mapView = await page.WaitForSelectorAsync("[data-testid='map-view']", new PageWaitForSelectorOptions
        {
            Timeout = 10000
        });
        var mineralCount = await page.WaitForSelectorAsync("[data-testid='mineral-count']");

        Assert.NotNull(mapView);
        Assert.NotNull(mineralCount);
    }

    [Fact]
    [Trait("TestLevel", "E2E")]
    [Trait("TestCase", "E2E-NAV-002")]
    [Trait("Requirement", "R1.3")]
    [Trait("Requirement", "R5.1")]
    public async Task Navigation_WhenTableAndMapButtonsAreClicked_SwitchesBetweenViews()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await LaunchBrowserAsync(playwright);
        var page = await browser.NewPageAsync();

        await page.GotoAsync(GetBaseUrl());
        await page.WaitForSelectorAsync("[data-testid='map-view']");

        await page.ClickAsync("[data-testid='nav-table-view']");
        var tableView = await page.WaitForSelectorAsync("[data-testid='table-view']", new PageWaitForSelectorOptions
        {
            Timeout = 10000
        });

        Assert.NotNull(tableView);

        await page.ClickAsync("[data-testid='nav-map-view']");
        var mapView = await page.WaitForSelectorAsync("[data-testid='map-view']", new PageWaitForSelectorOptions
        {
            Timeout = 10000
        });

        Assert.NotNull(mapView);
    }

    private static string GetBaseUrl()
    {
        return Environment.GetEnvironmentVariable("MINERALVAULT_E2E_BASE_URL")
            ?? "http://localhost:5119";
    }

    private static Task<IBrowser> LaunchBrowserAsync(IPlaywright playwright)
    {
        return playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });
    }
}
