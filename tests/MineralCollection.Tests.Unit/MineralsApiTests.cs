using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using MineralCollection.Domain;
using Xunit;

namespace MineralCollection.Tests.Unit;

// Diese Testklasse überprüft die Endpunkte der Minerals API
public class MineralsApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public MineralsApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_MineralWithoutName_ReturnsBadRequest()
    {
        // 1. Arrange: Ein Mineral ohne Namen vorbereiten
        var invalidMineral = new Mineral
        {
            Name = "", // FEHLER: Name ist leer
            Fundort = "Test-Ort"
        };

        // 2. Act: Den Request an die API senden
        var response = await _client.PostAsJsonAsync("/api/minerals", invalidMineral);

        // 3. Assert: Prüfen, ob die API den Fehler 400 (Bad Request) zurückgibt
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Put_UpdateExistingMineral_ReturnsNoContent()
    {
        // 1. Arrange: Ein Mineral anlegen und die ID abrufen
        var initialMineral = new Mineral { Name = "Original", Fundort = "Erde" };
        var postResponse = await _client.PostAsJsonAsync("/api/minerals", initialMineral);
        var createdMineral = await postResponse.Content.ReadFromJsonAsync<Mineral>();

        // 2. Act: Daten ändern und Update-Request senden
        createdMineral!.Name = "Updated Name";
        var putResponse = await _client.PutAsJsonAsync($"/api/minerals/{createdMineral.Id}", createdMineral);

        // 3. Assert: Validierung der Ergebnisse
        Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

        // Verifizieren, dass die Änderung in der DB angekommen ist
        var getResponse = await _client.GetFromJsonAsync<Mineral>($"/api/minerals/{createdMineral.Id}");
        Assert.NotNull(getResponse);
        Assert.Equal("Updated Name", getResponse.Name);
    }

}