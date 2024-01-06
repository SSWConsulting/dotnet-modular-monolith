using Common.SharedKernel.Domain.Base;
using Common.SharedKernel.Domain.Identifiers;

namespace Modules.Warehouse.Domain.Products;

public record LowStockEvent(ProductId ProductId) : DomainEvent;
