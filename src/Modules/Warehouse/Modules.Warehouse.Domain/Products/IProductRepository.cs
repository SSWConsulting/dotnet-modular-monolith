namespace Modules.Warehouse.Domain.Products;

public interface IProductRepository
{
    public Task<bool> SkuExistsAsync(Sku sku);

    public bool SkuExists(Sku sku);
}
