namespace TitanGate.Website.Api.Domain.Entities
{
    public class Client: BaseEntity<int>
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
