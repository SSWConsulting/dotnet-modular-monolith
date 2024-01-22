// using Common.Endpoints.Interfaces;
// using System.Reflection;
//
// namespace Common.Endpoints;
//
// public static class EndpointRegistration
// {
//     private static IEnumerable<IMapEndpoints> GetEndpoints => Assembly.GetCallingAssembly().GetTypes()
//         .Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(typeof(IMapEndpoints)))
//         .Select(Activator.CreateInstance)
//         .Cast<IMapEndpoints>()
//         .ToList();
//
//     private static readonly Lazy<IEnumerable<IMapEndpoints>> Endpoints = new(GetEndpoints);
//
//     public static void MapEndpoints(this WebApplication app)
//     {
//         // TODO: Test this
//         foreach (var group in Endpoints.Value)
//         {
//             group.MapEndpoints(app);
//         }
//     }
//
// }
