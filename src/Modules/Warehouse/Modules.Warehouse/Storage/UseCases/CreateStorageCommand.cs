using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Warehouse.Storage.Domain;

namespace Modules.Warehouse.Storage.UseCases;

internal static class CreateStorageCommand
{
    internal record Request(string Name, int NumBays, int NumShelves) : IRequest;

    internal static class Endpoint
    {
        public static void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/storage", async (Request request, ISender sender) => await sender.Send(request))
                .WithName("CreateStorage")
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

    internal class Handler : IRequestHandler<Request>
    {
        public async Task Handle(Request request, CancellationToken cancellationToken)
        {
            var storage = Aisle.Create(request.Name, request.NumBays, request.NumShelves);


        }
    }
}

// public record CreateStorageCommand : IRequest;
//
// public class CreateStorageValidator : AbstractValidator<CreateStorageCommand>
// {
//     public CreateStorageValidator()
//     {
//         // TODO: Add rules
//     }
// }
//
// public class CreateStorageCommandHandler : IRequestHandler<CreateStorageCommand>
// {
//     public async Task Handle(CreateStorageCommand request, CancellationToken cancellationToken)
//     {
//
//     }
// }
