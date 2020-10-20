using Amazon;
using Amazon.SecretsManager;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Iteris.Settings.Interface;

namespace Iteris.Settings
{
    public static class SettingsExtension
    {
        public static IServiceCollection AddSettings<TService, TImplementation>(this IServiceCollection serviceCollection)
            where TService : class
            where TImplementation : class, TService
        {
            Type implementation = typeof(TImplementation);

            if (!serviceCollection.Any(x => x.ServiceType == typeof(IEnvironmentVariableService)))
            {
                serviceCollection.AddSingleton<IEnvironmentVariable, EnvironmentVariable>();
                serviceCollection.AddSingleton<IEnvironmentVariableService, EnvironmentVariableService>();
                serviceCollection.AddSingleton<ISecretManager>(c => c.GetService<IEnvironmentVariableService>().SetProperties(new SecretManager()) as SecretManager);
                serviceCollection.AddSingleton<IAmazonSecretsManager>(c => new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(c.GetService<ISecretManager>().Region)));
                serviceCollection.AddSingleton<ISecurityData, SecurityData>();
            }
            object implementationObject = Activator.CreateInstance<TImplementation>();
            serviceCollection.AddSingleton<TService>(c => c
                                            .GetService<IEnvironmentVariableService>()
                                            .SetProperties(implementationObject, c.GetService<ISecurityData>()) as TImplementation);
           return serviceCollection;
        }



    }
}
