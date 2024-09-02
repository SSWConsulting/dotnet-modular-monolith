using Ardalis.Specification.EntityFrameworkCore;
using Common.SharedKernel;
using Common.SharedKernel.Api;
using Common.SharedKernel.Domain.Entities;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Categories.Domain;
using Modules.Catalog.Common.Persistence;
using Modules.Catalog.Products.Domain;
using System.Text.Json.Serialization;

namespace Modules.Catalog.Products.UseCases;

public static class UpdateProductPriceCommand
{
    public record Request([property: FromRoute]Guid ProductId, decimal Price) : IRequest<ErrorOr<Success>>;

    public static class Endpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/products/{productId:guid}",
                    async (Request request, ISender sender) =>
                    {
                        // var request = new Request(productId, categoryId);
                        var response = await sender.Send(request);
                        return response.IsError ? response.Problem() : TypedResults.NoContent();
                    })
                .WithName("UpdateProductPrice")
                .WithTags("Catalog")
                .ProducesPost()
                .WithOpenApi();
        }
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.ProductId)
                .NotEmpty();

            RuleFor(r => r.Price)
                .NotEmpty();
        }
    }

    internal class Handler : IRequestHandler<Request, ErrorOr<Success>>
    {
        private readonly CatalogDbContext _dbContext;

        public Handler(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<Success>> Handle(Request request, CancellationToken cancellationToken)
        {
            var productId = new ProductId(request.ProductId);
            var product = await _dbContext.Products
                .WithSpecification(new ProductByIdSpec(productId))
                .FirstOrDefaultAsync(cancellationToken);

            if (product is null)
                return ProductErrors.NotFound;

            var money = Money.Create(request.Price);
            product.UpdatePrice(money);

            return Result.Success;
        }
    }
}
