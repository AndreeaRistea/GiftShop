namespace GiftShopOnline.Models.CartItems;

    public class CartItemUpdateDto
    {
        public Guid CartItemId { get; set; }
        public int NewQuantity { get; set; }
    }

