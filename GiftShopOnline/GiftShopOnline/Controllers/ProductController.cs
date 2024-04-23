using GiftShopOnline.Data;
using GiftShopOnline.Entities;
using GiftShopOnline.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftShopOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UnitOfWork _uow;
        
        public ProductController (UnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _uow.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var product = await _uow.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("by-category-id/{categoryId}")]
        public async Task<ActionResult<ProductDto>> GetProductsByCategory (Guid categoryId)
        {
            var products = await _uow.Products.Include(prod=>prod.Category).Where(prod => prod.CategoryId == categoryId).ToListAsync();
            var productDtos = products.Select(ProductDto.FromEntity).ToList();
            return Ok(productDtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto product)
        {
            //var categoryExists = await _uow.Categories.AnyAsync(c => c.Id == product.CategoryId);
            var categoryExists = await _uow.Categories.FirstOrDefaultAsync(c => c.Id == product.CategoryId);
            if (categoryExists is null)
            {
                return BadRequest("Categoria specificată nu există.");
            }

            if (product.ImageFile != null && product.ImageFile.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await product.ImageFile.CopyToAsync(ms);
                    product.Image = ms.ToArray();
                }
            }
            product.Id = Guid.NewGuid();
            var newProduct = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description,
                ImageFile = product.ImageFile,
                Image = product.Image,
                CategoryId = categoryExists.Id,
                Category = categoryExists,
            };
            _uow.Products.Add(newProduct);
            await _uow.SaveChangesAsync();

            return Ok(newProduct);
        }

     

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _uow.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _uow.Products.Remove(product);
            await _uow.SaveChangesAsync();

            return NoContent();
        }
    }
}
