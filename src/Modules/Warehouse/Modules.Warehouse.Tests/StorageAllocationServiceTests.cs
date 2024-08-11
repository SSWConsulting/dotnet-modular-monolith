using FluentAssertions;
using Modules.Warehouse.Products.Domain;
using Modules.Warehouse.Storage.Domain;

namespace Module.Warehouse.Tests;

public class StorageAllocationServiceTests
{
    [Fact]
    public void AllocateStorage_WhenMaxStorageUsed_ShouldHaveNoAvailableStorage()
    {
       // Arrange
       var numBays = 2;
       var numShelves = 3;
       var aisle = Aisle.Create("Aisle 1", numBays, numShelves);
       var sut = new StorageAllocationService();
       var productId = new ProductId(Guid.NewGuid());

       // Act
       for(var i = 0; i < numBays * numShelves; i++)
       {
            StorageAllocationService.AllocateStorage([aisle], productId);
       }

       // Assert
       aisle.TotalStorage.Should().Be(numBays * numShelves);
       aisle.AvailableStorage.Should().Be(0);
    }

    [Fact]
    public void AllocateStorage_ShouldThrowException_WhenNoEmptyShelf()
    {
        // Arrange
        var numBays = 2;
        var numShelves = 3;
        var aisle = Aisle.Create("Aisle 1", numBays, numShelves);
        var sut = new StorageAllocationService();
        var productId = new ProductId(Guid.NewGuid());

        // Act
        for(var i = 0; i < numBays * numShelves; i++)
        {
            StorageAllocationService.AllocateStorage([aisle], productId);
        }

        // Assert
        var act = () => StorageAllocationService.AllocateStorage([aisle], productId);
        act.Should().Throw<Exception>();
    }
}
