using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Contracts.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ILogger = Serilog.ILogger;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisChageService;
        private readonly ISerializeService _serializeService;
        private readonly ILogger _logger;

        public BasketRepository(IDistributedCache redisChageService, ISerializeService serializeService, ILogger logger)
        {
            _redisChageService = redisChageService;
            _serializeService = serializeService;
            _logger = logger;
        }

        public async Task<bool> DeleteBasketFromUserName(string userName)
        {
            try
            {
                await _redisChageService.RefreshAsync(userName);
                return true;
            }
            catch(Exception e)
            {
                _logger.Error("DeleteBasketFromUserName" + e.Message);
                throw;
            }
        }

        public async Task<Cart?> GetBasketByUserName(string userName)
        {
            var basket = await _redisChageService.GetStringAsync(userName);
            return string.IsNullOrEmpty(basket) ? null : 
                _serializeService.Deserialize<Cart>(basket);
        }

        public async Task<Cart?> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = null)
        {
            if(options != null)
            {
                await _redisChageService.SetStringAsync(cart.UserName,
                    _serializeService.Serialize(cart), options);
            }
            else
            {
                await _redisChageService.SetStringAsync(cart.UserName,
                    _serializeService.Serialize(cart));
            }
            return await GetBasketByUserName(cart.UserName);
        }
    }
}
