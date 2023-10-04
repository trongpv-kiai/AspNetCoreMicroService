using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductController> _logger;
        public ProductController(
            IProductRepository repository,
            ILogger<ProductController> logger
            )
        {
            _logger = logger;
            _repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _repository.GetProducts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _repository.GetProduct(id);
            if(product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _repository.CreateProduct(product);

            return CreatedAtRoute(nameof(GetProduct), new {id = product.Id}, product);
        }

        [HttpPut()]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
        {
            var productModel = await _repository.GetProduct(product.Id);
            if(productModel == null)
            {
                _logger.LogError($"Product with id: {product.Id}, not found.");
                return NotFound();
            }
            return Ok(await _repository.UpdateProduct(product));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            var productModel = await _repository.GetProduct(id);
            if (productModel == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }
            return Ok(await _repository.DeleteProduct(id));
        }
    }
}
