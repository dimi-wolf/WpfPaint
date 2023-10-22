using System;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using WpfPaint.Authorization;
using WpfPaint.Messages;

namespace WpfPaint.ViewModels
{
    /// <summary>
    /// The main view model of the application.
    /// </summary>
    public class MainViewModel : ObservableRecipient, IRecipient<AuthenticationMessage>
    {
        private readonly IServiceProvider _serviceProvider;
        private object? _header;
        private object? _objects;
        private object? _mainContent;
        private object? _properties;
        private object? _footer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public MainViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            IsActive = true;
        }

        /// <summary>
        /// Gets the header.
        /// </summary>
        /// <value>
        /// The header.
        /// </value>
        public object? Header
        {
            get => _header;
            private set => SetProperty(ref _header, value);
        }

        /// <summary>
        /// Gets the objects.
        /// </summary>
        /// <value>
        /// The objects.
        /// </value>
        public object? Objects
        {
            get => _objects;
            private set => SetProperty(ref _objects, value);
        }

        /// <summary>
        /// Gets the board.
        /// </summary>
        /// <value>
        /// The board.
        /// </value>
        public object? MainContent
        {
            get => _mainContent;
            private set => SetProperty(ref _mainContent, value);
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        public object? Properties
        {
            get => _properties;
            private set => SetProperty(ref _properties, value);
        }

        /// <summary>
        /// Gets the footer.
        /// </summary>
        /// <value>
        /// The footer.
        /// </value>
        public object? Footer
        {
            get => _footer;
            private set => SetProperty(ref _footer, value);
        }

        public void Receive(AuthenticationMessage message)
        {
            ArgumentNullException.ThrowIfNull(message, nameof(message));

            if (message.IsLoggedIn)
            {
                if (Thread.CurrentPrincipal is CustomPrincipal principal && principal.Identity.IsAuthenticated)
                {
                    Objects = _serviceProvider.GetService<ObjectsViewModel>();
                    MainContent = _serviceProvider.GetService<BoardViewModel>();
                    Properties = _serviceProvider.GetService<PropertiesViewModel>();
                }
            }
            else
            {
                AuthenticationViewModel.Logout();
                MainContent = _serviceProvider.GetService<AuthenticationViewModel>();
                Objects = null;
                Properties = null;
            }
        }

        protected override void OnActivated()
        {
            base.OnActivated();

            Header = _serviceProvider.GetService<HeaderViewModel>();
            MainContent = _serviceProvider.GetService<AuthenticationViewModel>();
            Footer = _serviceProvider.GetService<FooterViewModel>();
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();

            Header = null;
            Objects = null;
            MainContent = null;
            Properties = null;
            Footer = null;
        }
    }
}
