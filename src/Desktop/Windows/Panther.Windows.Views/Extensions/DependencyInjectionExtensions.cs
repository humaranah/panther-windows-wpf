using Microsoft.Extensions.DependencyInjection;
using Panther.Windows.Views;
using System;

namespace Panther.NetCore.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddViews(this IServiceCollection services)
        {
            return services
                .AddSingleton<MainWindow>();
        }
    }
}
