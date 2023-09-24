using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using WpfPaint.ViewModels;

namespace WpfPaint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            mainContent.Content = serviceProvider.GetService<MainViewModel>();
        }
    }
}
