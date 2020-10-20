namespace Iteris.Settings.Interface
{
    public interface IEnvironmentVariableService
    {
        object SetProperties(object obj);

        object SetProperties(object obj, ISecurityData securityData);
    }
}
