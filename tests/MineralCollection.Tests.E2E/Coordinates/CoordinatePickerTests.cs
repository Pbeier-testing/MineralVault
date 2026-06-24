using Microsoft.Playwright;
using MineralCollection.Tests.E2E.Infrastructure;

namespace MineralCollection.Tests.E2E.Coordinates;

public class CoordinatePickerTests
{
    [Fact]
    [Trait("TestLevel", "E2E")]
    [Trait("TestCase", "E2E-COORD-001")]
    [Trait("Requirement", "R8.2")]
    [Trait("Requirement", "R8.5")]
    public async Task CoordinatePicker_WhenCancelledAfterNewSelection_KeepsOriginalCoordinates()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await E2ETestContext.LaunchBrowserAsync(playwright);
        var page = await browser.NewPageAsync();

        await page.GotoAsync(E2ETestContext.BaseUrl);
        await page.WaitForSelectorAsync("[data-testid='map-view']");

        await page.ClickAsync("[data-testid='nav-table-view']");
        await page.WaitForSelectorAsync("[data-testid='minerals-table']");

        var firstRow = page.Locator("[data-testid='mineral-table-row']").First;
        var latitudeInput = firstRow.Locator("[data-testid='mineral-latitude-input']");
        var longitudeInput = firstRow.Locator("[data-testid='mineral-longitude-input']");

        var originalLatitude = await latitudeInput.InputValueAsync();
        var originalLongitude = await longitudeInput.InputValueAsync();

        await firstRow.Locator("[data-testid='open-coordinate-picker-button']").ClickAsync();
        await page.WaitForSelectorAsync("[data-testid='coordinate-picker-modal']");
        await page.WaitForFunctionAsync("() => window.mapBox?.pickerMap !== undefined");

        var originalPickerValue = await page.Locator("[data-testid='coordinate-picker-value']").InnerTextAsync();
        var pickerMapBox = await page.Locator("[data-testid='coordinate-picker-map']").BoundingBoxAsync();
        Assert.NotNull(pickerMapBox);

        await page.Mouse.ClickAsync(
            pickerMapBox.X + pickerMapBox.Width * 0.65f,
            pickerMapBox.Y + pickerMapBox.Height * 0.40f);

        await WaitForPickerValueToChangeAsync(page, originalPickerValue);

        Assert.Equal(originalLatitude, await latitudeInput.InputValueAsync());
        Assert.Equal(originalLongitude, await longitudeInput.InputValueAsync());

        await page.ClickAsync("[data-testid='coordinate-picker-cancel-button']");
        await page.WaitForSelectorAsync("[data-testid='coordinate-picker-modal']", new PageWaitForSelectorOptions
        {
            State = WaitForSelectorState.Detached
        });

        Assert.Equal(originalLatitude, await latitudeInput.InputValueAsync());
        Assert.Equal(originalLongitude, await longitudeInput.InputValueAsync());
    }

    [Fact]
    [Trait("TestLevel", "E2E")]
    [Trait("TestCase", "E2E-COORD-002")]
    [Trait("Requirement", "R8.2")]
    [Trait("Requirement", "R8.4")]
    [Trait("Requirement", "R8.6")]
    public async Task CoordinatePicker_WhenCoordinatesAreSelectedAndApplied_UpdatesTableCoordinates()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await E2ETestContext.LaunchBrowserAsync(playwright);
        var page = await browser.NewPageAsync();

        await page.GotoAsync(E2ETestContext.BaseUrl);
        await page.WaitForSelectorAsync("[data-testid='map-view']");

        await page.ClickAsync("[data-testid='nav-table-view']");
        await page.WaitForSelectorAsync("[data-testid='minerals-table']");

        var firstRow = page.Locator("[data-testid='mineral-table-row']").First;
        var latitudeInput = firstRow.Locator("[data-testid='mineral-latitude-input']");
        var longitudeInput = firstRow.Locator("[data-testid='mineral-longitude-input']");
        var originalLatitude = await latitudeInput.InputValueAsync();
        var originalLongitude = await longitudeInput.InputValueAsync();

        await firstRow.Locator("[data-testid='open-coordinate-picker-button']").ClickAsync();
        await page.WaitForSelectorAsync("[data-testid='coordinate-picker-modal']");
        await page.WaitForFunctionAsync("() => window.mapBox?.pickerMap !== undefined");

        var originalPickerValue = await page.Locator("[data-testid='coordinate-picker-value']").InnerTextAsync();
        var pickerMapBox = await page.Locator("[data-testid='coordinate-picker-map']").BoundingBoxAsync();
        Assert.NotNull(pickerMapBox);

        await page.Mouse.ClickAsync(
            pickerMapBox.X + pickerMapBox.Width * 0.65f,
            pickerMapBox.Y + pickerMapBox.Height * 0.40f);

        await WaitForPickerValueToChangeAsync(page, originalPickerValue);

        var selectedPickerValue = await page.Locator("[data-testid='coordinate-picker-value']").InnerTextAsync();

        Assert.NotEqual(originalPickerValue, selectedPickerValue);
        Assert.Equal(originalLatitude, await latitudeInput.InputValueAsync());
        Assert.Equal(originalLongitude, await longitudeInput.InputValueAsync());

        await page.ClickAsync("[data-testid='coordinate-picker-apply-button']");
        await page.WaitForSelectorAsync("[data-testid='coordinate-picker-modal']", new PageWaitForSelectorOptions
        {
            State = WaitForSelectorState.Detached
        });

        Assert.NotEqual(originalLatitude, await latitudeInput.InputValueAsync());
        Assert.NotEqual(originalLongitude, await longitudeInput.InputValueAsync());
    }

    private static async Task WaitForPickerValueToChangeAsync(IPage page, string originalPickerValue)
    {
        await page.WaitForFunctionAsync(
            @"originalValue => {
                const element = document.querySelector('[data-testid=""coordinate-picker-value""]');
                return element?.textContent?.trim() !== originalValue.trim();
            }",
            originalPickerValue);
    }
}
