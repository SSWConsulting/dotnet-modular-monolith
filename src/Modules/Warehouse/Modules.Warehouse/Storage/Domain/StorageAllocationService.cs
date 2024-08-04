using Modules.Warehouse.Products.Domain;

namespace Modules.Warehouse.Storage.Domain;

/// <summary>
/// Simple storage algorithm that naively assumes 1 product per shelf.
/// </summary>
internal class StorageAllocationService
{
    internal void AllocateStorage(IEnumerable<Aisle> aisles, ProductId productId)
    {
        foreach (var aisle in aisles)
        {
            foreach (var bay in aisle.Bays)
            {
                foreach (var shelf in bay.Shelves)
                {
                    if (!shelf.IsEmpty)
                        continue;

                    shelf.AssignProduct(productId);
                    return;
                }
            }
        }

        throw new Exception("No available storage");
    }
}
