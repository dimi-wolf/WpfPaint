using WpfPaint.MVVM;

namespace WpfPaint.ViewModels
{
    /// <summary>
    /// The view model for the header.
    /// </summary>
    /// <seealso cref="WpfPaint.MVVM.ViewModelBase" />
    internal class HeaderViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderViewModel"/> class.
        /// </summary>
        public HeaderViewModel()
        {
            Title = "WPF Paint (MVVM ShowCase)";
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get => GetValue<string>() ?? string.Empty;
            set => SetValue(value);
        }
    }
}
