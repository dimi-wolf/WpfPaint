using System.Globalization;
using System.Resources;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfPaint.Localization
{
    /// <summary>
    /// The LocalizationSource is used by the <see cref="LocalizationExtension"/> to bind to the ResourceManager.
    /// </summary>
    /// <seealso cref="CommunityToolkit.Mvvm.ComponentModel.ObservableObject" />
    public sealed class LocalizationSource : ObservableObject
    {
        private readonly ResourceManager _resourceManager = Resources.Strings.ResourceManager;
        private CultureInfo? _currentCulture;

        /// <summary>
        /// Prevents a default instance of the <see cref="LocalizationSource"/> class from being created.
        /// </summary>
        private LocalizationSource() { }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static LocalizationSource Instance { get; } = new();

        /// <summary>
        /// Gets the <see cref="System.Nullable{System.String}"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="System.Nullable{System.String}"/>.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string? this[string key] => _resourceManager.GetString(key, _currentCulture);

        /// <summary>
        /// Gets or sets the current culture.
        /// </summary>
        /// <value>
        /// The current culture.
        /// </value>
        public CultureInfo? CurrentCulture
        {
            get => _currentCulture;
            set
            {
                if (SetProperty(ref _currentCulture, value) && value != null)
                {
                    SetCulture(value);
                    OnPropertyChanged(string.Empty);
                }
            }
        }

        /// <summary>
        /// Sets the culture.
        /// </summary>
        /// <param name="culture">The culture.</param>
        public static void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }
}
