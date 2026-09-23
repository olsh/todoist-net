namespace Todoist.Net.Tests.Extensions;

internal static class Constants
{
    public const string TraitName = "trait";

    public const string UnitTraitValue = "unit";

    public const string IntegrationFreeTraitValue = "integration-free";

    public const string IntegrationPremiumTraitValue = "integration-premium";

    public const string IntegrationCollaborationTraitValue = "integration-collaboration";

    /// <summary>
    /// These tests need a person to authorize a Todoist OAuth application in a browser, so they are explicit.
    /// </summary>
    public const string OAuthInteractiveTraitValue = "oauth-interactive";
}
