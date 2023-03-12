using MediatR;
using Ordering.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.V1.Orders;

public class GetOrdersQuery : IRequest<ApiResult<List<OrderDto>>>
{

}
