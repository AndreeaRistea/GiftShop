using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftShopOnline.Entities;

public class Wishlist
{
    [Key]
    public Guid Id { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual User User { get; set; }
}

