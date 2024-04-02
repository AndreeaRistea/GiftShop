using AutoMapper;
using GiftShopOnline.Data;
using GiftShopOnline.Entities;
using GiftShopOnline.Interfaces;
using GiftShopOnline.Utils;
using GiftShopOnline.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static GiftShopOnline.Utils.PasswordUtils;

namespace GiftShopOnline.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public AuthService(UnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<AuthResultModel?> LoginAsync(LoginRequestDto loginRequest)
    {
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(
            user => user.Email.Equals(loginRequest.Email));



        if (user == null || !VerifyPasswordHash(loginRequest.Password, user.PasswordHash)) return null;

        var jwt = CreateJwt(user);
        await _unitOfWork.SaveChangesAsync();

        //var mappedUser = AuthResponseDto.MapFromUser(user, _mapper, jwt.Token);
        var mappedUser = AuthResponseDto.MapFromUser(user, _mapper);
        mappedUser.Token = jwt.Token;
        mappedUser.RefreshToken = jwt.RefreshToken.Token;

        return new AuthResultModel
        {
            AuthResponse = mappedUser,
            RefreshToken = jwt.RefreshToken
        };


    }

    async Task<AuthResultModel?> IAuthService.RegisterAsync(RegisterRequestDto registerRequest)
    {
        var userExists = await _unitOfWork.Users.AnyAsync(
            user => user.Email.Equals(registerRequest.Email)
            );

        if (userExists) return null;


        CreatePasswordHash(registerRequest.Password, out var passwordHash);

        var user = new User
        {
            Email = registerRequest.Email,
            Name = registerRequest.Name,
            PasswordHash = passwordHash,
            Address = registerRequest.Address,
            Role = "",
            //Cart = new Cart(),
            //Wishlist = new Wishlist(),
        };

        var jwt = CreateJwt(user);

        var entityEntry = await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        //var mappedUser = AuthResponseDto.MapFromUser(user, _mapper, jwt.Token);
        var mappedUser = AuthResponseDto.MapFromUser(user, _mapper);
        mappedUser.Id = entityEntry.Entity.Id;
        mappedUser.Token = jwt.Token;
        mappedUser.RefreshToken = jwt.RefreshToken.Token;

        return new AuthResultModel
        {
            AuthResponse = mappedUser,
            RefreshToken = jwt.RefreshToken
        };
    }

    private JwtModel CreateJwt(User user)
    {
        // Generate a new refresh token
        var token = GenerateToken(user);
        var refreshToken = GenerateRefreshToken();

        // Set the new refresh token on the user entity
        user.RefreshToken = refreshToken.Token;
        user.TokenCreated = refreshToken.CreatedTime;
        user.TokenExpires = refreshToken.ExpiredTime;

        return new JwtModel
        {
            Token = token,
            RefreshToken = refreshToken
        };
    }

    private string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(Constants.Token.UserIdClaim, user.Id.ToString()),
            new(nameof(ClaimTypes.Name), user.Name),
            new(nameof(ClaimTypes.Email), user.Email),
            new("Address", user.Address),
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration[Constants.Token.JwtKey] ?? throw new InvalidOperationException()));

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.Add(Constants.Token.TokenLife),
            signingCredentials: signingCredentials,
            issuer: _configuration[Constants.Token.JwtIssuer],
            audience: _configuration[Constants.Token.JwtAudience]
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static RefreshTokenModel GenerateRefreshToken()
    {
        return new RefreshTokenModel
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            ExpiredTime = DateTime.UtcNow.Add(Constants.Token.RefreshTokenLife),
            CreatedTime = DateTime.UtcNow
        };
    }
}

