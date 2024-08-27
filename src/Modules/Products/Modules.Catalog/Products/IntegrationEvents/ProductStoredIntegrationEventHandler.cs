using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Warehouse.Messages;

namespace Modules.Catalog.Products.IntegrationEvents;

internal class ProductStoredIntegrationEventHandler : INotificationHandler<ProductStoredIntegrationEvent>
{
    private readonly ILogger<ProductStoredIntegrationEventHandler> _logger;

    public ProductStoredIntegrationEventHandler(ILogger<ProductStoredIntegrationEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(ProductStoredIntegrationEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Product stored integration event received");

        var productId = new ProductId(notification.ProductId);
        var name = notification.ProductName;
        var sku = notification.productSku;

        var product = Product.Create(name, sku, productId);

        // TODO: Save product to DB

        _logger.LogInformation("Product stored integration event processed");

        await Task.CompletedTask;

    }
}
