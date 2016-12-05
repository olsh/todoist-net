using System;
using System.Collections.Generic;

namespace Todoist.Net.Models
{
    public interface IWithRelationsArgument
    {
        void UpdateRelatedTempIds(IDictionary<Guid, int> map);
    }
}