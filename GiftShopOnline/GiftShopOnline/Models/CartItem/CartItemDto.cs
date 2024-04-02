using GiftShopOnline.Models.Product;

namespace GiftShopOnline.Models.CartItem;

 public class CartItemDto
 {
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
    public ProductDto Product { get; set; }
}


