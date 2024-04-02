namespace GiftShopOnline.Models.CartItem
{
    public class CreateCartItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
