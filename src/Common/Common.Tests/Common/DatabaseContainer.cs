using DotNet.Testcontainers.Builders;
using Testcontainers.SqlEdge;

namespace Common.Tests.Common;

/// <summary>
/// Wrapper for SQL edge container
/// </summary>
public class DatabaseContainer
{
    private readonly SqlEdgeContainer _container = new SqlEdgeBuilder()
        .WithName($"Modular-Monolith-Tests-{Guid.NewGuid()}")
        .WithPassword("Password123")
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
        .WithAutoRemove(true)
        .Build();

    public string? ConnectionString { get; private set; }

    public async Task InitializeAsync()
    {
        try
        {
            await _container.StartAsync();
            ConnectionString = _container.GetConnectionString();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task DisposeAsync()
    {
        await _container.StopAsync();
        await _container.DisposeAsync();
    }
}