using Common.SharedKernel.Domain.Base;
using Throw;

namespace Modules.Warehouse.Storage.Domain;

internal class Bay : Entity<int>
{
    private readonly List<Shelf> _shelves = [];

    public IReadOnlyList<Shelf> Shelves => _shelves.AsReadOnly();

    public int AvailableStorage => _shelves.Count(s => s.IsEmpty);

    public int TotalStorage => _shelves.Count;

    public string Name { get; private set; } = null!;

    public static Bay Create(int id, int numShelves)
    {
        numShelves.Throw().IfNegativeOrZero();

        var bay = new Bay
        {
            Id = id,
            Name = $"Bay {id}"
        };

        for (var j = 0; j < numShelves; j++)
        {
            var shelf = Shelf.Create(j);
            bay._shelves.Add(shelf);
        }

        return bay;
    }
}
