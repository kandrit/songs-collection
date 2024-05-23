using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.IO;
using System.Windows;
using SongsCollection.Core;

namespace SongsCollection.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IConfiguration Configuration { get; private set; }

    public App()
    {
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("AppSettings.json", false, true);

        this.Configuration = builder.Build();

        var configurationModule = new ConfigurationModule(this.Configuration);
        var containerBuilder = new ContainerBuilder();
        containerBuilder.RegisterModule(configurationModule);
        var container = containerBuilder.Build();

        var mainWindow = container.Resolve<MainWindow>();
        mainWindow.Show();
    }
}
