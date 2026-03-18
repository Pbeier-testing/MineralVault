using MineralCollection.Domain;
using Xunit;

namespace MineralCollection.Tests.Unit;

public class MineralTests
{
    [Fact] 
    public void Mineral_ShouldSetPropertiesCorrectly()
    {
        // 1. Arrange (Vorbereiten)
        var name = "Amethyst";
        var location = "Brasilien";

        // 2. Act (Ausführen)
        var mineral = new Mineral 
        { 
            Name = name, 
            Fundort = location 
        };

        // 3. Assert (Überprüfen)
        Assert.Equal("Amethyst", mineral.Name);
        Assert.Equal("Brasilien", mineral.Fundort);
    }

    [Fact]
     public void Mineral_WithoutName_ShouldBeInvalid()
    {
        // 1. Arrange (Vorbereiten)
        var mineral = new Mineral { Name = "", Fundort = "Alpen" };

        // 2. Act (Ausführen)
        bool hasName = !string.IsNullOrWhiteSpace(mineral.Name);

        // 3. Assert (Überprüfen)
        Assert.False(hasName, "Ein Mineral sollte einen Namen haben müssen.");
    }
}