using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Iteris.Sample.AppSetting;
using Iteris.Sample.AppSetting.Interface;
using Iteris.Sample.Service;
using Iteris.Settings;
using Microsoft.Extensions.DependencyInjection;
using System;


[assembly: LambdaSerializer(typeof(DefaultLambdaJsonSerializer))]
namespace Iteris.Sample.Lambda
{

    public class Function 
    {
        private static IServiceScope serviceScope;

        static Function() => ConfigureServices();

        static void ConfigureServices()
        {
            if (serviceScope != null) return;

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSettings<IBaseSetting, BaseSetting>();
            serviceCollection.AddTransient<ISampleService, SampleService>();
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            serviceScope = serviceProvider.CreateScope();
        }

        public string FunctionHandler(string value, ILambdaContext context)
        {
            return serviceScope.ServiceProvider.GetService<ISampleService>().GetSample();
        }

     
    }
}
