using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TitanGate.Website.Api.Contracts.ValidationUtility;

namespace TitanGate.Website.Api.Contracts
{
    public class LoginRequest : IValidatableObject
    {
        public int? Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [PasswordPropertyText]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                yield return Validation.GetIsMissingResult(nameof(Email));
            }
            if (Email?.Length > 50)
            {
                yield return Validation.GetDataIsTooLongResult(nameof(Email), 50);
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                yield return Validation.GetIsMissingResult(nameof(Password));
            }
            if (Password?.Length > 50)
            {
                yield return Validation.GetDataIsTooLongResult(nameof(Password), 50);
            }
        }
    }
}