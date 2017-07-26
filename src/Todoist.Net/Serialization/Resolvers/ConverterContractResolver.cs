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

            return property;
        }
    }
}
