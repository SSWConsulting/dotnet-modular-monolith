using Common.SharedKernel.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Common.SharedKernel;

public static class DependencyInjection
{
    public static void AddCommon(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSingleton(TimeProvider.System);
    }
}
