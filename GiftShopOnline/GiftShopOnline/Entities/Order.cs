using GiftShopOnline.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftShopOnline.Entities;

public class Order
{
    [Key] public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus status {  get; set; }
    [ForeignKey("UserId")]
    public Guid UserId { get; set; }
    public User User { get; set; }
    //public Guid CartItemId {  get; set; }
    //public CartItem CartItem { get; set; }
    public ICollection<CartItem> OrderItems { get; set; } = new List<CartItem>();
    public decimal TotalAmount
    {
        get
        {
            return OrderItems.Sum(item => item.Quantity * item.Product.Price);
        }
    }
}

