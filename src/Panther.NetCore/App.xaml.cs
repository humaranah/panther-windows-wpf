using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Panther.Windows.Views;
using System.Windows;
using static Panther.NetCore.Startup;

namespace Panther.NetCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App() : base()
        {
            _host = new HostBuilder()
                .ConfigureAppConfiguration(InitConfiguration)
                .ConfigureServices(ConfigureServices)
                .Build();

            Configure(_host.Services);
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            _host.Services.GetService<MiniPlayerWindow>().Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }


            base.OnExit(e);
        }
    }
}
