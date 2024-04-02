using AutoMapper;
using GiftShopOnline.Models.User;

namespace GiftShopOnline.Models.Auth;

public sealed class AuthResponseDto : UserDto
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }

    //public static AuthResponseDto MapFromUser(Entities.User user, IMapper mapper, string token)
    //{
    //    var dto = new AuthResponseDto
    //    {
    //        Id = user.UserId,
    //        Name = user.Name,
    //        Email = user.Email,
    //        PasswordHash = user.PasswordHash,
    //        Token = token,
    //        RefreshToken = user.RefreshToken
    //    };
    //    return dto;
    //}

    public static AuthResponseDto MapFromUser(Entities.User user, IMapper mapper)
    {
        var dto = mapper.Map<AuthResponseDto>(user);
        return dto;
    }
}

