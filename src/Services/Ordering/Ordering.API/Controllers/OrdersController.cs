using AutoMapper;
using Contracts.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Orders;
using Ordering.Domain.Entities;
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
        }

        [HttpGet("{username}", Name = RouteNames.GetOrders)]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUserName([Required] string username)
        {
            var query = new GetOrdersQuery(username);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        //[HttpGet]
        //public async Task<IActionResult> TestEmail()
        //{
        //    var message = new MailRequest
        //    {
        //        Body = "hello",
        //        Subject = "test mail",
        //        ToAddress = "namqd98@gmail.com"
        //    };
        //    await _emailService.SendEmailServices(message);
        //    return Ok();
        //}

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            var addedOrder = await _orderRepository.CreateOrder(order);
            await _orderRepository.SaveChangesAsync();
            var result = _mapper.Map<OrderDto>(addedOrder);
            _messageProducer.SendMessage(result);
            return Ok(result);
        }
    }
}