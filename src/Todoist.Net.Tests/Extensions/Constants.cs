namespace Todoist.Net.Tests.Extensions;

internal static class Constants
{
    public const string TodoistApiTestCollectionName = "todoist-api-tests";

    public const string TraitName = "trait";

    public const string UnitTraitValue = "unit";

    public const string IntegrationFreeTraitValue = "integration-free";

    public const string IntegrationPremiumTraitValue = "integration-premium";

    /// <summary>
    /// These kind of test won't work with MFA enabled.
    /// </summary>
    public const string MfaRequiredTraitValue = "mfa-required";
}
