using AutoMapper;
using MediatR;
using Ordering.Application.Common.Mappings;
using Ordering.Application.Common.Models;
using Ordering.Domain.Entities;
using Shared.SeedWork;
using Infrastructure.Mappings;

namespace Ordering.Application.Features.V1.Orders;

public class UpdateOrderCommand: CreateOrderCommand, IRequest<ApiResult<OrderDto>>, IMapFrom<Order>
{
    public long Id {get;set;}
    public void SetId (long id) {
        Id = id;
    }

    public void Mapping(Profile profile) {
        profile.CreateMap<UpdateOrderCommand, Order>()
        .ForMember(dest => dest.Status, otps => otps.Ignore())
        .IgnoreAllNonExisting();
    }

}
