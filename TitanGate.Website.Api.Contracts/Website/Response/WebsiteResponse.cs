namespace TitanGate.Website.Api.Contracts.Response
{
    public class WebsiteResponse
    {
        public int? Id  { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public Category Category { get; set; }

        public LoginResponse Login { get; set; }

        public string HomepageSnapshot { get; set; }
    }
}
