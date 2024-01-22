// using Common.Endpoints.Interfaces;
// using System.Reflection;
//
// namespace Common.Endpoints;
//
// public static class ModuleRegistration
// {
//     //private static IEnumerable<IModule> GetModules =>
//
//     //private static readonly Lazy<IEnumerable<IModule>> Modules = new(GetModules);
//
//     public static void AddModuleServices(this IServiceCollection services, IConfiguration configuration)
//     {
//         // TODO: Test this
//         foreach (var module in Process.GetInstances<IModule>())
//         {
//             module.AddServices(services, configuration);
//         }
//     }
//
//     public static async Task UseModules(this WebApplication app)
//     {
//         foreach (var module in Process.GetInstances<IModule>())
//         {
//             await module.UseModule(app);
//         }
//     }
// }
//
// internal static class Process
// {
//     public static IEnumerable<T> GetInstances<T>()
//     {
//         var allAssemblies = AppDomain.CurrentDomain.GetAssemblies();
//
//         // Get all assemblies loaded in the current application domain
//         var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name.StartsWith("Modules`"));
//
//         // List to hold all types
//         var allTypes = new List<T>();
//
//         foreach (var assembly in assemblies)
//         {
//             // Get types in the assembly
//             var types = assembly
//                 .GetTypes()
//                 .Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(typeof(T)))
//                 .Select(Activator.CreateInstance)
//                 .Cast<T>();
//
//             // Add types to the list
//             allTypes.AddRange(types);
//         }
//
//         return allTypes;
//     }
// }
