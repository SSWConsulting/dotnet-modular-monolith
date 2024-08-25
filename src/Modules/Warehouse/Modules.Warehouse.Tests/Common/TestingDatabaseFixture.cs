using Microsoft.Extensions.DependencyInjection;
using Modules.Warehouse.Common.Persistence;
using Respawn;
using Xunit.Abstractions;

namespace Modules.Warehouse.Tests.Common;

/// <summary>
/// Initializes and resets the database before and after each test
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class TestingDatabaseFixture : IAsyncLifetime
{
    private string ConnectionString => Factory.Database.ConnectionString!;

    private Respawner _checkpoint = default!;

    public IServiceScopeFactory ScopeFactory { get; private set; } = null!;

    public WebApiTestFactory Factory { get; } = new();

    public async Task InitializeAsync()
    {
        // Initialize DB Container
        await Factory.Database.InitializeAsync();
        ScopeFactory = Factory.Services.GetRequiredService<IServiceScopeFactory>();

        // Create and seed database
        using var scope = ScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WarehouseDbContext>();
        await dbContext.Database.EnsureCreatedAsync();

        // NOTE: If there are any tables you want to skip being reset, they can be configured here
        _checkpoint = await Respawner.CreateAsync(ConnectionString);
    }

    public async Task DisposeAsync()
    {
        await Factory.Database.DisposeAsync();
    }

    public async Task ResetState()
    {
        await _checkpoint.ResetAsync(ConnectionString);
    }

    public void SetOutput(ITestOutputHelper output) => Factory.Output = output;
}

[CollectionDefinition(Name)]
public class TestingDatabaseFixtureCollection : ICollectionFixture<TestingDatabaseFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.

    public const string Name = nameof(TestingDatabaseFixtureCollection);
}
