using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GiftShopOnline.Entities;

public class Category
{
    [Key]
    public Guid Id { get; set; }

    public string CategoryName { get; set; }
    public byte[]? CoverImage { get; set; }


    [DataType(DataType.Upload)]
    [NotMapped]
    public IFormFile CoverImageFile { get; set; }

    public ICollection<Product>? Products { get; set;}
}

