using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TitanGate.Website.Api.Contracts.ValidationUtility;

namespace TitanGate.Website.Api.Contracts
{
    public class Category: IValidatableObject
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                yield return Validation.GetIsMissingResult(nameof(Name));
            }
            if (Name?.Length > 50)
            {
                yield return Validation.GetDataIsTooLongResult(nameof(Name), 50);
            }
        }
    }
}