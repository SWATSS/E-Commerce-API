using Domain.Contracts;
using Domain.Models.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    internal class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase() ; // Get The DataBase Form Redis Server
        public async Task<Basket?> CreateOrUpdateAsync(Basket basket, TimeSpan? timeToLive = null)
        {
            var jsonBasket = JsonSerializer.Serialize(basket);
            var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonBasket, timeToLive ?? TimeSpan.FromDays(3));
            if (isCreatedOrUpdated)
                return await GetBasketAsync(basket.Id);
            return null;
        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        public async Task<Basket?> GetBasketAsync(string id)
        {
            var basket = await _database.StringGetAsync(id); // he can Change fsrom Redis Key => string
            if (basket.IsNullOrEmpty) return null;

            return JsonSerializer.Deserialize<Basket>(basket!); // ! => forgiveness
        }
    }
}
