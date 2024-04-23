using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftShopOnline.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Stock {  get; set; }

        public string Description { get; set; }

        public byte[]? Image { get; set; }

        [DataType(DataType.Upload)]
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public CartItem? CartItem { get; set; }
        public ICollection<Wishlist>? Wishlists { get; set; }

        public Guid? CategoryId {  get; set; }

        public virtual Category? Category { get; set; }
    }
}
