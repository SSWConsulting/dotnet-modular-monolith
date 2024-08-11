using FluentAssertions;
using Modules.Warehouse.Storage.Domain;

namespace Modules.Warehouse.Tests;

public class AisleTests
{
    [Fact]
    public void Create_WithBaysAndShelves_CreatesCorrectNumberOfShelves()
    {
        // Arrange
        var numBays = 2;
        var numShelves = 3;

        // Act
        var sut = Aisle.Create("Aisle 1", numBays, numShelves);

        // Assert
        sut.TotalStorage.Should().Be(numBays * numShelves);
    }
}
