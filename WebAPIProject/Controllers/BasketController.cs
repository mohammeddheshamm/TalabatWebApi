using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using TalabatAPIS.Dtos;

namespace TalabatAPIS.Controllers
{
    
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet("id")] // GET : api/basket/1
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string basketId)
        {
            var basket =await _basketRepository.GetBasketAsync(basketId);
            return Ok(basket ?? new CustomerBasket(basketId));
        }

        [HttpPost] //Post : api/basket
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            // We used CustomerBasketDto to validate on the Data Passed to be updated.
            var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var updatedOrCreatedBasket = await _basketRepository.UpdateBasketAsync(mappedBasket);
            return Ok(updatedOrCreatedBasket);
        }

        [HttpDelete] // DELETE : /api/basket
        public async Task DeleteBasket(string basketId)
        {
            await _basketRepository.DeleteBasketAsync(basketId);
        }
    }
}
