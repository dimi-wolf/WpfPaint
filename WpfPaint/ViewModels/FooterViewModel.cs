using MVVM.ComponentModel;

namespace WpfPaint.ViewModels
{
    /// <summary>
    /// The view model for the footer.
    /// </summary>
    /// <seealso cref="WpfPaint.MVVM.ViewModelBase" />
    public class FooterViewModel : ViewModelBase
    {
        private string _statusText = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="FooterViewModel"/> class.
        /// </summary>
        public FooterViewModel()
        {
            StatusText = Resources.Strings.Ready;
        }

        /// <summary>
        /// Gets or sets the status text.
        /// </summary>
        /// <value>
        /// The status text.
        /// </value>
        public string StatusText
        {
            get => _statusText;
            set => SetValue(ref _statusText, value);
        }
    }
}
