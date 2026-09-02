namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents workspace organization size values.
    /// </summary>
    public class WorkspaceOrganizationSize : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceOrganizationSize"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private WorkspaceOrganizationSize(string value) : base(value) { }

        /// <summary>Gets size_1.</summary>
        public static WorkspaceOrganizationSize Size1 { get; } = new WorkspaceOrganizationSize("size_1");
        
        /// <summary>Gets size_2_to_10.</summary>
        public static WorkspaceOrganizationSize Size2To10 { get; } = new WorkspaceOrganizationSize("size_2_to_10");
        
        /// <summary>Gets size_11_to_50.</summary>
        public static WorkspaceOrganizationSize Size11To50 { get; } = new WorkspaceOrganizationSize("size_11_to_50");
        
        /// <summary>Gets size_51_to_100.</summary>
        public static WorkspaceOrganizationSize Size51To100 { get; } = new WorkspaceOrganizationSize("size_51_to_100");
        
        /// <summary>Gets size_101_to_250.</summary>
        public static WorkspaceOrganizationSize Size101To250 { get; } = new WorkspaceOrganizationSize("size_101_to_250");
        
        /// <summary>Gets size_51_to_250.</summary>
        public static WorkspaceOrganizationSize Size51To250 { get; } = new WorkspaceOrganizationSize("size_51_to_250");
        
        /// <summary>Gets more_than_250.</summary>
        public static WorkspaceOrganizationSize MoreThan250 { get; } = new WorkspaceOrganizationSize("more_than_250");
    }
}