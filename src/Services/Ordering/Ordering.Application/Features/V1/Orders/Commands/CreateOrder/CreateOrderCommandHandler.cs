using AutoMapper;
using Contracts.Services;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Domain.Entities;
using Serilog;
using Shared.SeedWork;
using Shared.Services.Email;

namespace Ordering.Application.Features.V1.Orders
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResult<long>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ISmtpEmailService _smtpEmailService;
        private readonly ILogger _logger;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ISmtpEmailService smtpEmailService, ILogger logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _smtpEmailService = smtpEmailService;
            _logger = logger;
        }

        private const string MethodName = "CreateOrderCommandHandler";

        public async Task<ApiResult<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.Information($"BEGIN: {MethodName} - UserName: {request.UserName}");
            var orderEntity = _mapper.Map<CreateOrderCommand, Order>(request);
            var addedOrder = await _orderRepository.CreateAsync(orderEntity);
            await _orderRepository.SaveChangesAsync();
            _logger.Information($"Order: {addedOrder.Id} is successfully created.");

            //SendEmalService(addedOrder, cancellationToken);

            _logger.Information($"END: {MethodName} - UserName: {request.UserName}");
            return new ApiSuccessResult<long>(addedOrder.Id);

        }

        private async Task SendEmalService(Order order, CancellationToken cancellationToken)
        {
            var emailRequest = new MailRequest()
            {
                ToAddress = order.EmailAddress,
                Body = "Order was created",
                Subject = "Order was created"
            };

            try
            {
                await _smtpEmailService.SendEmailServices(emailRequest, cancellationToken);
                _logger.Information($"Sent created order to email {emailRequest.ToAddress}");
            }
            catch(Exception ex )
            {
                _logger.Error($"Order {order.Id} failed due to an error with email service: {ex.Message}");
            }
        }
    }
}