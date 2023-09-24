using WpfPaint.MVVM;

namespace WpfPaint.ViewModels
{
    /// <summary>
    /// The view model for the footer.
    /// </summary>
    /// <seealso cref="WpfPaint.MVVM.ViewModelBase" />
    internal class FooterViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FooterViewModel"/> class.
        /// </summary>
        public FooterViewModel()
        {
            StatusText = "Bereit";
        }

        /// <summary>
        /// Gets or sets the status text.
        /// </summary>
        /// <value>
        /// The status text.
        /// </value>
        public string StatusText
        {
            get => GetValue<string>() ?? string.Empty;
            set => SetValue(value);
        }
    }
}
