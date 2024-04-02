using Microsoft.EntityFrameworkCore.Storage.Json;

namespace GiftShopOnline.Models.Product;

public class ProductDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string? Description { get; set; }

    public IFormFile? ImageFile { get; set; }

    public byte[]? Image { get; set; }

    public Guid CategoryId { get; set; }

    public string? CategoryName { get; set;}
}

