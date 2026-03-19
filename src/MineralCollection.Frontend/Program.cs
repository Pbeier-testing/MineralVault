using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MineralCollection.Frontend;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient mit Basisadresse der API registrieren, damit die Blazor App die API erreichen kann
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5247") });

await builder.Build().RunAsync();
