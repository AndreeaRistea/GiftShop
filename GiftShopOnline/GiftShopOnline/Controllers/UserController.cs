using GiftShopOnline.Interfaces;
using GiftShopOnline.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.AspNetCore.Mvc;
using GiftShopOnline.Models.User;
using GiftShopOnline.Helpers;
using GiftShopOnline.Data;

namespace GiftShopOnline.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly CurrentUser _user;
    private readonly UnitOfWork _uow;

    public UserController(IAuthService authService, CurrentUser user, UnitOfWork uoW)
    {
        _authService = authService;
        _user = user;
        _uow = uoW;
    }


    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(Status200OK, Type = typeof(AuthResponseDto))]
    [ProducesResponseType(Status400BadRequest)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto loginRequest)
    {

        var authResult = await _authService.LoginAsync(loginRequest);

        if (authResult == null) return BadRequest("User not found or invalid password.");

        SetRefreshToken(authResult.RefreshToken);

        return Ok(authResult.AuthResponse);
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    [ProducesResponseType(Status200OK, Type = typeof(AuthResponseDto))]
    [ProducesResponseType(Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto registerRequest)
    {
        var authResult = await _authService.RegisterAsync(registerRequest);

        if (authResult == null) return BadRequest("User already exists.");

        SetRefreshToken(authResult.RefreshToken);

        return Created("", authResult.AuthResponse);
    }
    private void SetRefreshToken(RefreshTokenModel refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.ExpiredTime
        };

        Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
    }

    [HttpGet("GetUserDetail")]
    public async Task<ActionResult<UserDetailsDto>> GetUserDetail() {
        var user = _user;

        var userDetailsDto = new UserDetailsDto
        {
            UserDto = new UserDto
            {
                Name = user.Name,
                Email = user.Email,
            },
            Address = user.Address,
        };

        return Ok(userDetailsDto);
    }

    [HttpPost("ProfilePicture")]
    public async Task<IActionResult> AddProfilePicture([FromBody] ProfilePictureDto profilePicture)
    {
        var user = _user.Id;

        var existingUser = await _uow.Users.FindAsync(user);

        if (existingUser == null)
        {
            return BadRequest("User not found");
        }

        existingUser.ProfilePhoteBase64 = profilePicture.ProfilePhoto;
        await _uow.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("ProfilePicture")]
    public async Task<IActionResult> GetProfilePicture()
    {
        var user = _user.Id;

        var existingUser = await _uow.Users.FindAsync(user);

        if (existingUser == null)
        {
            return BadRequest("User not found");
        }

        return Ok(
            new ProfilePictureDto
            {
                ProfilePhoto = existingUser.ProfilePhoteBase64
            });
    }
}
public class ProfilePictureDto()
{
    public string ProfilePhoto { get; set; }
}



