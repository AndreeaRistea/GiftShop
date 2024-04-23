using GiftShopOnline.Data;
using GiftShopOnline.Helpers;
using GiftShopOnline.Models.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GiftShopOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly UnitOfWork _uow;
        private readonly CurrentUser _currentUser;
        public OrderController(CurrentUser currentUser, UnitOfWork uow)
        {
            _currentUser = currentUser;
            _uow = uow;
        }

        [HttpGet("orders-history")]
        public async Task<IActionResult> OrdersHistory ()
        {
            var userId = _currentUser.Id;
            var orders = await _uow.Orders
                .Where(o => o.UserId==userId)
                .Include(o=>o.OrderItems)
                .ThenInclude(o=>o.Product)
                .ThenInclude(p => p.Category)
                .ToListAsync();

            if (orders.Count()==0)
            {
                return NotFound();
            }

            return Ok(orders.Select(OrderHistoryDto.FromEntity));
        }

        [HttpGet("order-details/{orderId}")] 
        public async Task<IActionResult> OrderDetails (Guid orderId)
        {
            var order = await _uow.Orders
                .Where(o=>o.Id==orderId)
                .Include(o=>o.OrderItems)
                .ThenInclude(o=>o.Product)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync();
            if (order == null)
            {
                return NotFound();
            }

            return Ok(OrderDetailsDto.FromEntity(order));
        }

        [HttpPost("approve-order/{orderId:Guid}")]
        public async Task<IActionResult> ApproveOrder(Guid orderId)
        {
            var order = await _uow.Orders.FindAsync(orderId);
            order.status = Enums.OrderStatus.Approved;
            await _uow.SaveChangesAsync();
            return Ok();

        }
    }
}
