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
}