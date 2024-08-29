using Testcontainers.SqlEdge;

namespace Common.Tests.Common;

/// <summary>
/// Wrapper for SQL edge container
/// </summary>
public class DatabaseContainer
{
    private readonly SqlEdgeContainer _container;

    public DatabaseContainer(string name)
    {
        _container = new SqlEdgeBuilder()
            .WithName($"Modular-Monolith-Tests-{name}")
            .WithPassword("Password123")
            .WithAutoRemove(true)
            .Build();
    }

    public string? ConnectionString { get; private set; }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        ConnectionString = _container.GetConnectionString();
    }

    public Task DisposeAsync() => _container.StopAsync() ?? Task.CompletedTask;
}
