using System.ComponentModel.DataAnnotations;

namespace MineralCollection.Domain;

public class Mineral
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Location is required")]
    public string Fundort { get; set; } = string.Empty;

    public DateTime Funddatum { get; set; }

    public string Beschreibung { get; set; } = string.Empty;
    
    // Geografische Koordinaten
    // Breitengrad sollte zwischen -90 und 90 liegen
    [Range(-90, 90)]
    public double Breitengrad { get; set; }

    // Längengrad sollte zwischen -180 und 180 liegen
    [Range(-180, 180)]
    public double Laengengrad { get; set; }
    
    // Url zum Bild des Minerals
    public string? BildUrl { get; set; }
}