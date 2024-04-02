using GiftShopOnline.Data;
using GiftShopOnline.Entities;
using GiftShopOnline.Helpers;
using GiftShopOnline.Models.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace GiftShopOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly UnitOfWork _uow;
        private readonly CurrentUser _currentUser;

        public WishlistController (UnitOfWork uow, CurrentUser currentUser)
        {
            _uow = uow;
            _currentUser = currentUser;
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(Guid productId)
        {
            var userId = _currentUser.Id;
            var product = await _uow.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (userId == null)
            {
                return Problem("User not found");
            }

            if (product == null)
            {
                return Problem("Product not found");
            }

            var wishList = await _uow.Wishlist.FirstOrDefaultAsync(w => w.User.Id ==  userId);
            var user = await _uow.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (wishList == null)
            {
                wishList = new Entities.Wishlist { User = user, Products = new List<Product>() };
                _uow.Wishlist.Add(wishList);
                wishList.Products.Add(product);
            }
            else
            {
                wishList.Products.Add(product);
            }
            await _uow.SaveChangesAsync();
            return Ok(wishList);

        }

        [HttpGet] 
        public async Task<ActionResult<ProductDto>> GetProductInWishlist ()
        {
            var userId = _currentUser.Id;
            if (userId == null)
            {
                return Problem("User not found");
            }

            /*var wishlist = await _uow.Wishlist.FirstOrDefaultAsync(w => w.User.Id == userId);

            if (wishlist == null)
            {
                return NotFound();
            }*/

            var wishlist = await _uow.Wishlist.Include(w => w.Products).ThenInclude(p => p.Category).
                //ThenInclude(w => w.Category).
                                FirstOrDefaultAsync(w => w.User.Id == userId);
            //(w => w.User.Id == userId);.ToListAsync();

            if (wishlist == null)
            {
                return NotFound();
            }

            wishlist.Products = wishlist.Products.ToList();


            var productDtos = wishlist.Products.Select(prod => new ProductDto
            {
                Id = prod.Id,
                Name = prod.Name,
                Price = prod.Price,
                Stock = prod.Stock,
                Description = prod.Description,
                Image = prod.Image,
                CategoryId = prod.CategoryId.Value,
                CategoryName = prod.Category.CategoryName,
            }).ToList();
            return Ok(productDtos);
            //return Ok(products);
        }

        [HttpDelete] 
        public async Task<IActionResult> RemoveFromWishlist(Guid productId)
        {
            var user = _currentUser.Id;
            var wishlist = await _uow.Wishlist
                .Include(w => w.Products)
                .FirstOrDefaultAsync(w => w.User.Id == user);

            wishlist.Products = wishlist.Products
                .Where(p => p.Id != productId)
                .ToList();

            await _uow.SaveChangesAsync();

            return Ok();
        }

    }
}
