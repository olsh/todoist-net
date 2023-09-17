using System.Collections.Generic;
using System.Threading.Tasks;

using Todoist.Net.Models;

namespace Todoist.Net.Services
{
    /// <summary>
    /// Contains methods for sections management.
    /// </summary>
    /// <seealso cref="Todoist.Net.Services.ISectionService" />
    /// <seealso cref="Todoist.Net.Services.SectionsCommandService" />
    internal class SectionService : SectionsCommandService, ISectionService
    {
        internal SectionService(IAdvancedTodoistClient todoistClient)
            : base(todoistClient)
        {
        }

        /// <inheritdoc/>
        public Task<Section> GetAsync(ComplexId id)
        {
            return TodoistClient.PostAsync<Section>(
                "sections/get",
                new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("section_id", id.ToString())
                    });
        }
    }
}
