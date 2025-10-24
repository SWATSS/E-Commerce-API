using Domain.Models.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<Basket?> GetBasketAsync(string id);
        Task<Basket?> CreateOrUpdateAsync(Basket basket, TimeSpan? timeToLive = null);
        Task<bool> DeleteBasketAsync(string key);
    }
}
