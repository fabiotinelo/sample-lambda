using Iteris.Sample.AppSetting.Interface;
using Iteris.Settings;
using System.Diagnostics.CodeAnalysis;


namespace Iteris.Sample.AppSetting
{
    [ExcludeFromCodeCoverage]
    public class ExternalServices : IExternalServices
    {
        [EnvironmentVariableAttribute("URL_SERVICE_1", true)]
        public string URLService1 { get; set; }

        [EnvironmentVariableAttribute("URL_SERVICE_2", true)]
        public string URLService2 { get; set; }
    }
}
