namespace TitanGate.Website.Api.Domain.Settings
{
    public class ClientApiSettings
    {
        public string JwtKey { get; set; }

        public string JwtIssuer { get; set; }

        public string JwtExpirationInMinutes { get; set; }
    }
}
