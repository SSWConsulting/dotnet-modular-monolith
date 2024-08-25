using ErrorOr;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Warehouse.Common.Persistence;
using Modules.Warehouse.Products.Domain;
using Modules.Warehouse.Storage.UseCases;

namespace Modules.Warehouse.Products.UseCases;

public static class CreateProduct
{
    public record CreateProductCommand(string Name, string Sku) : IRequest<ErrorOr<Success>>;

    public static class Endpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/products", async (CreateProductCommand request, ISender sender) =>
                {
                    var response = await sender.Send(request);
                    return response.IsError ? response.Problem() : TypedResults.Created();
                })
                .WithName("Create Product")
                .WithTags("Warehouse")
                .WithOpenApi();
        }
    }

    public class Validator : AbstractValidator<CreateProductCommand>
    {
        public Validator()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Sku)
                .NotEmpty()
                .Length(Sku.DefaultLength);
        }
    }

    internal class Handler : IRequestHandler<CreateProductCommand, ErrorOr<Success>>
    {
        private readonly WarehouseDbContext _dbDbContext;

        public Handler(WarehouseDbContext dbContext)
        {
            _dbDbContext = dbContext;
        }

        public async Task<ErrorOr<Success>> Handle(CreateProductCommand createProductCommand, CancellationToken cancellationToken)
        {
            var sku = Sku.Create(createProductCommand.Sku);

            var product = Product.Create(createProductCommand.Name, sku);
            _dbDbContext.Products.Add(product);
            await _dbDbContext.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
