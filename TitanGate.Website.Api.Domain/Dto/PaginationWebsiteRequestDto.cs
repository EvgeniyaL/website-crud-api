namespace TitanGate.Website.Api.Domain.Dto
{
    public class PaginationWebsiteRequestDto
    {
        public int PageNumber { get; set; } 

        public int PageSize { get; set; } 

        public SortOrder SortOrder { get; set; }

        public SortOrderByProperty OrderByProperty { get; set; }
    }
}
