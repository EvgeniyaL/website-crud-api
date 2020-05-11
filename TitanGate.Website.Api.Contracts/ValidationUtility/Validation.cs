using System.ComponentModel.DataAnnotations;

namespace TitanGate.Website.Api.Contracts.ValidationUtility
{
    internal static class Validation
    {
        public static ValidationResult GetGreaterThanZeroResult(string propertyName)
        {
            return new ValidationResult($"{propertyName} must be greater than zero.", new[] { propertyName });
        }

        public static ValidationResult GetIsMissingResult(string propertyName)
        {
            return new ValidationResult($"{propertyName} is missing.", new[] { propertyName });
        }

        public static ValidationResult GetDataIsTooLongResult(string propertyName, int length)
        {
            return new ValidationResult($"{propertyName} is too long it needs to be less than {length} symbols.", new[] { propertyName });
        }
        public static ValidationResult GetInvalidImageString(string propertyName)
        {
            return new ValidationResult($"{propertyName} is not a valid Base64 string", new[] { propertyName });
        }
    }
}
