using Iteris.Sample.AppSetting.Interface;
using System;
using System.Text;

namespace Iteris.Sample.Service
{
    public class SampleService : ISampleService
    {

        private readonly IBaseSetting baseSetting;

        public SampleService(IBaseSetting baseSetting)
        {
            this.baseSetting = baseSetting;
        }

        public string GetSample()
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine($"ConnectionString:{baseSetting.Database.ConnectionString}{Environment.NewLine}");
            str.AppendLine($"URLService1:{baseSetting.ExternalServiceSetting.URLService1}{Environment.NewLine}");
            str.AppendLine($"URLService1:{baseSetting.ExternalServiceSetting.URLService2}{Environment.NewLine}");
            str.AppendLine($"SystemName:{baseSetting.SystemName}{Environment.NewLine}");
            str.AppendLine($"Envrioment:{baseSetting.Environment.ToString()}");

            return str.ToString();
        }

     


    }
}
