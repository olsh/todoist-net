namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents workspace department values.
    /// </summary>
    public class WorkspaceDepartment : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceDepartment"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private WorkspaceDepartment(string value) : base(value) { }

        /// <summary>Gets administration.</summary>
        public static WorkspaceDepartment Administration { get; } = new WorkspaceDepartment("administration");
        
        /// <summary>Gets customer service.</summary>
        public static WorkspaceDepartment CustomerService { get; } = new WorkspaceDepartment("customer_service");
        
        /// <summary>Gets finance and accounting.</summary>
        public static WorkspaceDepartment FinanceAccounting { get; } = new WorkspaceDepartment("finance_accounting");
        
        /// <summary>Gets human resources.</summary>
        public static WorkspaceDepartment HumanResources { get; } = new WorkspaceDepartment("human_resources");
        
        /// <summary>Gets information technology.</summary>
        public static WorkspaceDepartment InformationTechnology { get; } = new WorkspaceDepartment("information_technology");
        
        /// <summary>Gets legal.</summary>
        public static WorkspaceDepartment Legal { get; } = new WorkspaceDepartment("legal");
        
        /// <summary>Gets marketing.</summary>
        public static WorkspaceDepartment Marketing { get; } = new WorkspaceDepartment("marketing");
        
        /// <summary>Gets operations.</summary>
        public static WorkspaceDepartment Operations { get; } = new WorkspaceDepartment("operations");
        
        /// <summary>Gets product development.</summary>
        public static WorkspaceDepartment ProductDevelopment { get; } = new WorkspaceDepartment("product_development");
        
        /// <summary>Gets research and development.</summary>
        public static WorkspaceDepartment ResearchDevelopment { get; } = new WorkspaceDepartment("research_development");
        
        /// <summary>Gets sales.</summary>
        public static WorkspaceDepartment Sales { get; } = new WorkspaceDepartment("sales");
        
        /// <summary>Gets supply chain management.</summary>
        public static WorkspaceDepartment SupplyChainManagement { get; } = new WorkspaceDepartment("supply_chain_management");
        
        /// <summary>Gets engineering.</summary>
        public static WorkspaceDepartment Engineering { get; } = new WorkspaceDepartment("engineering");
        
        /// <summary>Gets quality assurance.</summary>
        public static WorkspaceDepartment QualityAssurance { get; } = new WorkspaceDepartment("quality_assurance");
        
        /// <summary>Gets executive management.</summary>
        public static WorkspaceDepartment ExecutiveManagement { get; } = new WorkspaceDepartment("executive_management");
        
        /// <summary>Gets other.</summary>
        public static WorkspaceDepartment Other { get; } = new WorkspaceDepartment("other");
    }
}