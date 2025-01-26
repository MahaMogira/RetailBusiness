using Microsoft.AspNetCore.Mvc;
using RetailBusiness.Core.Entities;
using RetailBusiness.Infrastructure.Services;

namespace RetailBusiness.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int page = 1, int pageSize = 10)
        {
            var products = await _productService.GetAllProductsAsync(page, pageSize);
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategoryFromSP(int categoryId)
        {
            var products = await _categoryService.GetProductsByCategoryAsync(categoryId);

            if (products == null || !products.Any())
            {
                return NotFound($"No products found for category with ID {categoryId}.");
            }

            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var success = await _productService.AddProductAsync(product);
            if (!success)
                return StatusCode(500, "Failed to save the product.");

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);

        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            var productdata = await _productService.GetProductByIdAsync(id);
            if (productdata == null)
                return BadRequest("Product ID mismatch.");
            var success = await _productService.UpdateProductAsync(product);
            if (!success)
                return StatusCode(500, "Failed to update the product.");

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _productService.DeleteProductAsync(id);
            if (!success)
                return NotFound("Product not found or could not be deleted.");

            return NoContent();
        }
    }
}
