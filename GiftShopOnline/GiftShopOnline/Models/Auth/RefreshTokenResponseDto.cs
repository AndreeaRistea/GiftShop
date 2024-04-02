namespace GiftShopOnline.Models.Auth;

public class RefreshTokenResponseDto
{
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
}

