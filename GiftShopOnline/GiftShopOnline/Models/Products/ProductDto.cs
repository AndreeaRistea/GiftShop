using GiftShopOnline.Entities;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace GiftShopOnline.Models.Products;

public class ProductDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string? Description { get; set; }

    public IFormFile? ImageFile { get; set; }

    public byte[]? Image { get; set; }

    public Guid? CategoryId { get; set; }

    public string? CategoryName { get; set;}

    public static ProductDto FromEntity(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock,
            Description = product.Description,
            ImageFile = product.ImageFile,
            Image = product.Image,
            CategoryId = product.CategoryId,
            CategoryName = product.Category.CategoryName,
        };
    }
}

