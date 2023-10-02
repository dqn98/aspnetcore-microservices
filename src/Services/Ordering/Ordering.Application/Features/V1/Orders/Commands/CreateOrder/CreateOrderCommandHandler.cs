﻿using AutoMapper;
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
        private readonly ISmtpEmailService _emailServerce;
        private readonly ILogger _logger;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ISmtpEmailService emailService,
        ILogger logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailServerce = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private const string MethodName = "CreateOrderCommandHandler";

        public async Task<ApiResult<long>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.Information($"BEGIN: {MethodName} - UserName: {request.UserName}");
            var orderEntity = _mapper.Map<Order>(request);
            var addedOrder = await _orderRepository.CreateOrderAsync(orderEntity);
            await _orderRepository.SaveChangesAsync();
            _logger.Information($"Order {addedOrder.Id} is successfully created.");

            SendEmailAsync(addedOrder, cancellationToken);
            _logger.Information($"END: {MethodName} - UserName: {request.UserName}");
            return new ApiSuccessResult<long>(addedOrder.Id);
        }

        private async Task SendEmailAsync(Order order, CancellationToken cancellationToken)
        {
            var emailRequest = new MailRequest
            {
                ToAddress = order.EmailAddress,
                Body = "Order was created.",
                Subject = "Order was created."
            };

            try
            {
                await _emailServerce.SendEmailAsync(emailRequest, cancellationToken);
                _logger.Information($"Send created order to email: {order.EmailAddress}");
            }

            catch (Exception ex)
            {
                _logger.Error($"Order {order.Id} failed due to an error with the email service: {ex.Message}");
            }
        }
    }
}
