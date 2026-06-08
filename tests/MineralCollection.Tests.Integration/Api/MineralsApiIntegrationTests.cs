using System.Net;
using System.Net.Http.Json;
using MineralCollection.Domain;
using MineralCollection.Tests.Integration.Infrastructure;

namespace MineralCollection.Tests.Integration.Api;

public class MineralsApiIntegrationTests
{
    [Fact]
    [Trait("TestLevel", "Integration")]
    [Trait("TestCase", "ITC-API-001")]
    [Trait("Requirement", "R9.2")]
    [Trait("Requirement", "R11.3")]
    public async Task PostMineral_WhenMineralIsValid_PersistsMineral()
    {
        await using var factory = new MineralApiFactory();
        var client = factory.CreateClient();
        var mineral = new Mineral
        {
            Name = "Quarz",
            Nummer = "1",
            Fundort = "Freiberg",
            Region = "Sachsen",
            Land = "Deutschland"
        };

        var postResponse = await client.PostAsJsonAsync("/api/minerals", mineral);
        var createdMineral = await postResponse.Content.ReadFromJsonAsync<Mineral>();
        var getResponse = await client.GetAsync($"/api/minerals/{createdMineral!.Id}");
        var persistedMineral = await getResponse.Content.ReadFromJsonAsync<Mineral>();

        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        Assert.NotEqual(0, createdMineral.Id);
        Assert.Equal("Quarz", persistedMineral!.Name);
        Assert.Equal("Freiberg", persistedMineral.Fundort);
    }

    [Fact]
    [Trait("TestLevel", "Integration")]
    [Trait("TestCase", "ITC-API-002")]
    [Trait("Requirement", "R9.3")]
    [Trait("Requirement", "R11.3")]
    public async Task PutMineral_WhenMineralExists_UpdatesMineral()
    {
        await using var factory = new MineralApiFactory();
        var client = factory.CreateClient();
        var mineral = new Mineral { Name = "Quarz", Fundort = "Freiberg" };
        var postResponse = await client.PostAsJsonAsync("/api/minerals", mineral);
        var createdMineral = await postResponse.Content.ReadFromJsonAsync<Mineral>();

        createdMineral!.Name = "Amethyst";
        createdMineral.Fundort = "Idar-Oberstein";
        var putResponse = await client.PutAsJsonAsync($"/api/minerals/{createdMineral.Id}", createdMineral);
        var persistedMineral = await client.GetFromJsonAsync<Mineral>($"/api/minerals/{createdMineral.Id}");

        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);
        Assert.Equal("Amethyst", persistedMineral!.Name);
        Assert.Equal("Idar-Oberstein", persistedMineral.Fundort);
    }

    [Fact]
    [Trait("TestLevel", "Integration")]
    [Trait("TestCase", "ITC-API-003")]
    [Trait("Requirement", "R9.8")]
    [Trait("Requirement", "R11.3")]
    public async Task DeleteMineral_WhenMineralExists_RemovesMineral()
    {
        await using var factory = new MineralApiFactory();
        var client = factory.CreateClient();
        var mineral = new Mineral { Name = "Quarz", Fundort = "Freiberg" };
        var postResponse = await client.PostAsJsonAsync("/api/minerals", mineral);
        var createdMineral = await postResponse.Content.ReadFromJsonAsync<Mineral>();

        var deleteResponse = await client.DeleteAsync($"/api/minerals/{createdMineral!.Id}");
        var getResponse = await client.GetAsync($"/api/minerals/{createdMineral.Id}");

        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
}
