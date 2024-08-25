using Common.SharedKernel.Domain.Base;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Modules.Warehouse.Common.Persistence.Extensions;

internal class StronglyTypedIdConverter<TId, TValue> : ValueConverter<TId, TValue>
    where TId : IStronglyTypedId<TValue>
{
    public StronglyTypedIdConverter(ConverterMappingHints? mappingHints = null)
        : base(
            id => id.Value,
            value => (TId)Activator.CreateInstance(typeof(TId), value)!,
            mappingHints)
    {
    }
}
