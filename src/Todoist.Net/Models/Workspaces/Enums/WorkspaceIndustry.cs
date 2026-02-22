namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents workspace industry values.
    /// </summary>
    public class WorkspaceIndustry : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceIndustry"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private WorkspaceIndustry(string value) : base(value) { }

        /// <summary>Gets agriculture.</summary>
        public static WorkspaceIndustry Agriculture { get; } = new WorkspaceIndustry("agriculture");
        
        /// <summary>Gets arts and entertainment.</summary>
        public static WorkspaceIndustry ArtsEntertainment { get; } = new WorkspaceIndustry("arts_entertainment");
        
        /// <summary>Gets automotive.</summary>
        public static WorkspaceIndustry Automotive { get; } = new WorkspaceIndustry("automotive");
        
        /// <summary>Gets banking and financial services.</summary>
        public static WorkspaceIndustry BankingFinancialServices { get; } = new WorkspaceIndustry("banking_financial_services");
        
        /// <summary>Gets construction.</summary>
        public static WorkspaceIndustry Construction { get; } = new WorkspaceIndustry("construction");
        
        /// <summary>Gets consulting.</summary>
        public static WorkspaceIndustry Consulting { get; } = new WorkspaceIndustry("consulting");
        
        /// <summary>Gets consumer goods.</summary>
        public static WorkspaceIndustry ConsumerGoods { get; } = new WorkspaceIndustry("consumer_goods");
        
        /// <summary>Gets education.</summary>
        public static WorkspaceIndustry Education { get; } = new WorkspaceIndustry("education");
        
        /// <summary>Gets energy and utilities.</summary>
        public static WorkspaceIndustry EnergyUtilities { get; } = new WorkspaceIndustry("energy_utilities");
        
        /// <summary>Gets food and beverages.</summary>
        public static WorkspaceIndustry FoodBeverages { get; } = new WorkspaceIndustry("food_beverages");
        
        /// <summary>Gets government and public sector.</summary>
        public static WorkspaceIndustry GovernmentPublicSector { get; } = new WorkspaceIndustry("government_public_sector");
        
        /// <summary>Gets healthcare and life sciences.</summary>
        public static WorkspaceIndustry HealthcareLifeSciences { get; } = new WorkspaceIndustry("healthcare_life_sciences");
        
        /// <summary>Gets information technology.</summary>
        public static WorkspaceIndustry InformationTechnology { get; } = new WorkspaceIndustry("information_technology");
        
        /// <summary>Gets insurance.</summary>
        public static WorkspaceIndustry Insurance { get; } = new WorkspaceIndustry("insurance");
        
        /// <summary>Gets legal services.</summary>
        public static WorkspaceIndustry LegalServices { get; } = new WorkspaceIndustry("legal_services");
        
        /// <summary>Gets manufacturing.</summary>
        public static WorkspaceIndustry Manufacturing { get; } = new WorkspaceIndustry("manufacturing");
        
        /// <summary>Gets media and communications.</summary>
        public static WorkspaceIndustry MediaCommunications { get; } = new WorkspaceIndustry("media_communications");
        
        /// <summary>Gets non-profit.</summary>
        public static WorkspaceIndustry NonProfit { get; } = new WorkspaceIndustry("non_profit");
        
        /// <summary>Gets pharmaceuticals.</summary>
        public static WorkspaceIndustry Pharmaceuticals { get; } = new WorkspaceIndustry("pharmaceuticals");
        
        /// <summary>Gets real estate.</summary>
        public static WorkspaceIndustry RealEstate { get; } = new WorkspaceIndustry("real_estate");
        
        /// <summary>Gets retail and wholesale.</summary>
        public static WorkspaceIndustry RetailWholesale { get; } = new WorkspaceIndustry("retail_wholesale");
        
        /// <summary>Gets telecommunications.</summary>
        public static WorkspaceIndustry Telecommunications { get; } = new WorkspaceIndustry("telecommunications");
        
        /// <summary>Gets transportation and logistics.</summary>
        public static WorkspaceIndustry TransportationLogistics { get; } = new WorkspaceIndustry("transportation_logistics");
       
        /// <summary>Gets travel and hospitality.</summary>
        public static WorkspaceIndustry TravelHospitality { get; } = new WorkspaceIndustry("travel_hospitality");
        
        /// <summary>Gets other.</summary>
        public static WorkspaceIndustry Other { get; } = new WorkspaceIndustry("other");
    }
}