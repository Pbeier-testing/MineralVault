using System.ComponentModel.DataAnnotations;

namespace MineralCollection.Domain;

public class Mineral
{
    public int Id { get; set; }
    public string? Nummer { get; set; }

    [Required(ErrorMessage = "Der Name des Hauptminerals ist Pflicht.")]
    public required string Name { get; set; }

    public string? Begleitmineral { get; set; }
    public string? Fundort { get; set; }
    public string? Region { get; set; }
    public string? Land { get; set; }


    [Range(-90, 90, ErrorMessage = "Breitengrad muss zwischen -90 und 90 liegen.")]
    public double? Breitengrad { get; set; }

    [Range(-180, 180, ErrorMessage = "Längengrad muss zwischen -180 und 180 liegen.")]
    public double? Laengengrad { get; set; }

    public string? Bemerkungen { get; set; }


    public List<MineralImage> Images { get; set; } = new();
}

public class MineralImage
{
    public int Id { get; set; }
    public string? FileName { get; set; }
    public string? Caption { get; set; }
    public int MineralId { get; set; } // Fremdschlüssel
}