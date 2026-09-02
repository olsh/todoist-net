using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents workspace plan details and usage summary.
    /// </summary>
    public class WorkspacePlanDetails
    {
        /// <summary>
        /// Gets the current member count.
        /// </summary>
        [JsonPropertyName("current_member_count")]
        public int CurrentMemberCount { get; internal set; }

        /// <summary>
        /// Gets the current plan name.
        /// </summary>
        [JsonPropertyName("current_plan")]
        public string CurrentPlan { get; internal set; }

        /// <summary>
        /// Gets the current plan status.
        /// </summary>
        [JsonPropertyName("current_plan_status")]
        public string CurrentPlanStatus { get; internal set; }

        /// <summary>
        /// Gets the downgrade timestamp.
        /// </summary>
        [JsonPropertyName("downgrade_at")]
        public string DowngradeAt { get; internal set; }

        /// <summary>
        /// Gets the current active projects count.
        /// </summary>
        [JsonPropertyName("current_active_projects")]
        public int CurrentActiveProjects { get; internal set; }

        /// <summary>
        /// Gets the maximum active projects count.
        /// </summary>
        [JsonPropertyName("maximum_active_projects")]
        public int MaximumActiveProjects { get; internal set; }

        /// <summary>
        /// Gets available plan prices by billing cycle.
        /// </summary>
        [JsonPropertyName("price_list")]
        public IReadOnlyList<WorkspacePlanPriceListItem> PriceList { get; internal set; }

        /// <summary>
        /// Gets the workspace identifier.
        /// </summary>
        [JsonPropertyName("workspace_id")]
        public long WorkspaceId { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the workspace is trialing.
        /// </summary>
        [JsonPropertyName("is_trialing")]
        public bool IsTrialing { get; internal set; }

        /// <summary>
        /// Gets the trial end timestamp.
        /// </summary>
        [JsonPropertyName("trial_ends_at")]
        public string TrialEndsAt { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether cancel at period end is enabled.
        /// </summary>
        [JsonPropertyName("cancel_at_period_end")]
        public bool CancelAtPeriodEnd { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether this workspace has previously used a trial.
        /// </summary>
        [JsonPropertyName("has_trialed")]
        public bool HasTrialed { get; internal set; }

        /// <summary>
        /// Gets the currently selected plan price.
        /// </summary>
        [JsonPropertyName("plan_price")]
        public WorkspacePlanPrice PlanPrice { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether billing portal is available.
        /// </summary>
        [JsonPropertyName("has_billing_portal")]
        public bool HasBillingPortal { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether billing portal annual-switch is available.
        /// </summary>
        [JsonPropertyName("has_billing_portal_switch_to_annual")]
        public bool HasBillingPortalSwitchToAnnual { get; internal set; }
    }
}
