using Microsoft.Extensions.DependencyInjection;
using Panther.NetCore.Views;

namespace Panther.NetCore.Extensions
{
    public static class ViewsExtensions
    {
        public static IServiceCollection AddViews(this IServiceCollection services)
        {
            return services
                .AddSingleton<MainWindow>();
        }
    }
}
