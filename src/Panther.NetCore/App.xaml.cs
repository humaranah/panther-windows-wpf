using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Panther.NetCore.Views;
using System;
using System.Windows;
using static Panther.NetCore.Startup;

namespace Panther.NetCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Configuration = InitConfiguration();
            InitServiceProvider();

            ServiceProvider.GetRequiredService<MainWindow>().Show();
        }

        private void InitServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, Configuration);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
