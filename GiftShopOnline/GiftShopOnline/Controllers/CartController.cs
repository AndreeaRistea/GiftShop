using GiftShopOnline.Data;
using GiftShopOnline.Entities;
using GiftShopOnline.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftShopOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly UnitOfWork _uow;
        private readonly CurrentUser _currentUser;
        public CartController(CurrentUser currentUser, UnitOfWork uow)
        {
            _currentUser = currentUser;
            _uow = uow;
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var userId = _currentUser.Id;
            var cartItems = await _uow.CartItems.Where(c => c.UserId == userId).ToListAsync();
            if (cartItems.Count()==0)
            {
                return NotFound();
            }

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                UserId = userId.Value,
                OrderItems = cartItems,
            };
            _uow.Orders.Add(order);
            // _uow.CartItems.RemoveRange(cartItems);
            cartItems.ForEach(c => c.UserId = null);
            await _uow.SaveChangesAsync();
            return Ok();
        }
    }
}