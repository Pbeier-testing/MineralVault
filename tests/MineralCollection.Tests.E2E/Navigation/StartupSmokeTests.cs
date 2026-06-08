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
        var baseUrl = Environment.GetEnvironmentVariable("MINERALVAULT_E2E_BASE_URL")
            ?? "http://localhost:5119";

        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });
        var page = await browser.NewPageAsync();

        await page.GotoAsync(baseUrl);
        var map = await page.WaitForSelectorAsync("#map", new PageWaitForSelectorOptions
        {
            Timeout = 10000
        });

        Assert.NotNull(map);
        await AssertContainsTextAsync(page, "Funde");
    }

    private static async Task AssertContainsTextAsync(IPage page, string text)
    {
        var content = await page.ContentAsync();
        Assert.Contains(text, content);
    }
}
