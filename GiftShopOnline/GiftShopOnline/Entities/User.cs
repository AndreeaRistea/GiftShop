using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftShopOnline.Entities;

public class User
{
    [Key]
    public Guid Id {  get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }   

    public string Address { get; set; }

    public string? Role { get; set; }
    
    public string RefreshToken {  get; set; }

    public DateTime TokenCreated { get; set; }

    public DateTime TokenExpires { get; set; }

    //public Guid? CartId { get; set; }

    //public virtual Cart? Cart { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public Guid? WishlistId {  get; set; }

    public virtual Wishlist? Wishlist { get; set; }

    public string? ProfilePhoteBase64 {  get; set; }
}

