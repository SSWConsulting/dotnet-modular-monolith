﻿using Common.SharedKernel.Persistence.Interceptors;
using Common.Tests.Common;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Modules.Warehouse.Common.Persistence.Interceptors;
using System.Diagnostics;

namespace Common.Tests.Extensions;

internal static class ServiceCollectionExt
{
    /// <summary>
    /// Replaces the DbContext with a new instance using the provided database container
    /// </summary>
    internal static IServiceCollection ReplaceDbContext<T>(
        this IServiceCollection services,
        DatabaseContainer databaseContainer) where T : DbContext
    {
        services
            .RemoveAll<DbContextOptions<T>>()
            .RemoveAll<T>()
            .AddDbContextPool<T>((_, options) =>
            {
                options.UseSqlServer(databaseContainer.ConnectionString,
                    b => b.MigrationsAssembly(typeof(T).Assembly.FullName));

                options.LogTo(m => Debug.WriteLine(m));
                options.EnableSensitiveDataLogging();

                var serviceProvider = services.BuildServiceProvider();

                options.AddInterceptors(
                    serviceProvider.GetRequiredService<EntitySaveChangesInterceptor>(),
                    serviceProvider.GetRequiredService<DispatchDomainEventsInterceptor>()
                );

                options.UseExceptionProcessor();
            });

        return services;
    }
}
