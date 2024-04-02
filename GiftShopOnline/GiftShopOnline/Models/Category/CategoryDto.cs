namespace GiftShopOnline.Models.Category;

public class CategoryDto
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    //public IFormFile CoverImageFile { get; set; }
    public byte[] CoverImage { get; set; }
}

