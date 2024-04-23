using AutoMapper;
using GiftShopOnline.Entities;
using GiftShopOnline.Models.Auth;
using GiftShopOnline.Models.CartItems;
using GiftShopOnline.Models.Category;
using GiftShopOnline.Models.Products;
using GiftShopOnline.Models.User;

namespace GiftShopOnline.Mapping;
internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, AuthResponseDto>();
        CreateMap<User, RefreshTokenResponseDto>();
        CreateMap<User, UserDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<Product, ProductDto>();
        CreateMap<CartItem, CartItemDto>();

    }
}