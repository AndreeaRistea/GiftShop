using GiftShopOnline.Data;
using GiftShopOnline.Entities;
using GiftShopOnline.Helpers;
using GiftShopOnline.Models.CartItem;
using GiftShopOnline.Models.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftShopOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly UnitOfWork _uow;
        private readonly CurrentUser _currentUser;
        public CartItemController(CurrentUser currentUser, UnitOfWork uow)
        {
            _currentUser = currentUser;
            _uow = uow;
        }

        [HttpPost]
        public async Task<IActionResult> AddCartItems([FromBody] CreateCartItemDto cartItemDto)
        {
            var userId = _currentUser.Id;
            var product = await _uow.Products.FindAsync(cartItemDto.ProductId);
            if (userId == null)
            {
                return Problem("User not found");
            }

            if (product == null)
            {
                return Problem("Product not found");
            }

            var existingCartItem = await _uow.CartItems.FirstOrDefaultAsync(c => c.ProductId == cartItemDto.ProductId &&
                 c.UserId == userId);



            if (existingCartItem != null)
            {
                existingCartItem.Quantity += cartItemDto.Quantity;
            }
            else
            {
                var newCartItem = new CartItem
                {
                    Quantity = cartItemDto.Quantity,
                    ProductId = cartItemDto.ProductId,
                    UserId = userId.Value,
                    Product = product,

                };

                _uow.CartItems.Add(newCartItem);
            }
            await _uow.SaveChangesAsync();

            return Ok(product);
        }

        [HttpGet("all")]
        public async Task<ActionResult<CartItemDto>> GetAllCartItems()
        {
            var userId = _currentUser.Id;

            if (userId == null)
            {
                return Problem("User not found");
            }

            var userCartItems = await _uow.CartItems.Include(c => c.Product).
                ThenInclude(w => w.Category).
                Where(c => c.UserId == userId).ToListAsync();

            var cartItemDtos = userCartItems.Select(c => new CartItemDto
            {
                Id = c.Id,
                Quantity = c.Quantity,
                ProductId = c.ProductId.Value,
                Product = new ProductDto
                {
                    Id = c.ProductId.Value,
                    Name = c.Product.Name,
                    Description = c.Product.Description,
                    Price = c.Product.Price,
                    Stock = c.Product.Stock,
                    CategoryId = c.ProductId.Value,
                    CategoryName = c.Product.Category.CategoryName,
                    Image = c.Product.Image,
                }

            }).ToList();
            return Ok(cartItemDtos);
        }

        [HttpPost("updateQuantity /{cartItemId}")]
        public async Task<IActionResult> UpdateQuantity (Guid cartItemId, int newQuantity)
        {
            var userId = _currentUser.Id;

            var cartItem = await _uow.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId && c.UserId == userId);

            if (cartItem == null)
            {
                return NotFound();
            }

            if(cartItem.Quantity + newQuantity > 1) 
            {
                cartItem.Quantity += newQuantity;
                await _uow.SaveChangesAsync();
                return Ok(cartItem);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        public async Task<IActionResult> RemoveItem (Guid cartItemId)
        {
            var userId = _currentUser.Id;

            var cartItem = await _uow.CartItems.FirstOrDefaultAsync(c=>c.Id == cartItemId && c.UserId == userId);
            if (cartItem == null)
            {
                return NotFound(); 
            }
            else
            {
                _uow.Remove(cartItem);
                await _uow.SaveChangesAsync();
                return Ok();
            }
        }
    }
}
