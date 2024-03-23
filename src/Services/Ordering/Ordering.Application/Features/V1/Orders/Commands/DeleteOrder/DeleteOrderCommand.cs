using MediatR;
using Ordering.Application.Common.Models;
using Shared.SeedWork;

namespace Ordering.Application.Features.V1.Orders
{
    public class DeleteOrderCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }
}