using System;

namespace Todoist.Net.Tests.Extensions
{
    public class CustomTraitBase : Attribute
    {
        public string Name { get; protected set; }
    }
}
