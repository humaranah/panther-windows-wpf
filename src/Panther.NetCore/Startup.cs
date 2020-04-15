using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Panther.Core.DependencyInjection.Extensions;
using Panther.NetCore.Extensions;
using System;
using System.IO;
using System.Linq;

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
                .AddLibraryServices()
                .AddPlayerServices()
                .AddPlayerQueueServices(configuration)
                .AddViews();
        }

        public static void Configure(IServiceProvider serviceProvider)
        {
            serviceProvider.ConfigureLibrary();
        }
    }
}
