namespace GiftShopOnline.Models.Auth;

public class AuthResultModel
{
    public required AuthResponseDto AuthResponse { get; set; }
    public required RefreshTokenModel RefreshToken { get; set; }
}

