namespace Iteris.Sample.AppSetting.Interface
{

    public enum AppEnvironment
    {
        LocalDevelopment,
        Development,
        Homolog,
        Production
    }


    public interface IBaseSetting
    {
        AppEnvironment Environment { get; }

        Database Database { get; }

        ExternalServices ExternalServiceSetting { get; }

        string SystemName { get; }
    }
}
