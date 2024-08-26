using FluentAssertions;
using Modules.Warehouse.Products.Domain;
using Modules.Warehouse.Storage.Domain;
using Modules.Warehouse.Storage.UseCases;
using Modules.Warehouse.Tests.Common;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace Modules.Warehouse.Tests.Storage;

public class StorageAllocationIntegrationTests (TestingDatabaseFixture fixture, ITestOutputHelper output)
    : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task AllocateStorage_ValidRequest_ReturnsOk()
    {
        // Arrange
        var client = GetAnonymousClient();
        var product = Product.Create("Name", Sku.Create("12345678"));
        await AddEntityAsync(product);
        var aisle = Aisle.Create("Name", 2, 2);
        await AddEntityAsync(aisle);
        var request = new AllocateStorageCommand.Request(product.Id.Value);

        // Act
        var response = await client.PostAsJsonAsync("/api/aisles/allocate-storage", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
