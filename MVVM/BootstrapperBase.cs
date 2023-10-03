using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace MVVM
{
    /// <summary>
    /// The base class of a bootstrapper to initialize dependencies for the program startup.
    /// </summary>
    /// <seealso cref="IDisposable"/>
    public abstract class BootstrapperBase : IDisposable
    {
        private bool _disposedValue;
        private readonly ServiceCollection _serviceCollection;
        private ServiceProvider? _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootstrapperBase"/> class.
        /// </summary>
        protected BootstrapperBase()
        {
            _serviceCollection = new ServiceCollection();

            if (Application.Current != null)
            {
                Application.Current.Startup += OnApplicationStartup;
                Application.Current.Exit += OnApplicationExit;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _serviceCollection.Clear();
                    _serviceProvider?.Dispose();

                    if (Application.Current != null)
                    {
                        Application.Current.Startup -= OnApplicationStartup;
                        Application.Current.Exit -= OnApplicationExit;
                    }
                }

                _disposedValue = true;
            }
        }

        /// <summary>
        /// Configures the service dependencies.
        /// </summary>
        /// <param name="services">The service collection.</param>
        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }

        /// <summary>
        /// Called when the application is starting.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        protected virtual void OnStartup(IServiceProvider serviceProvider)
        {
        }

        /// <summary>
        /// Called when the application shuts down.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        protected virtual void OnExit(IServiceProvider? serviceProvider)
        {
        }

        /// <summary>
        /// Called when the application is starting.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="StartupEventArgs"/> instance containing the event data.</param>
        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            ConfigureServices(_serviceCollection);
            _serviceProvider = _serviceCollection.BuildServiceProvider();
            OnStartup(_serviceProvider);
        }

        /// <summary>
        /// Called when the application is shutting down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ExitEventArgs"/> instance containing the event data.</param>
        private void OnApplicationExit(object sender, ExitEventArgs e)
        {
            OnExit(_serviceProvider);
            _serviceCollection.Clear();
        }
    }
}
