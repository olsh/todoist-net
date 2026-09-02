namespace Todoist.Net.Tests.Extensions;

internal static class Constants
{
    public const string TraitName = "trait";

    public const string UnitTraitValue = "unit";

    public const string IntegrationFreeTraitValue = "integration-free";

    public const string IntegrationPremiumTraitValue = "integration-premium";

    public const string IntegrationCollaborationTraitValue = "integration-collaboration";

    /// <summary>
    /// These kind of test won't work with MFA enabled.
    /// </summary>
    public const string MfaRequiredTraitValue = "mfa-required";
}
