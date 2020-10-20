using System;
using System.Diagnostics.CodeAnalysis;

namespace Iteris.Settings
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Property)]
    public class EnvironmentVariableAttribute : Attribute
    {
        public EnvironmentVariableAttribute(string name)
        {
            this.name = name;
        }

        public EnvironmentVariableAttribute(string name, bool required)
        {
            this.name = name;
            this.required = required;
        }

        private string name;
        public string Name { get => name; set => name = value; }

        private bool required;
        public bool Required { get => required; set => required = value; }
    }
}
