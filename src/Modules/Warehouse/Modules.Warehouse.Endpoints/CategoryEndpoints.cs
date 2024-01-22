using Common.Endpoints.Interfaces;
using MediatR;
using Modules.Warehouse.Application.Products.Commands.CreateProduct;
using Modules.Warehouse.Application.Products.Queries.GetProducts;
using Modules.Warehouse.Endpoints.Extensions;

namespace Modules.Warehouse.Endpoints;

public static class CategoryEndpoints //: IMapEndpoints
{
    // public static void MapCategoryEndpoints(this WebApplication app)
    // {
    //
    // }

    public static void MapCategoryEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("api/categories")
            .WithTags("Warehouse")
            .WithOpenApi();

        // group
        //     .MapGet("/", async (ISender sender, CancellationToken ct) => await sender.Send(new GetProductsQuery(), ct))
        //     .WithName("GetCa")
        //     .ProducesGet<ProductDto[]>();

        // group
        //     .MapPost("/", async (ISender sender, CreateCategoryCommand command, CancellationToken ct) => await sender.Send(command, ct))
        //     .WithName("CreateCategory")
        //     .ProducesPost();
    }
}
