using MediatR;

namespace Modules.Warehouse.Messages;

public record ProductStoredIntegrationEvent(Guid ProductId, string ProductName, string productSku) : INotification;
