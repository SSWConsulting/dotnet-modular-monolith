using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Warehouse.Common.Persistence;
using Modules.Warehouse.Storage.Domain;

namespace Modules.Warehouse.Storage.UseCases;

internal static class CreateAisleCommand
{
    internal record Request(string Name, int NumBays, int NumShelves) : IRequest<IResult>;

    internal static class Endpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/aisles", async (Request request, ISender sender) => await sender.Send(request))
                .WithName("CreateAisle")
                .WithTags("Storage")
                .WithOpenApi();
        }
    }

    internal class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.NumBays).GreaterThan(0);
            RuleFor(r => r.NumShelves).GreaterThan(0);
        }
    }

    internal class Handler : IRequestHandler<Request, IResult>
    {
        private readonly WarehouseDbContext _context;

        public Handler(WarehouseDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> Handle(Request request, CancellationToken cancellationToken)
        {
            var aisle = Aisle.Create(request.Name, request.NumBays, request.NumShelves);
            await _context.Aisles.AddAsync(aisle, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return TypedResults.Created();
        }
    }
}
