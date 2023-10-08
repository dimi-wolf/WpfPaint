using System.IO;
using System.Reflection;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WpfPaint.Model;
using WpfPaint.ViewModels;

namespace WpfPaint
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly string? _assemblyLocation = Assembly.GetEntryAssembly()?.Location;
        private static readonly string _assemblyDir = Path.GetDirectoryName(_assemblyLocation) ?? string.Empty;

        private static readonly IHost _host = Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(c => { c.SetBasePath(_assemblyDir); })
            .ConfigureServices((context, services) =>
            {
                // main window
                services.AddSingleton<MainWindow>();

                // model
                services.AddSingleton<ObjectsStore>();

                // view models
                services.AddTransient<MainViewModel>();
                services.AddTransient<HeaderViewModel>();
                services.AddTransient<ObjectsViewModel>();
                services.AddTransient<PropertiesViewModel>();
                services.AddTransient<FooterViewModel>();
                services.AddTransient<BoardViewModel>();

                // drawing primitives
                services.AddTransient<Rectangle>();
                services.AddTransient<Circle>();
                services.AddTransient<PolyLine>();

            }).Build();

        public static T? GetService<T>()
            where T : class
        {
            return _host.Services.GetService<T>();
        }

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync().ConfigureAwait(true);
            GetService<MainWindow>()?.Show();
        }

        private async void Application_Exit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync().ConfigureAwait(true);
            _host.Dispose();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
        }
    }
}
