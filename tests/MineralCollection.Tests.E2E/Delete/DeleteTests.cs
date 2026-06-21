using Microsoft.Playwright;
using MineralCollection.Tests.E2E.Infrastructure;

namespace MineralCollection.Tests.E2E.Delete;

public class DeleteTests
{
    [Fact]
    [Trait("TestLevel", "E2E")]
    [Trait("TestCase", "E2E-DELETE-001")]
    [Trait("Requirement", "R9.5")]
    [Trait("Requirement", "R9.6")]
    public async Task DeleteMineral_WhenConfirmationIsCancelled_KeepsMineralVisible()
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
        var firstRow = rows.First;
        var mineralName = await firstRow.Locator("[data-testid='mineral-name-input']").InputValueAsync();

        var dialogResult = new TaskCompletionSource<string>();
        page.Dialog += async (_, dialog) =>
        {
            dialogResult.SetResult(dialog.Message);
            await dialog.DismissAsync();
        };

        await firstRow.Locator("[data-testid='delete-mineral-button']").ClickAsync();
        var dialogMessage = await dialogResult.Task.WaitAsync(TimeSpan.FromSeconds(5));

        Assert.Contains(mineralName, dialogMessage);
        Assert.Equal(initialRowCount, await rows.CountAsync());
        Assert.Equal(mineralName, await rows.First.Locator("[data-testid='mineral-name-input']").InputValueAsync());
    }
}
