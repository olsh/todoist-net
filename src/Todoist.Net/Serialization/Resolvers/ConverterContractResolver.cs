using System;
using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Todoist.Net.Models;
using Todoist.Net.Serialization.Converters;

namespace Todoist.Net.Serialization.Resolvers
{
    internal class ConverterContractResolver : DefaultContractResolver
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            JsonContract contract = base.CreateContract(objectType);

            if (objectType == typeof(ComplexId) || objectType == typeof(ComplexId?))
            {
                contract.Converter = new ComplexIdConverter();
            }
            else if (objectType.GetTypeInfo().IsSubclassOf(typeof(StringEnum)))
            {
                contract.Converter = new StringEnumTypeConverter();
            }

            return contract;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            if (property.DeclaringType == typeof(UserInfo))
            {
                property.ShouldSerialize = instance => false;
            }
            if (typeof(INonNullDefault).IsAssignableFrom(property.PropertyType) && member is PropertyInfo propertyInfo)
            {
                property.NullValueHandling = NullValueHandling.Include;
                property.ShouldSerialize = instance =>
                {
                    var value = propertyInfo.GetValue(instance, null) as INonNullDefault;
                    return value?.IsDefault != true; // Serialize null and non-default values (as long as IsDefault isn't true).
                };
            }

            return property;
        }
    }
}
