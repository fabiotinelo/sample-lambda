using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;


namespace Iteris.Settings
{
    public static class PropertyExtension
    {
        /// <summary>
        /// Convert uma string para o tipo especifico definido na propriedade
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ConvertValueVariable(this PropertyInfo property, string value)
        {
            Type propertyType = property.PropertyType;
            object objConverted = value;

            if (string.IsNullOrEmpty(value))
                return null;
                     
            if (property.PropertyType.IsEnum)
            { Enum.TryParse(propertyType, value, out object ret); objConverted = ret; }
            else if (propertyType == typeof(string)) objConverted = value;
            else if (propertyType == typeof(int))
            { int.TryParse(value, out int ret); objConverted = ret; }
            else if (propertyType == typeof(Int64))
            { Int64.TryParse(value, out Int64 ret); objConverted = ret; }
           // else if (propertyType == typeof(Int32))
           // { Int32.TryParse(value, out Int32 ret); objConverted = ret; }
            else if (propertyType == typeof(Int16))
            { Int16.TryParse(value, out Int16 ret); objConverted = ret; }
            else if (propertyType == typeof(uint))
            { uint.TryParse(value, out uint ret); objConverted = ret; }
            else if (propertyType == typeof(UInt16))
            { UInt16.TryParse(value, out UInt16 ret); objConverted = ret; }
           // else if (propertyType == typeof(UInt32))
           /// { UInt32.TryParse(value, out UInt32 ret); objConverted = ret; }
            else if (propertyType == typeof(UInt64))
            { UInt64.TryParse(value, out UInt64 ret); objConverted = ret; }
            else if (propertyType == typeof(decimal))
            { decimal.TryParse(value, out decimal ret); objConverted = ret; }
            else if (propertyType == typeof(DateTime))
            { DateTime.TryParse(value, out DateTime ret); objConverted = ret; }
            else if (propertyType == typeof(double))
            { double.TryParse(value, out double ret); objConverted = ret; }
            else if (propertyType == typeof(float))
            { float.TryParse(value, out float ret); objConverted = ret; }
            else if (propertyType == typeof(bool))
            { bool.TryParse(value, out bool ret); objConverted = ret; }
            else if (propertyType == typeof(char))
            { char.TryParse(value, out char ret); objConverted = ret; }

            return objConverted;
        }

        public static bool IsValueType(this PropertyInfo property)
        {
            return (property.PropertyType.IsEnum ||
                       property.PropertyType.FullName.StartsWith("System.", StringComparison.InvariantCultureIgnoreCase)
                   );
        }

        public static void CreateInstance(this PropertyInfo property, object obj)
        {
            object instanceProperty = Activator.CreateInstance(property.PropertyType);
            property.SetValue(obj, instanceProperty, null);
        }

        public static void SetProperty(this PropertyInfo property, object obj, string value)
        {
            EnvironmentVariableAttribute variableAttribute = property.GetCustomAttribute<EnvironmentVariableAttribute>();

            if (variableAttribute.Required && value == null)
                throw new ValidationException($"A propriedade {property.Name} requer um valor. A variável de ambiente relacionada [{variableAttribute.Name}] não retornou um valor válido");

            property.SetValue(obj, property.ConvertValueVariable(value), null);
        }

        public static string GetVariableName(this PropertyInfo property)
        {
            EnvironmentVariableAttribute environmentVariableProperty = property.GetCustomAttribute<EnvironmentVariableAttribute>();

            if (string.IsNullOrEmpty(environmentVariableProperty.Name))
                throw new ValidationException($"A propriedade {property.Name} requer um nome de variável no atributto {nameof(EnvironmentVariableAttribute)}");

            return environmentVariableProperty.Name;
        }
    }
}
