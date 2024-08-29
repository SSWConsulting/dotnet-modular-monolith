using Common.Tests.Common;
using Modules.Warehouse.Common.Persistence;
using Xunit.Abstractions;

namespace Modules.Warehouse.Tests.Common;

// ReSharper disable once ClassNeverInstantiated.Global
public class WarehouseDatabaseFixture : TestingDatabaseFixture<WarehouseDbContext>;

[Collection(TestingDatabaseFixtureCollection.Name)]
public abstract class WarehouseIntegrationTestBase(
    WarehouseDatabaseFixture fixture,
    ITestOutputHelper output)
    : IntegrationTestBase<WarehouseDbContext>(fixture, output);

[CollectionDefinition(Name)]
public class TestingDatabaseFixtureCollection : ICollectionFixture<WarehouseDatabaseFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.

    public const string Name = nameof(TestingDatabaseFixtureCollection);
}
