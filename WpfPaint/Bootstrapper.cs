using System;
using Microsoft.Extensions.DependencyInjection;
using WpfPaint.Messaging;
using WpfPaint.Model;
using WpfPaint.MVVM;
using WpfPaint.ViewModels;

namespace WpfPaint
{
    /// <summary>
    /// The bootstrapper initializes the application and hosts the main window.
    /// </summary>
    /// <seealso cref="WpfPaint.MVVM.BootstrapperBase" />
    public class Bootstrapper : BootstrapperBase
    {
        /// <summary>
        /// Configures the service dependencies.
        /// </summary>
        /// <param name="services">The service collection.</param>
        protected override void ConfigureServices(IServiceCollection services)
        {
            // main window
            services.AddSingleton<MainWindow>();

            // services
            services.AddSingleton<IEventAggregator, EventAggregator>();

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
        }

        /// <summary>
        /// Called when the application is starting.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        protected override void OnStartup(IServiceProvider serviceProvider)
        {
            MainWindow? mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }
    }
}
