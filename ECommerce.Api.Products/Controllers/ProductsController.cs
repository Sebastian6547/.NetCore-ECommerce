using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider productsProvider;

        public ProductsController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var (isSuccess, products, errorMessage) = await productsProvider.GetProductsAsync();
            if (isSuccess)
            {
                return Ok(products);
            }
            return NotFound(errorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var (isSuccess, product, errorMessage) = await productsProvider.GetProductAsync(id);
            if (isSuccess)
            {
                return Ok(product);
            }
            return NotFound(errorMessage);
        }
    }
}
