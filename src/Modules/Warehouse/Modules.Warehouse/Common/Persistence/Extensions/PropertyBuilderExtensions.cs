using Common.SharedKernel.Domain.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modules.Warehouse.Common.Persistence.Extensions;

internal static class PropertyBuilderExtensions
{
    public static PropertyBuilder<TId> HasStronglyTypedId<TId, TValue>(this PropertyBuilder<TId> propertyBuilder)
        where TId : IStronglyTypedId<TValue>
    {
        return propertyBuilder.HasConversion(new StronglyTypedIdConverter<TId, TValue>());
    }
}
