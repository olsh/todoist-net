namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents workspace region values.
    /// </summary>
    public class WorkspaceRegion : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceRegion"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private WorkspaceRegion(string value) : base(value) { }

        /// <summary>Gets AF.</summary>
        public static WorkspaceRegion Africa { get; } = new WorkspaceRegion("AF");
        
        /// <summary>Gets AS.</summary>
        public static WorkspaceRegion Asia { get; } = new WorkspaceRegion("AS");
        
        /// <summary>Gets EU.</summary>
        public static WorkspaceRegion Europe { get; } = new WorkspaceRegion("EU");
        
        /// <summary>Gets NA.</summary>
        public static WorkspaceRegion NorthAmerica { get; } = new WorkspaceRegion("NA");
        
        /// <summary>Gets SA.</summary>
        public static WorkspaceRegion SouthAmerica { get; } = new WorkspaceRegion("SA");
        
        /// <summary>Gets OC.</summary>
        public static WorkspaceRegion Oceania { get; } = new WorkspaceRegion("OC");
        
        /// <summary>Gets AN.</summary>
        public static WorkspaceRegion Antarctica { get; } = new WorkspaceRegion("AN");
    }
}