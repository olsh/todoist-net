using System;

using Newtonsoft.Json.Serialization;

using Todoist.Net.Models;
using Todoist.Net.Serialization.Converters;

namespace Todoist.Net.Serialization.Resolvers
{
    internal class ConverterContractResolver : DefaultContractResolver
    {
        public static readonly ConverterContractResolver Instance = new ConverterContractResolver();

        protected override JsonContract CreateContract(Type objectType)
        {
            JsonContract contract = base.CreateContract(objectType);

            if (objectType == typeof(ComplexId) || objectType == typeof(ComplexId?))
            {
                contract.Converter = new ComplexIdConverter();
            }
            else if (objectType == typeof(ResourceType))
            {
                contract.Converter = new ResourceTypeConverter();
            }

            return contract;
        }
    }
}
