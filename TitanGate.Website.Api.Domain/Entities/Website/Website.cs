namespace TitanGate.Website.Api.Domain.Entities
{
    public class Website : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public Category Category { get; set; }

        public Login Login { get; set; }

        public bool IsDeleted { get; set; }

        public string FilePath { get; set; }
    }
}
