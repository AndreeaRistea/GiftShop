using GiftShopOnline.Entities;
using GiftShopOnline.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace GiftShopOnline.Models.Orders
{
    public class OrderHistoryDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus OrderStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public static OrderHistoryDto FromEntity(Order order)
        {
            return new OrderHistoryDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                OrderStatus = order.status,
                TotalAmount = order.TotalAmount,
            };
        }

    }
}
