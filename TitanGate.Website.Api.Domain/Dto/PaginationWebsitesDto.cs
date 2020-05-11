using System.Collections.Generic;

namespace TitanGate.Website.Api.Domain.Dto
{
    using Website = Entities.Website;

     public class PaginationWebsitesDto
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalRecordCount { get; set; }

        public int TotalPagesCount { get; set; }

        public IEnumerable<Website> Records { get; set; }
    }
}
