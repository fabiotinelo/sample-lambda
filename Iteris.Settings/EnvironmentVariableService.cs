using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Iteris.Settings.Interface;

namespace Iteris.Settings
{
    [ExcludeFromCodeCoverage]
    public class EnvironmentVariableService : IEnvironmentVariableService
    {
        private readonly IEnvironmentVariable environmentVariable;

        public EnvironmentVariableService(IEnvironmentVariable environmentVariable)
        {
            this.environmentVariable = environmentVariable;
        }


        public object SetProperties(object obj)
        {
            return SetProperties(obj, null);
        }


        public object SetProperties(object obj, ISecurityData securityData)
        {

            string variableName;
            string value;
            Type objType = obj.GetType();
            IEnumerable<PropertyInfo> properties = objType.GetRuntimeProperties();

            foreach (PropertyInfo property in properties)
            {

                if (property.GetCustomAttribute<IgnorePropertyAttribute>() != null)
                    continue;
                else if (!property.IsValueType())
                {
                    property.CreateInstance(obj);
                    SetProperties(property.GetValue(obj), securityData);
                    continue;
                }
                else if (property.GetCustomAttribute<EnvironmentVariableAttribute>() != null)
                {
                    variableName = property.GetVariableName();
                    value = environmentVariable.GetValueEnvironmenVariable(variableName);

                    if (securityData != null && securityData.ContainsPrefix(value))
                        value = securityData.GetSecret(value);

                    property.SetProperty(obj, value);
                }

            }
            return obj;
        }

    }


}
