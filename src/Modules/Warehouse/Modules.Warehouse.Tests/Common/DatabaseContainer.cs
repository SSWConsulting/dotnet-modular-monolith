using Testcontainers.SqlEdge;

namespace Modules.Warehouse.Tests.Common;

/// <summary>
/// Wraper for SQL edge container
/// </summary>
public class DatabaseContainer
{
    private readonly SqlEdgeContainer _container = new SqlEdgeBuilder()
        .WithName("Modular-Monolith-IntegrationTests-DbContainer")
        .WithPassword("Password123")
        .WithAutoRemove(true)
        .Build();

    public string? ConnectionString { get; private set; }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        ConnectionString = _container.GetConnectionString();
    }

    public Task DisposeAsync() => _container.StopAsync() ?? Task.CompletedTask;
}
