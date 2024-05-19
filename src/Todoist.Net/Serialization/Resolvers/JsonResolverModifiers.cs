using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

using Todoist.Net.Models;

namespace Todoist.Net.Serialization.Resolvers
{
    internal static class JsonResolverModifiers
    {
        public static void SerializeInternalSetters(JsonTypeInfo typeInfo)
        {
            string GetPropertyName(PropertyInfo property)
            {
                var propertyNameAttribute = property.GetCustomAttribute<JsonPropertyNameAttribute>();

                return propertyNameAttribute?.Name
                    ?? typeInfo.Options.PropertyNamingPolicy?.ConvertName(property.Name)
                    ?? property.Name;
            }

            var internalSetterProperties = typeInfo.Type
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(property => property.SetMethod?.IsAssembly == true);

            var nullSetterJsonProperties = typeInfo.Properties
                .Where(property => property.Set == null);

            var joinedProperties = internalSetterProperties
                .Select(property => new
                {
                    Property = property,
                    Name = GetPropertyName(property)
                })
                .Join(nullSetterJsonProperties, x => x.Name, y => y.Name, (x, y) => new
                {
                    PropertyInfo = x.Property,
                    JsonPropertyInfo = y
                });

            foreach (var property in joinedProperties)
            {
                property.JsonPropertyInfo.Set = property.PropertyInfo.SetValue;
            }
        }

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
