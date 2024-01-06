using MediatR;
using Modules.Warehouse.Application.Products.Queries.GetProducts;
using Modules.Warehouse.Endpoints.Extensions;

namespace Modules.Warehouse.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var group = app
            .MapGroup("api/products")
            .WithTags("Warehouse")
            .WithOpenApi();

        group
            .MapGet("/", async (ISender sender, CancellationToken ct) => await sender.Send(new GetProductsQuery(), ct))
            .WithName("GetProducts")
            .ProducesGet<ProductDto[]>();

        // group
        //     .MapPost("/", async (ISender sender, CreateProductCommand command, CancellationToken ct) => await sender.Send(command, ct))
        //     .WithName("CreateProduct")
        //     .ProducesPost();
    }
}