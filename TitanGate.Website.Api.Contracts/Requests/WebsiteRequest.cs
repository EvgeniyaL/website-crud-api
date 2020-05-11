using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TitanGate.Website.Api.Contracts.ValidationUtility;

namespace TitanGate.Website.Api.Contracts.Requests
{
    public class WebsiteRequest : IValidatableObject
    {
        public int? Id  { get; set; }
        public string Name { get; set; }

        [Url]
        public string Url { get; set; }

        public Category Category { get; set; }

        public LoginRequest Login { get; set; }

        public string HomepageSnapshot { get; set; }

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
            if (string.IsNullOrWhiteSpace(Url))
            {
                yield return Validation.GetIsMissingResult(nameof(Url));
            }
            if (Category is null)
            {
                yield return Validation.GetIsMissingResult(nameof(Category));
            }
            if (Login is null)
            {
                yield return Validation.GetIsMissingResult(nameof(Login));
            }
            if (string.IsNullOrWhiteSpace(HomepageSnapshot))
            {
                yield return Validation.GetIsMissingResult(nameof(HomepageSnapshot));
            }
            if (!CheckIsBase64String(HomepageSnapshot))
            {
                yield return Validation.GetInvalidImageString(nameof(HomepageSnapshot));
            }
        }

        private bool CheckIsBase64String(string homepageSnapshot)
        {
            try
            {
                Convert.FromBase64String(homepageSnapshot);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
