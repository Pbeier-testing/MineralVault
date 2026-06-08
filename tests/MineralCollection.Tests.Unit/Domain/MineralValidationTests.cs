using System.ComponentModel.DataAnnotations;
using MineralCollection.Domain;

namespace MineralCollection.Tests.Unit.Domain;

public class MineralValidationTests
{
    [Fact]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-VAL-001")]
    [Trait("Requirement", "R10.1")]
    [Trait("Requirement", "R10.4")]
    public void Validate_WhenNameIsEmpty_ReturnsRequiredError()
    {
        var mineral = new Mineral { Name = "" };

        var errors = Validate(mineral);

        Assert.Contains(errors, error => error.ErrorMessage == "Der Name des Hauptminerals ist Pflicht.");
    }

    [Theory]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-VAL-002")]
    [Trait("Requirement", "R10.2")]
    [InlineData(-90.0)]
    [InlineData(90.0)]
    [InlineData(null)]
    public void Validate_WhenLatitudeIsInsideAllowedRange_ReturnsNoLatitudeError(double? latitude)
    {
        var mineral = new Mineral { Name = "Quarz", Breitengrad = latitude };

        var errors = Validate(mineral);

        Assert.DoesNotContain(errors, error => error.MemberNames.Contains(nameof(Mineral.Breitengrad)));
    }

    [Theory]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-VAL-003")]
    [Trait("Requirement", "R10.2")]
    [Trait("Requirement", "R10.4")]
    [InlineData(-90.1)]
    [InlineData(90.1)]
    public void Validate_WhenLatitudeIsOutsideAllowedRange_ReturnsLatitudeError(double latitude)
    {
        var mineral = new Mineral { Name = "Quarz", Breitengrad = latitude };

        var errors = Validate(mineral);

        Assert.Contains(errors, error => error.MemberNames.Contains(nameof(Mineral.Breitengrad)));
    }

    [Theory]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-VAL-004")]
    [Trait("Requirement", "R10.3")]
    [InlineData(-180.0)]
    [InlineData(180.0)]
    [InlineData(null)]
    public void Validate_WhenLongitudeIsInsideAllowedRange_ReturnsNoLongitudeError(double? longitude)
    {
        var mineral = new Mineral { Name = "Quarz", Laengengrad = longitude };

        var errors = Validate(mineral);

        Assert.DoesNotContain(errors, error => error.MemberNames.Contains(nameof(Mineral.Laengengrad)));
    }

    [Theory]
    [Trait("TestLevel", "Unit")]
    [Trait("TestCase", "UTC-VAL-005")]
    [Trait("Requirement", "R10.3")]
    [Trait("Requirement", "R10.4")]
    [InlineData(-180.1)]
    [InlineData(180.1)]
    public void Validate_WhenLongitudeIsOutsideAllowedRange_ReturnsLongitudeError(double longitude)
    {
        var mineral = new Mineral { Name = "Quarz", Laengengrad = longitude };

        var errors = Validate(mineral);

        Assert.Contains(errors, error => error.MemberNames.Contains(nameof(Mineral.Laengengrad)));
    }

    private static List<ValidationResult> Validate(Mineral mineral)
    {
        var context = new ValidationContext(mineral);
        var results = new List<ValidationResult>();

        Validator.TryValidateObject(mineral, context, results, validateAllProperties: true);

        return results;
    }
}
