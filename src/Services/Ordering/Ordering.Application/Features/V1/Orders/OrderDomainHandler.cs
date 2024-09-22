using MediatR;
using Ordering.Domain.OrderAggegate.Events;
using Serilog;

namespace Ordering.Application.Features.V1.Orders
{
    public class OrderDomainHandler : INotificationHandler<OrderCreatedEvent>,
        INotificationHandler<OrderDeletedEvent>
    {
        private readonly ILogger _logger;

        public OrderDomainHandler(ILogger logger)
        {
            _logger = logger;
        }

        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.Information($"Ordering Domain Event: {notification.GetType().Name}.");
            return Task.CompletedTask;
        }

        public Task Handle(OrderDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.Information($"Ordering Domain Event: {notification.GetType().Name}.");
            return Task.CompletedTask;
        }
    }
}