using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Modules.Warehouse.Common.Persistence;
using Xunit.Abstractions;

namespace Modules.Warehouse.Tests.Common;

/// <summary>
/// Integration tests inherit from this to access helper classes
/// </summary>
[Collection(TestingDatabaseFixtureCollection.Name)]
public abstract class IntegrationTestBase : IAsyncLifetime
{
    private readonly IServiceScope _scope;

    private readonly TestingDatabaseFixture _fixture;

    protected IMediator Mediator { get; }

    protected IQueryable<T> GetQueryable<T>() where T : class => DbContext.Set<T>().AsNoTracking();

    internal WarehouseDbContext DbContext { get; }

    protected IntegrationTestBase(TestingDatabaseFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _fixture.SetOutput(output);

        _scope = _fixture.ScopeFactory.CreateScope();
        Mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        DbContext = _scope.ServiceProvider.GetRequiredService<WarehouseDbContext>();
    }


    protected async Task AddEntityAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
    {
        await DbContext.Set<T>().AddAsync(entity, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    // protected async Task AddEntitiesAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : class
    // {
    //     await Context.Set<T>().AddRangeAsync(entities, cancellationToken);
    //     await Context.SaveChangesAsync(cancellationToken);
    // }

    /// <summary>
    /// Gets called between each test to reset the state of the database
    /// </summary>
    public async Task InitializeAsync()
    {
        await _fixture.ResetState();
    }

    protected HttpClient GetAnonymousClient() => _fixture.Factory.AnonymousClient.Value;

    public Task DisposeAsync()
    {
        _scope.Dispose();
        return Task.CompletedTask;
    }
}
