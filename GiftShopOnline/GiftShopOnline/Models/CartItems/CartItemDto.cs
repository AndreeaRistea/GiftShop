using GiftShopOnline.Entities;
using GiftShopOnline.Models.Products;

namespace GiftShopOnline.Models.CartItems;

 public class CartItemDto
 {
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
    public ProductDto Product { get; set; }
    public static CartItemDto FromEntity(CartItem cartItem)
    {
        return new CartItemDto
        {
            Id = cartItem.Id,
            Quantity = cartItem.Quantity,
            Product = ProductDto.FromEntity(cartItem.Product),
        };
    }
}


