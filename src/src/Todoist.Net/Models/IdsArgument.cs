using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using Todoist.Net.Models.Types;

namespace Todoist.Net.Models
{
    public class IdsArgument : ICommandArgument
    {
        public IdsArgument()
        {
            Ids = new List<ComplexId>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdsArgument"/> class.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <exception cref="ArgumentNullException"><paramref name="ids"/> is <see langword="null"/></exception>
        public IdsArgument(IEnumerable<int> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            Ids = new List<ComplexId>();
            foreach (var id in ids)
            {
                Ids.Add(id);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdsArgument"/> class.
        /// </summary>
        /// <param name="tempIds">The temporary ids.</param>
        /// <exception cref="ArgumentNullException"><paramref name="tempIds"/> is <see langword="null"/></exception>
        public IdsArgument(IEnumerable<ComplexId> tempIds)
        {
            if (tempIds == null)
            {
                throw new ArgumentNullException(nameof(tempIds));
            }

            Ids = new List<ComplexId>();
            foreach (var tempId in tempIds)
            {
                Ids.Add(tempId);
            }
        }

        [JsonProperty("ids")]
        public ICollection<ComplexId> Ids { get; internal set; }
    }
}
