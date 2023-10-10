using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscountController : Controller
    {
        private readonly IDiscountRepository _repository;
        public DiscountController(IDiscountRepository discountRepository)
        {
            _repository = discountRepository;
        }

        [HttpGet("{productName}")]
        public async Task<ActionResult<Coupon?>> GetDiscount(string productName)
        {
            return Ok(await _repository.GetDiscount(productName));
        }

        [HttpPost]
        public async Task<ActionResult<Coupon?>> CreateDiscount(Coupon coupon)
        {
            return CreatedAtAction(nameof(GetDiscount), new { productName = coupon.ProductName }, await _repository.CreateDiscount(coupon));
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteDiscount(Coupon coupon)
        {
            return Ok(await _repository.DeleteDiscount(coupon.ProductName));
        }
    }
}
