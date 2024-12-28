using Microsoft.Extensions.Configuration;
using System;

namespace Configurations
{
    public class ConfigureService
    {
        public IConfiguration Configuration { get; }
        public ConfigureService()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appConfigurations.json")
                .Build();
        }
    }
}
