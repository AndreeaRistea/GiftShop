using GiftShopOnline.Entities;
using GiftShopOnline.Enums;
using GiftShopOnline.Models.CartItems;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace GiftShopOnline.Models.Orders
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public List<CartItemDto> OrderItems { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public static OrderDetailsDto FromEntity(Order order)
        {
            return new OrderDetailsDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(CartItemDto.FromEntity).ToList(),
                OrderStatus = order.status,
                TotalAmount = order.TotalAmount,
            };
        }

    }
}
