using MediatR;
using Ordering.Application.Common.Exceptions;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Serilog;

namespace Ordering.Application.Features.V1.Orders;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger _logger;
    public DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger logger)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    private const string MethodName = "DeleteOrderCommandHandler";

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = await _orderRepository.GetByIdAsync(request.Id);
        if (orderEntity == null) throw new NotFoundException(nameof(Order), request.Id);

        _logger.Information($"BEGIN: {MethodName} - Order: {request.Id}");
        _orderRepository.DeleteAsync(orderEntity);
        _orderRepository.SaveChangesAsync();
        _logger.Information($"Order {orderEntity.Id} was successfully deleted.");
        _logger.Information($"END: {MethodName} - Order: {request.Id}");

        return Unit.Value;
    }
}
