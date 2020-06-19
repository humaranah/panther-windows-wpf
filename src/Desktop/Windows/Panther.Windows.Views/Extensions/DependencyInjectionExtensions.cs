using Microsoft.Extensions.DependencyInjection;
using Panther.Windows.Views;
using Panther.Windows.Views.ViewModels;
using System;
using System.Windows.Threading;

namespace Panther.NetCore.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddViews(this IServiceCollection services) =>
            services
                .AddPlaybackTimer()
                .AddWindows();

        private static IServiceCollection AddWindows(this IServiceCollection services) =>
            services
                .AddSingleton<MainWindow>()
                .AddSingleton<MiniPlayerWindow>()
                .AddSingleton<IPlayerViewModel, PlayerViewModel>();

        private static IServiceCollection AddPlaybackTimer(this IServiceCollection services) =>
            services
                .AddSingleton<DispatcherTimer>(new DispatcherTimer(DispatcherPriority.DataBind)
                {
                    Interval = TimeSpan.FromMilliseconds(1000),
                });
    }
}
