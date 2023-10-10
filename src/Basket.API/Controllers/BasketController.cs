using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : Controller
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcService)
        {
            _repository = repository;
            _discountGrpcService = discountGrpcService;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<ShoppingCart?>> GetBasket(string username)
        {
            return Ok(await _repository.GetBasket(username));
        }

        [HttpPut]
        public async Task<ActionResult<ShoppingCart?>> UpdateBaseket([FromBody] ShoppingCart shoppingCart)
        {
            
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBasket(string username)
        {
            await _repository.DeleteBasket(username);
            return Ok();
        }
    }
}
