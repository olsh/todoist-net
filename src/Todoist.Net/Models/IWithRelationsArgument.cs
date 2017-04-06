using System;
using System.Collections.Generic;

namespace Todoist.Net.Models
{
    internal interface IWithRelationsArgument
    {
        void UpdateRelatedTempIds(IDictionary<Guid, long> map);
    }
}
