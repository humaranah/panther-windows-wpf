using Microsoft.Extensions.DependencyInjection;
using Panther.Windows.Views;
using Panther.Windows.Views.ViewModels;

namespace Panther.NetCore.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddViews(this IServiceCollection services) =>
            services
                .AddWindows();

        private static IServiceCollection AddWindows(this IServiceCollection services) =>
            services
                .AddSingleton<MainWindow>()
                .AddSingleton<MiniPlayerWindow>()
                .AddSingleton<MiniPlayerViewModel>();
    }
}
