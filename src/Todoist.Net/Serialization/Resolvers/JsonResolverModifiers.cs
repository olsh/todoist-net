using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

using Todoist.Net.Models;

namespace Todoist.Net.Serialization.Resolvers
{
    internal static class JsonResolverModifiers
    {
        public static void SerializeInternalSetters(JsonTypeInfo typeInfo)
        {
            var internalSetterProperties = typeInfo.Type
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(property => property.SetMethod?.IsAssembly == true);

            var nullSetterJsonProperties = typeInfo.Properties
                .Where(property => property.Set == null);

            var joinedProperties = internalSetterProperties
                .Select(property => new
                {
                    Property = property,
                    Name = GetPropertyName(property, typeInfo.Options)
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
            }
        }

        public static void IncludeUnsetProperties(JsonTypeInfo typeInfo)
        {
            // There's no use for this contract if the default ignore condition is set to Never.
            if (typeInfo.Options.DefaultIgnoreCondition is JsonIgnoreCondition.Never)
            {
                return;
            }
            if (!typeof(IUnsettableProperties).IsAssignableFrom(typeInfo.Type))
            {
                return;
            }
            foreach (var propertyInfo in typeInfo.Properties)
            {
                if (propertyInfo.ShouldSerialize != null)
                {
                    // Ignore properties that have been configured by previous modifiers.
                    // Note: ShouldSerialize equals null by default unless modified on purpose.
                    continue;
                }
                propertyInfo.ShouldSerialize = (obj, value) =>
                {
                    if (value != null)
                    {
                        // Return default behavior if the property has a value.
                        return ShouldSerializeByDefault(propertyInfo);
                    }
                    return ((IUnsettableProperties)obj)
                        .UnsetProperties
                        .Any(property => GetPropertyName(property, typeInfo.Options) == propertyInfo.Name);
                };
            }
        }


        private static bool ShouldSerializeByDefault(JsonPropertyInfo propertyInfo)
        {
            // Properties that has no setter delegate are assumed to be readonly,
            // while properties that has a setter delegate should be serialized normally.
            if (propertyInfo.Set != null)
            {
                return true;
            }
            var options = propertyInfo.Options;

            if (options.IgnoreReadOnlyFields && options.IncludeFields)
            {
                return false;
            }
            if (options.IgnoreReadOnlyProperties)
            {
                return false;
            }
            return true;
        }

        private static string GetPropertyName(PropertyInfo property, JsonSerializerOptions options)
        {
            var propertyNameAttribute = property.GetCustomAttribute<JsonPropertyNameAttribute>();

            return propertyNameAttribute?.Name
                ?? options.PropertyNamingPolicy?.ConvertName(property.Name)
                ?? property.Name;
        }

    }
}
