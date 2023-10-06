namespace Billine.Admin.Api.Authorization
{
    public class JwtOptions
    {
        public const string Issuer = "https://api.billine.com.br";

        public const string Audience = "https://client.billine.com.br";

        public string Secret { get; set; }
    }
}
