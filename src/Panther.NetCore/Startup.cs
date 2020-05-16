using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Panther.Core.DependencyInjection.Extensions;
using Panther.NetCore.Extensions;
using System;
using System.IO;

namespace Panther.NetCore
{
    public static class Startup
    {
        public static void InitConfiguration(IConfigurationBuilder builder)
        {
            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);
        }

        public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            var configuration = context.Configuration;

            services
                .AddDataServices()
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
