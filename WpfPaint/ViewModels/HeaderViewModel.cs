using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using MVVM.ComponentModel;
using WpfPaint.Model;

namespace WpfPaint.ViewModels
{
    /// <summary>
    /// The view model for the header.
    /// </summary>
    /// <seealso cref="WpfPaint.MVVM.ViewModelBase" />
    public class HeaderViewModel : ViewModelBase
    {
        private string _title = string.Empty;
        private Language? _selectedLanguage;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderViewModel"/> class.
        /// </summary>
        public HeaderViewModel()
        {
            Title = "WPF Paint (MVVM ShowCase)";
            Languages.Add(new("DE", Resources.Strings.German));
            Languages.Add(new("EN", Resources.Strings.English));
            SelectedLanguage = Languages.First();
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get => _title;
            set => SetValue(ref _title, value);
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <value>
        /// The languages.
        /// </value>
        public ObservableCollection<Language> Languages { get; } = new();

        /// <summary>
        /// Gets or sets the selected language.
        /// </summary>
        /// <value>
        /// The selected language.
        /// </value>
        public Language? SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (SetValue(ref _selectedLanguage, value))
                {
                    if (value != null)
                    {
                        Localization.LocalizationSource.Instance.CurrentCulture = new CultureInfo(value.Code);
                    }
                }
            }
        }
    }
}
