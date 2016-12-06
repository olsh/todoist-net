using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Todoist.Net.Models
{
    internal class IdsArgument : ICommandArgument
    {
        public IdsArgument()
        {
            Ids = new List<ComplexId>();
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
        public ICollection<ComplexId> Ids { get; }
    }
}
