using AutoMapper;
using Contracts.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Orders;
using Ordering.Domain.Entities;
using Shared.SeedWork;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        //private readonly ISmtpEmailService _emailService;
        private IMessageProducer _messageProducer;

        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrdersController(IMediator mediator, IMessageProducer messageProducer, IOrderRepository orderRepository
            , IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _messageProducer = messageProducer;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public static class RouteNames
        {
            public const string GetOrders = nameof(GetOrders);
            public const string CreateOrder = nameof(CreateOrder);
            public const string UpdateOrder = nameof(UpdateOrder);
            public const string DeleteOrder = nameof(DeleteOrder);
        }

        #region CRUD
        [HttpGet("{username}", Name = RouteNames.GetOrders)]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUserName([Required] string username)
        {
            var query = new GetOrdersQuery(username);
            var result = await _mediator.Send(query);
            return Ok(result);
        }



        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            var addedOrder = await _orderRepository.CreateOrderAsync(order);
            await _orderRepository.SaveChangesAsync();
            var result = _mapper.Map<OrderDto>(addedOrder);
            _messageProducer.SendMessage(result);
            return Ok(result);
        }

        // [HttpPost]
        // [ProducesDefaultResponseType(typeof(ApiResult<long>), (int)HttpStatusCode.OK)]
        // public async Task<IActionResult<ApiResult<long>>> CreateOrder([FromBody]CreateOrderCommand command){
        //     var result = await _mediator.Send(command);
        //     return Ok(result)
        // }

        #endregion CRUD
    }
}