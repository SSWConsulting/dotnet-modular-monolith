using Common.SharedKernel;
using Common.SharedKernel.Api;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Categories.Domain;
using Modules.Catalog.Common.Persistence;
using Modules.Catalog.Products.Domain;

namespace Modules.Catalog.Products.UseCases;

public static class RemoveProductCategoryCommand
{
    public record Request(Guid ProductId, Guid CategoryId) : IRequest<ErrorOr<Success>>;

    public static class Endpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("/api/products/{productId:guid}/category/{categoryId:guid}",
                    async (Guid productId, Guid categoryId, ISender sender) =>
                    {
                        var request = new Request(productId, categoryId);
                        var response = await sender.Send(request);
                        return response.IsError ? response.Problem() : TypedResults.NoContent();
                    })
                .WithName("RemoveProductCategory")
                .WithTags("Catalog")
                .ProducesDelete()
                .WithOpenApi();
        }
    }

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.ProductId)
                .NotEmpty();

            RuleFor(r => r.CategoryId)
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
            var product =
                await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId,
                    cancellationToken: cancellationToken);

            if (product is null)
                return ProductErrors.NotFound;

            var categoryId = new CategoryId(request.CategoryId);
            var category =
                await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId,
                    cancellationToken: cancellationToken);

            if (category is null)
                return CategoryErrors.NotFound;

            product.RemoveCategory(category);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
