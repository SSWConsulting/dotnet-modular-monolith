using Testcontainers.SqlEdge;

namespace Common.Tests.Common;

/// <summary>
/// Wrapper for SQL edge container
/// </summary>
public class DatabaseContainer
{
    // private static readonly int _exposedPort = Random.Shared.Next(10000, 60000);
    // private static readonly int _exposedPort = 20_000;
    // private static readonly int _internalPort = 1433;

    private readonly SqlEdgeContainer _container = new SqlEdgeBuilder()
        .WithName("Modular-Monolith-IntegrationTests-DbContainer")
        .WithPassword("Password123")
        .WithAutoRemove(true)
        // .WithPortBinding(_internalPort)
        // // .WithExposedPort(_exposedPort)
        // .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(_internalPort))
        .Build();

    public string? ConnectionString { get; private set; }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        ConnectionString = _container.GetConnectionString();
    }

    public Task DisposeAsync() => _container.StopAsync() ?? Task.CompletedTask;
}
