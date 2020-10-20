using System.Diagnostics.CodeAnalysis;
using Iteris.Settings.Interface;

namespace Iteris.Settings
{
    [ExcludeFromCodeCoverage]
    public class SecretManager : ISecretManager
    {

        [EnvironmentVariableAttribute("SecretManager_Prefix", false)]
        public string Prefix { get; set; }

        [EnvironmentVariableAttribute("SecretManager_PrefixDelimiter", false)]
        public string PrefixDelimiter { get; set; }

        [EnvironmentVariableAttribute("SecretManager_Region", false)]
        public string Region { get; set; }

        [EnvironmentVariableAttribute("SecretManager_SecretName", false)]
        public string SecretName { get; set; }

        [EnvironmentVariableAttribute("SecretManager_VersionStageDefault", false)]
        public string VersionStageDefault { get; set; }

    }

}
