using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.BasketModule;
using Service.Abstraction;
using Shared.DataTransferObjects.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateAsync(BasketDto basket)
        {
            var basketModel = _mapper.Map<BasketDto, Basket>(basket);
            var CreatedOrUpdatedBasket = await _basketRepository.CreateOrUpdateAsync(basketModel);
            if (CreatedOrUpdatedBasket != null)
                return await GetBasketAsync(basket.Id);

            throw new Exception("Cant Create Or Update Basket Right Now, Please Try Again Later");
        }
        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _basketRepository.DeleteBasketAsync(id);
        }

        public async Task<BasketDto> GetBasketAsync(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            if (basket is not null)
                return _mapper.Map<Basket, BasketDto>(basket);
            throw new BasketNotFoundException(id);
        }
    }
}
