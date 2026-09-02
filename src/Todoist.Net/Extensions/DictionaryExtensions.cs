using System.Collections.Generic;
using System.Linq;

namespace Todoist.Net.Models
{
    internal static class DictionaryExtensions
    {
        public static void AddIfNotNullOrEmpty(this Dictionary<string, string> dictionary, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                dictionary.Add(key, value);
            }
        }

        public static void AddIfHasValue<T>(this Dictionary<string, string> dictionary, string key, T? value) 
            where T : struct
        {
            if (value.HasValue)
            {
                dictionary.Add(key, value.Value.ToString());
            }
        }

        public static void AddIfTrue(this Dictionary<string, string> dictionary, string key, bool value)
        {
            if (value)
            {
                dictionary.Add(key, "true");
            }
        }

        public static Dictionary<string, string> ToNonEmptyValuesDictionary(this Dictionary<string, string> dictionary)
        {
            var filtered = new Dictionary<string, string>();
            foreach (var kvp in dictionary.Where(kvp => !string.IsNullOrEmpty(kvp.Value)))
            {
                filtered.Add(kvp.Key, kvp.Value);
            }
            return filtered;
        }

        public static Dictionary<string, string> MergeWith(this Dictionary<string, string> dictionary, IEnumerable<KeyValuePair<string, string>> additional)
        {
            var merged = new Dictionary<string, string>(dictionary);
            foreach (var kvp in additional)
            {
                merged[kvp.Key] = kvp.Value; // This will add or overwrite existing keys
            }
            return merged;
        }
    }
}
