namespace Iteris.Settings.Interface
{
    public interface ISecretManager
    {
        string Prefix { get; }

        string PrefixDelimiter { get; }

        string Region { get; }

        string SecretName { get; }

        string VersionStageDefault { get; }
    }
}
