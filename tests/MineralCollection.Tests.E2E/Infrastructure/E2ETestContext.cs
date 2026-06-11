using Microsoft.Playwright;

namespace MineralCollection.Tests.E2E.Infrastructure;

internal static class E2ETestContext
{
    public static string BaseUrl =>
        Environment.GetEnvironmentVariable("MINERALVAULT_E2E_BASE_URL")
        ?? "http://localhost:5119";

    public static Task<IBrowser> LaunchBrowserAsync(IPlaywright playwright)
    {
        return playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });
    }
}
