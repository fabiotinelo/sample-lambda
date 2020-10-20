using Iteris.Sample.AppSetting.Interface;
using Iteris.Settings;
using System.Diagnostics.CodeAnalysis;


namespace Iteris.Sample.AppSetting
{
    [ExcludeFromCodeCoverage]
    public class BaseSetting : IBaseSetting
    {
        [EnvironmentVariableAttribute("ASPNETCORE_ENVIRONMENT", true)]
        public AppEnvironment Environment { get; set; }

        public Database Database { get; set; }

        public ExternalServices ExternalServiceSetting { get; set; }

        [EnvironmentVariableAttribute("SYSTEM_NAME", true)]
        public string SystemName { get; set; }
    }
}
