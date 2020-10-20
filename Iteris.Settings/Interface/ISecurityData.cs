namespace Iteris.Settings.Interface
{
    public interface ISecurityData
    {
        string GetSecret(string key);

        bool ContainsPrefix(string environmentVariable);
    }
}
