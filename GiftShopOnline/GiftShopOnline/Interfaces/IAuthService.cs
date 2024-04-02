using GiftShopOnline.Models.Auth;

namespace GiftShopOnline.Interfaces;

public interface IAuthService
{
    Task<AuthResultModel?> RegisterAsync(RegisterRequestDto registerRequest);

    Task<AuthResultModel?> LoginAsync(LoginRequestDto loginRequest);
}

