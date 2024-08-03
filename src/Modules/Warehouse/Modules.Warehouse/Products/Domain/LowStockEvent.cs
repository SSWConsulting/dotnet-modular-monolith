using Common.SharedKernel.Domain.Base;
using Common.SharedKernel.Domain.Identifiers;

namespace Modules.Warehouse.Products.Domain;

internal record LowStockEvent(ProductId ProductId) : DomainEvent;
