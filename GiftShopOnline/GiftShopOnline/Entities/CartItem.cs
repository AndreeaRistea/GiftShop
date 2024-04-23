using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftShopOnline.Entities;

public class CartItem
{
    [Key]
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Guid? ProductId {  get; set; }
    public Product? Product { get; set; }

    public Guid? UserId { get; set; }
    public virtual User User { get; set; }

    //[ForeignKey("OrderId")]
    public Guid? OrderId { get; set; }
    public virtual Order? Order { get; set; }
    //public virtual Order Order { get; set; }
    //public Guid OrderItemId { get; set; }
}

