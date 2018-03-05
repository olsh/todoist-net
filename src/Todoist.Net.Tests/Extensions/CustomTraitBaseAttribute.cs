using System;

namespace Todoist.Net.Tests.Extensions
{
    public class CustomTraitBaseAttribute : Attribute
    {
        public string Name { get; protected set; }
    }
}
