using AutoMapper;
using EventBus.Messages.IntegrationEvents.Events;

namespace Basket.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
            CreateMap<BasketCheckout, BasketCheckoutEvent>
        }
    }
}
