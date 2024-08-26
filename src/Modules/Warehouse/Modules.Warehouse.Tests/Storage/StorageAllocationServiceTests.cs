using FluentAssertions;
using Modules.Warehouse.Products.Domain;
using Modules.Warehouse.Storage.Domain;

namespace Modules.Warehouse.Tests.Storage
{
    public class StorageAllocationServiceTests
    {
        [Fact]
        public void AllocateStorage_ShouldAssignProductToFirstEmptyShelf()
        {
            // Arrange
            var productId = new ProductId(Guid.NewGuid());
            var aisles = new List<Aisle>
            {
                Aisle.Create("name", 2, 2)
            };

            // Act
            var result = StorageAllocationService.AllocateStorage(aisles, productId);

            // Assert
            result.IsError.Should().BeFalse();
            aisles[0].Bays[0].Shelves[0].ProductId.Should().Be(productId);
        }

        [Fact]
        public void AllocateStorage_ShouldThrowException_WhenNoEmptyShelfIsAvailable()
        {
            // Arrange
            var productId = new ProductId(Guid.NewGuid());
            var aisles = new List<Aisle>
            {
                Aisle.Create("name", 1, 1)
            };
            StorageAllocationService.AllocateStorage(aisles, productId);

            // Act
            var result = StorageAllocationService.AllocateStorage(aisles, productId);

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Should().Be(StorageAllocationErrors.NoAvailableStorage);
        }
    }
}
