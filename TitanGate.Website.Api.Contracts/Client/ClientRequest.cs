using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TitanGate.Website.Api.Contracts.ValidationUtility;

namespace TitanGate.Website.Api.Contracts
{
    public class ClientRequest: IValidatableObject
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(ClientId))
            {
                yield return Validation.GetIsMissingResult(nameof(ClientId));
            }
            if (ClientId?.Length > 50)
            {
                yield return Validation.GetDataIsTooLongResult(nameof(ClientId), 50);
            }
            if (string.IsNullOrWhiteSpace(ClientSecret))
            {
                yield return Validation.GetIsMissingResult(nameof(ClientSecret));
            }
            if (ClientSecret?.Length > 50)
            {
                yield return Validation.GetDataIsTooLongResult(nameof(ClientSecret), 50);
            }
        }
    }
}
