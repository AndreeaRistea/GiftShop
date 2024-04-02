using Microsoft.AspNetCore.Http;
using GiftShopOnline.Utils;
using System.Security.Claims;

namespace GiftShopOnline.Helpers;

public class CurrentUser 
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private string _email;
    private string _name;
    private Guid? _id;
    private string _address;
    public CurrentUser (IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? Id
    {
        get
        {
            if (_id != null) return _id;

            var id = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(claim => claim.Type.Equals(Constants.Token.UserIdClaim))?.Value;

            if (id == null) return null;

            return _id = new Guid(id);
        }
    }

    public string? Name
    {
        get
        {
            if (_name != null) return _name;

            return _name = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(claim => claim.Type.Equals(nameof(ClaimTypes.Name)))?.Value;
        }
    }

    public string? Email
    {
        get
        {
            if (_email != null) return _email;

            return _email = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(claim => claim.Type.Equals(nameof(ClaimTypes.Email)))?.Value;
        }
    }

    public string? Address
    {
        get
        {
            if (_address != null) return _address;

            return _address = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(claim => claim.Type.Equals("Address"))?.Value;
        }
    }
}

