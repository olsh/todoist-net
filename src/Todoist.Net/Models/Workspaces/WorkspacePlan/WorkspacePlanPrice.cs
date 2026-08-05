using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents the selected workspace plan price.
    /// </summary>
    public class WorkspacePlanPrice
    {
        /// <summary>
        /// Gets the formatted amount.
        /// </summary>
        [JsonPropertyName("amount")]
        public string Amount { get; internal set; }

        /// <summary>
        /// Gets the raw numeric amount.
        /// </summary>
        [JsonPropertyName("raw_amount")]
        public decimal RawAmount { get; internal set; }

        /// <summary>
        /// Gets the currency code.
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; internal set; }

        /// <summary>
        /// Gets the billing cycle.
        /// </summary>
        [JsonPropertyName("billing_cycle")]
        public string BillingCycle { get; internal set; }

        /// <summary>
        /// Gets the tax behavior.
        /// </summary>
        [JsonPropertyName("tax_behavior")]
        public string TaxBehavior { get; internal set; }
    }
}
