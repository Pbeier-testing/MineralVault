namespace MineralCollection.Domain;

public class Mineral
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Fundort { get; set; } = string.Empty;
    public DateTime Funddatum { get; set; }
    public string Beschreibung { get; set; } = string.Empty;
    
    // Für die Karte (Bing Maps / Leaflet)
    public double Breitengrad { get; set; }
    public double Laengengrad { get; set; }
    
    // Später für Bilder
    public string? BildUrl { get; set; }
}