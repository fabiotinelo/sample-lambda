using System;
using System.Diagnostics.CodeAnalysis;
using Iteris.Settings.Interface;

namespace Iteris.Settings
{
    [ExcludeFromCodeCoverage]
    public class EnvironmentVariable : IEnvironmentVariable
    {
        public string GetValueEnvironmenVariable(string variableName) => Environment.GetEnvironmentVariable(variableName.ToUpper());
    }
}
