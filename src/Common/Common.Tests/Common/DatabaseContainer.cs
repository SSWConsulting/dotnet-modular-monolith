using Testcontainers.MsSql;

namespace Common.Tests.Common;

/// <summary>
/// Wrapper for SQL edge container
/// </summary>
public class DatabaseContainer
{
    private readonly MsSqlContainer _container = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-CU14-ubuntu-22.04")
        .WithName($"BizCover-IntegrationTests-{Guid.NewGuid()}")
        .WithPassword("Password123")
        .WithPortBinding(1433, true)
        .WithAutoRemove(true)
        .Build();

    private const int MaxRetries = 5;

    public string? ConnectionString { get; private set; }

    public async Task InitializeAsync()
    {
        await StartWithRetry();
        ConnectionString = _container.GetConnectionString();
    }

    private async Task StartWithRetry()
    {
        // NOTE: For some reason the container sometimes fails to start up.  Add in a retry to protect against this
        var notReady = true;
        var numTries = 0;

        while (notReady)
        {
            if (numTries >= MaxRetries)
            {
                Console.WriteLine("Max tries reached, giving up");
                break;
            }

            try
            {
                await _container.StartAsync();
                notReady = false;
            }
            catch (Exception ex)
            {
                numTries++;
                await Task.Delay(2000);
                Console.WriteLine($"container failed to start: {ex.Message}");
            }
        }
    }

    public async Task DisposeAsync()
    {
        await _container.StopAsync();
        await _container.DisposeAsync();
    }
}