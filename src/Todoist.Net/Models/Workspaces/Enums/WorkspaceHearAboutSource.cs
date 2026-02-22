namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents workspace "how did you hear about us" values.
    /// </summary>
    public class WorkspaceHearAboutSource : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceHearAboutSource"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private WorkspaceHearAboutSource(string value) : base(value) { }

        /// <summary>Gets friend.</summary>
        public static WorkspaceHearAboutSource Friend { get; } = new WorkspaceHearAboutSource("friend");
        
        /// <summary>Gets social_media.</summary>
        public static WorkspaceHearAboutSource SocialMedia { get; } = new WorkspaceHearAboutSource("social_media");
        
        /// <summary>Gets ai_chatbot.</summary>
        public static WorkspaceHearAboutSource AiChatbot { get; } = new WorkspaceHearAboutSource("ai_chatbot");
        
        /// <summary>Gets search_engine.</summary>
        public static WorkspaceHearAboutSource SearchEngine { get; } = new WorkspaceHearAboutSource("search_engine");
        
        /// <summary>Gets app_store.</summary>
        public static WorkspaceHearAboutSource AppStore { get; } = new WorkspaceHearAboutSource("app_store");
        
        /// <summary>Gets other.</summary>
        public static WorkspaceHearAboutSource Other { get; } = new WorkspaceHearAboutSource("other");
    }
}