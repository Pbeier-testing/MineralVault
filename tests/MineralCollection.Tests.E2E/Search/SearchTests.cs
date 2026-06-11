using Microsoft.Playwright;
using MineralCollection.Tests.E2E.Infrastructure;

namespace MineralCollection.Tests.E2E.Search;

public class SearchTests
{
    [Fact]
    [Trait("TestLevel", "E2E")]
    [Trait("TestCase", "E2E-SEARCH-001")]
    [Trait("Requirement", "R4.1")]
    [Trait("Requirement", "R4.3")]
    [Trait("Requirement", "R4.8")]
    [Trait("Requirement", "R4.10")]
    public async Task MapSearch_WhenSearchTermMatchesMineral_FiltersCardListAndUpdatesCount()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await E2ETestContext.LaunchBrowserAsync(playwright);
        var page = await browser.NewPageAsync();

        await page.GotoAsync(E2ETestContext.BaseUrl);
        await page.WaitForSelectorAsync("[data-testid='map-view']");
        await page.WaitForSelectorAsync("[data-testid='mineral-card']");

        var initialCount = await ReadMineralCountAsync(page);
        Assert.True(initialCount > 1);

        await page.FillAsync("[data-testid='map-search-input']", "Coelestin");
        await WaitForMineralCountAsync(page, 1);

        var filteredCount = await page.Locator("[data-testid='mineral-card']").CountAsync();
        var matchingCards = await page.Locator("[data-testid='mineral-card']", new PageLocatorOptions
        {
            HasTextString = "Coelestin"
        }).CountAsync();

        Assert.Equal(1, filteredCount);
        Assert.Equal(1, matchingCards);

        await page.FillAsync("[data-testid='map-search-input']", string.Empty);
        await WaitForMineralCountAsync(page, initialCount);
    }

    private static async Task<int> ReadMineralCountAsync(IPage page)
    {
        var text = await page.Locator("[data-testid='mineral-count']").InnerTextAsync();
        var countText = text.Split(' ', StringSplitOptions.RemoveEmptyEntries).First();

        return int.Parse(countText);
    }

    private static async Task WaitForMineralCountAsync(IPage page, int expectedCount)
    {
        await page.WaitForFunctionAsync(
            @"([testId, expected]) => {
                const element = document.querySelector(`[data-testid='${testId}']`);
                return element?.textContent?.trim().startsWith(`${expected} `);
            }",
            new object[] { "mineral-count", expectedCount });
    }
}
