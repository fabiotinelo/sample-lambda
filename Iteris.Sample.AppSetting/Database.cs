using Iteris.Sample.AppSetting.Interface;
using Iteris.Settings;
using System.Diagnostics.CodeAnalysis;

namespace Iteris.Sample.AppSetting
{
    [ExcludeFromCodeCoverage]
    public class Database: IDatabase
    {
        [EnvironmentVariableAttribute("DATABASE_CONNECTIONSTRING", true)]
        public string ConnectionString { get; set; }

    }
}
