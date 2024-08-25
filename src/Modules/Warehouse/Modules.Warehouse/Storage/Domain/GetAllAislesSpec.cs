using Ardalis.Specification;

namespace Modules.Warehouse.Storage.Domain;

internal class GetAllAislesSpec : Specification<Aisle>
{
    public GetAllAislesSpec()
    {
        Query
            .Include(a => a.Bays)
            .ThenInclude(b => b.Shelves);
    }
}