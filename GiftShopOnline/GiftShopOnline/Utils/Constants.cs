namespace GiftShopOnline.Utils;

public class Constants
{
    public const string DbConnectionString = "GiftShopDb";

    public static class Files
    {
        private static readonly string? Storage =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "GiftShopOnline"
                );

        public static readonly string ProductPictures = $@"{Storage}\ProductPictures";
    }

    public static class Token
    {
        public const string UserIdClaim = "id";

        public const string JwtKey = "Jwt:Key";
        public const string JwtIssuer = "Jwt:Issuer";
        public const string JwtAudience = "Jwt:Audience";

        public const int OtpDigits = 4;

        public static readonly TimeSpan TokenLife = TimeSpan.FromHours(1);
        public static readonly TimeSpan RefreshTokenLife = TimeSpan.FromDays(7);
        public static readonly TimeSpan OtpLife = TimeSpan.FromMinutes(10);
    }
}

