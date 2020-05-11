namespace TitanGate.Website.Api.Domain.Entities
{
    public class Login : BaseEntity<int>
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public int LoginOfWebsiteId { get; set; }

        public Website Website { get; set; }
    }
}
