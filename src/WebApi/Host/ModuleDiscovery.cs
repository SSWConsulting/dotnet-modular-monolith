using Common.SharedKernel.Discovery;
using System.Reflection;

namespace Web.Host;

public static class ModuleDiscovery
{
    private static readonly Type ModuleType = typeof(IModule);

    public static void AddModules(this IServiceCollection services, IConfiguration config, params Assembly[] assemblies)
    {
        if (assemblies.Length == 0)
        {
            throw new ArgumentException("At least one assembly must be provided.", nameof(assemblies));
        }
        
        var moduleTypes = GetModuleTypes(assemblies);

        foreach (var type in moduleTypes)
        {
            var method = GetAddServicesMethod(type);
            method?.Invoke(null, [services, config]);
        }
    }
    
    private static IEnumerable<Type> GetModuleTypes(params Assembly[] assemblies) =>
        assemblies.SelectMany(x => x.GetTypes())
            .Where(x => ModuleType.IsAssignableFrom(x) &&
                        x is { IsInterface: false, IsAbstract: false });

    private static MethodInfo? GetAddServicesMethod(Type type) =>
        type.GetMethod(nameof(IModule.AddServices),
            BindingFlags.Static | BindingFlags.Public);
}