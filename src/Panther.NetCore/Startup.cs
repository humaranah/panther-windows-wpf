using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Panther.Core.DependencyInjection.Extensions;
using Panther.NetCore.Extensions;
using System.IO;

namespace Panther.NetCore
{
    public static class Startup
    {
        public static IConfiguration InitConfiguration()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);

            return configBuilder.Build();
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddPlayerServices()
                .AddPlayerQueueServices(configuration)
                .AddViews();
        }
    }
}
