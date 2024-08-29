using Common.Tests.Extensions;
using Meziantou.Extensions.Logging.Xunit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi;
using Xunit.Abstractions;

namespace Common.Tests.Common;

/// <summary>
/// Host builder (services, DI, and configuration) for integration tests
/// </summary>
public class WebApiTestFactory<TDbContext> : WebApplicationFactory<IWebApiMarker> where TDbContext : DbContext
{
    public DatabaseContainer Database { get; } = new(typeof(TDbContext).Name);

    public ITestOutputHelper? Output { private get; set; }

    public Lazy<HttpClient> AnonymousClient => new(CreateClient());

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Redirect application logging to test output
        builder.ConfigureLogging(x =>
        {
            x.ClearProviders();
            x.AddFilter(level => level >= LogLevel.Information);
            x.Services.AddSingleton<ILoggerProvider>(new XUnitLoggerProvider(Output!));
        });

        // Override default DB registration to use out Test Container instead
        builder.ConfigureTestServices(services =>
        {
            services.ReplaceDbContext<TDbContext>(Database);
        });
    }
}
