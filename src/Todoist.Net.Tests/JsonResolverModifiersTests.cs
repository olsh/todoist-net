using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

using Todoist.Net.Models;
using Todoist.Net.Serialization.Resolvers;
using Todoist.Net.Tests.Extensions;

using Xunit;

namespace Todoist.Net.Tests
{
    [Trait(Constants.TraitName, Constants.UnitTraitValue)]
    public class JsonResolverModifiersTests
    {
        private class UnsettablePropertiesModel : IUnsettableProperties
        {
            HashSet<PropertyInfo> IUnsettableProperties.UnsetProperties { get; } = [];

            [JsonPropertyName("first_property")]
            public string Property1 { get; set; }

            [JsonPropertyName("second_property")]
            public int? Property2 { get; set; }

            [JsonPropertyName("third_property")]
            public bool? Property3 { get; set; }
        }

        private static readonly JsonSerializerOptions _serializerOptions = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers = { JsonResolverModifiers.IncludeUnsetProperties }
            }
        };


        [Fact]
        public void IncludeUnsetProperties_NoUnsetProperties_HasNoNulls()
        {
            var model = new UnsettablePropertiesModel
            {
                Property1 = "Test"
            };

            var json = JsonSerializer.Serialize(model, _serializerOptions);

            Assert.Contains("\"first_property\":\"Test\"", json);
            Assert.DoesNotContain("second_property", json);
            Assert.DoesNotContain("null", json);

            model = new UnsettablePropertiesModel
            {
                Property2 = 5
            };

            json = JsonSerializer.Serialize(model, _serializerOptions);

            Assert.Contains("\"second_property\":5", json);
            Assert.DoesNotContain("first_property", json);
            Assert.DoesNotContain("null", json);
        }

        [Fact]
        public void IncludeUnsetProperties_WithUnsetProperty_IncludeNull()
        {
            var model = new UnsettablePropertiesModel
            {
                Property1 = "Test"
            };
            model.Unset(x => x.Property2);

            var json = JsonSerializer.Serialize(model, _serializerOptions);

            Assert.Contains("\"first_property\":\"Test\"", json);
            Assert.Contains("\"second_property\":null", json);
            Assert.DoesNotContain("third_property", json);

            model = new UnsettablePropertiesModel
            {
                Property2 = 5
            };
            model.Unset(x => x.Property1);

            json = JsonSerializer.Serialize(model, _serializerOptions);

            Assert.Contains("\"first_property\":null", json);
            Assert.Contains("\"second_property\":5", json);
            Assert.DoesNotContain("third_property", json);
        }

    }
}
