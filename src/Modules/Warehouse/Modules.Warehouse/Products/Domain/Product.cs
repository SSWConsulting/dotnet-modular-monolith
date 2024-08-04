using Common.SharedKernel.Domain.Base;
using Common.SharedKernel.Domain.Entities;
using Common.SharedKernel.Domain.Exceptions;
using Throw;

namespace Modules.Warehouse.Products.Domain;

internal record ProductId(Guid Value);

internal class Product : AggregateRoot<ProductId>
{
    private const int LowStockThreshold = 5;

    public string Name { get; private set; } = null!;

    public Money Price { get; private set; } = null!;

    public Sku Sku { get; private set; } = null!;

    public int StockOnHand { get; private set; }

    private Product()
    {
    }

    // NOTE: Need to use a factory, as EF does not let owned entities (i.e Money & Sku) be passed via the constructor
    public static Product Create(string name, Money price, Sku sku, IProductRepository productRepository)
    {
        name.Throw().IfEmpty();
        price.Throw().IfNegativeOrZero(p => p.Amount);

        var product = new Product
        {
            Id = new ProductId(Guid.NewGuid()),
            // CategoryId = categoryId,
            Name = name,
            Price = price,
            StockOnHand = 0
        };

        product.UpdateSku(sku, productRepository);

        product.AddDomainEvent(ProductCreatedEvent.Create(product));

        return product;
    }

    public void UpdateName(string name)
    {
        name.Throw().IfEmpty();
        Name = name;
    }

    public void UpdatePrice(Money price)
    {
        price.Throw().IfNegativeOrZero(p => p.Amount);
        Price = price;
    }

    public void UpdateSku(Sku sku, IProductRepository productRepository)
    {
        if (productRepository.SkuExists(sku))
            throw new ArgumentException("Sku already exists");

        Sku = sku;
    }

    public void RemoveStock(int quantity)
    {
        quantity.Throw().IfNegativeOrZero();

        if (StockOnHand - quantity < 0)
            throw new DomainException("Cannot adjust stock below zero");

        StockOnHand -= quantity;

        if (StockOnHand <= LowStockThreshold)
            AddDomainEvent(new LowStockEvent(Id));
    }

    public void AddStock(int quantity)
    {
        quantity.Throw().IfNegativeOrZero();
        StockOnHand += quantity;
    }
}
