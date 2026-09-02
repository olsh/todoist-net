using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents plan pricing grouped by billing cycle.
    /// </summary>
    public class WorkspacePlanPriceListItem
    {
        /// <summary>
        /// Gets the billing cycle.
        /// </summary>
        [JsonPropertyName("billing_cycle")]
        public string BillingCycle { get; internal set; }

        /// <summary>
        /// Gets currency-specific prices for this billing cycle.
        /// </summary>
        [JsonPropertyName("prices")]
        public IReadOnlyList<WorkspacePlanPriceAmount> Prices { get; internal set; }
    }
    
    /// <summary>
    /// Represents a currency-specific plan price amount.
    /// </summary>
    public class WorkspacePlanPriceAmount
    {
        /// <summary>
        /// Gets the currency code.
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; internal set; }

        /// <summary>
        /// Gets the unit amount.
        /// </summary>
        [JsonPropertyName("unit_amount")]
        public int UnitAmount { get; internal set; }

        /// <summary>
        /// Gets the tax behavior.
        /// </summary>
        [JsonPropertyName("tax_behavior")]
        public string TaxBehavior { get; internal set; }
    }
}
