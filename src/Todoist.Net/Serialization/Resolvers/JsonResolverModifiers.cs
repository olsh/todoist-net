using System.Text.Json.Serialization.Metadata;

using Todoist.Net.Models;

namespace Todoist.Net.Serialization.Resolvers
{
    internal static class JsonResolverModifiers
    {
        public static void FilterSerializationByType(JsonTypeInfo typeInfo)
        {
            if (typeInfo.Type == typeof(UserInfo))
            {
                foreach (var propertyInfo in typeInfo.Properties)
                {
                    propertyInfo.ShouldSerialize = (obj, value) => false;
                }
                return;
            }
            foreach (var propertyInfo in typeInfo.Properties)
            {
                // Null DueDate == no DueDate, so we should always send the DueDate
                // https://developer.todoist.com/sync/v9/#due-dates
                if (propertyInfo.PropertyType == typeof(DueDate))
                {
                    propertyInfo.ShouldSerialize = (obj, value) => true;
                }
            }
        }
    }
}
