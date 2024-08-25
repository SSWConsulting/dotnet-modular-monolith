using Ardalis.Specification.EntityFrameworkCore;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Modules.Warehouse.Storage.Domain;
using Modules.Warehouse.Storage.UseCases;
using Modules.Warehouse.Tests.Common;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace Modules.Warehouse.Tests.Storage;

public class AisleIntegrationTests (TestingDatabaseFixture fixture, ITestOutputHelper output)
    : IntegrationTestBase(fixture, output)
{
    [Fact]
    public async Task CreateAisle_ValidRequest_ReturnsCreatedAisle()
    {
        // Arrange
        var client = GetAnonymousClient();
        var request = new CreateAisle.CreateAisleCommand("Name", 2, 2);

        // Act
        var response = await client.PostAsJsonAsync("/api/aisles", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var aisles = await GetQueryable<Aisle>().WithSpecification(new GetAllAislesSpec()).ToListAsync();
        aisles.Should().HaveCount(1);

        var aisle = aisles.First();
        aisle.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        aisle.CreatedBy.Should().NotBeNullOrWhiteSpace();
        aisle.Name.Should().Be(request.Name);
        aisle.Bays.Count.Should().Be(request.NumBays);

        var shelves = aisles.First().Bays.SelectMany(b => b.Shelves).ToList();
        shelves.Count.Should().Be(request.NumBays * request.NumShelves);
    }

    [Theory]
    [InlineData("name", 0, 0)]
    [InlineData("name", 0, 1)]
    [InlineData("name", 1, 0)]
    [InlineData("", 1, 1)]
    [InlineData(" ", 1, 1)]
    [InlineData(null, 1, 1)]
    public async Task CreateAisle_WithInvalidRequest_Throws(string name, int numBays, int numShelves)
    {
        // Arrange
        var client = GetAnonymousClient();
        var request = new CreateAisle.CreateAisleCommand(name, numBays, numShelves);

        // Act
        var response = await client.PostAsJsonAsync("/api/aisles", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        output.WriteLine(content);
    }
}
