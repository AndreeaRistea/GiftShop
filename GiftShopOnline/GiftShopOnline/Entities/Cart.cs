/*using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftShopOnline.Entities;

public class Cart
{
    public Guid Id { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public decimal TotalAmount
    {
        get
        {
            return CartItems.Sum(item => item.Quantity * item.Product.Price);
        }
    } 
    public virtual User User { get; set; }
}

*/