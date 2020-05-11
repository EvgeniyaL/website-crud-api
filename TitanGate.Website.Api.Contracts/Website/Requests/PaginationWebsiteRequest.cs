using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TitanGate.Website.Api.Contracts.ValidationUtility;

namespace TitanGate.Website.Api.Contracts.Request
{
    public class PaginationWebsiteRequest : IValidatableObject
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 5;

        public SortOrder SortOrder { get; set; } = SortOrder.Ascending;

        public SortOrderByProperty OrderByProperty { get; set; } = SortOrderByProperty.Name;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PageNumber <= 0)
                yield return Validation.GetGreaterThanZeroResult(nameof(PageNumber));
            if (PageSize <= 0)
                yield return Validation.GetGreaterThanZeroResult(nameof(PageSize));
        }
    }
}
