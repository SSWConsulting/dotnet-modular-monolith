using ErrorOr;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Warehouse.Common.Persistence;
using Modules.Warehouse.Storage.Domain;

namespace Modules.Warehouse.Storage.UseCases;

public static class CreateAisle
{
    public record CreateAisleCommand(string Name, int NumBays, int NumShelves) : IRequest<ErrorOr<Success>>;

    public static class Endpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/aisles", async (CreateAisleCommand request, ISender sender) =>
                {
                    var response = await sender.Send(request);
                    return response.IsError ? response.Problem() : TypedResults.Created();
                })
                .WithName("CreateAisle")
                .WithTags("Warehouse")
                .WithOpenApi();
        }
    }

    public class Validator : AbstractValidator<CreateAisleCommand>
    {
        public Validator()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.NumBays).GreaterThan(0);
            RuleFor(r => r.NumShelves).GreaterThan(0);
        }
    }

    internal class Handler : IRequestHandler<CreateAisleCommand, ErrorOr<Success>>
    {
        private readonly WarehouseDbContext _context;

        public Handler(WarehouseDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<Success>> Handle(CreateAisleCommand createAisleCommand, CancellationToken cancellationToken)
        {
            var aisle = Aisle.Create(createAisleCommand.Name, createAisleCommand.NumBays, createAisleCommand.NumShelves);
            await _context.Aisles.AddAsync(aisle, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success;
        }
    }
}
