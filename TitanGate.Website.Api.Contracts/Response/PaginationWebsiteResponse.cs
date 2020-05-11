using System.Collections.Generic;

namespace TitanGate.Website.Api.Contracts.Response
{
    public class PaginationWebsiteResponse
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalRecordCount { get; set; }

        public int TotalPagesCount { get; set; }

        public IEnumerable<WebsiteResponse> Records { get; set; }
    }
}
