using System;

using Newtonsoft.Json;

using Todoist.Net.Serialization.Converters;

namespace Todoist.Net.Models
{
    internal class CompleteRecurringItemArgument : ICommandArgument
    {
        public CompleteRecurringItemArgument(RecurringItemState recurringItemState)
        {
            Id = recurringItemState.Id;
            DateString = recurringItemState.DateString;
            IsForward = recurringItemState.IsForward;
            NewDate = recurringItemState.NewDate;
        }

        [JsonProperty("date_string")]
        public string DateString { get; }

        [JsonProperty("id")]
        public ComplexId Id { get; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("is_forward")]
        public bool? IsForward { get; }

        [JsonConverter(typeof(UtcDateTimeConverter))]
        [JsonProperty("new_date_utc")]
        public DateTime? NewDate { get; }
    }
}
