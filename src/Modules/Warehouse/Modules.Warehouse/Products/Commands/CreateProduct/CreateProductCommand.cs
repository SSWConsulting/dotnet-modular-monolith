using Common.SharedKernel.Domain.Entities;
using Modules.Warehouse.Common.Persistence;
using Modules.Warehouse.Products.Domain;

namespace Modules.Warehouse.Products.Commands.CreateProduct;

internal record CreateProductCommand(string Name, decimal Amount, string Sku, Guid CategoryId)
    : IRequest;

internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
{
    // private readonly WarehouseDbContext _dbContext;
    // private readonly IProductRepository _productRepository;
    //
    // public CreateProductCommandHandler(WarehouseDbContext dbContext, IProductRepository productRepository, CancellationToken cancellationToken)
    // {
    //     _dbContext = dbContext;
    //     _productRepository = productRepository;
    // }

    public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // var money = new Money(Currency.Default, request.Amount);
        // var sku = Sku.Create(request.Sku);
        // ArgumentNullException.ThrowIfNull(sku);
        // var product = Product.Create(request.Name, money, sku, _productRepository);
        //
        // _dbContext.Products.Add(product);
        //
        // await _dbContext.SaveChangesAsync(cancellationToken);

        await Task.CompletedTask;
    }
}
