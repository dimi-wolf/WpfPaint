using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using WpfPaint.Messages;
using WpfPaint.Model;

namespace WpfPaint.ViewModels
{
    /// <summary>
    /// The view model for the header.
    /// </summary>
    /// <seealso cref="CommunityToolkit.Mvvm.ComponentModel.ObservableRecipient" />
    public partial class HeaderViewModel : ObservableRecipient
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [ObservableProperty]
        private string _title = string.Empty;
        private Language? _selectedLanguage;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderViewModel"/> class.
        /// </summary>
        public HeaderViewModel()
        {
            Title = "WPF Paint (MVVM ShowCase)";
            Languages.Add(new("DE", () => Resources.Strings.German));
            Languages.Add(new("EN", () => Resources.Strings.English));
            SelectedLanguage = Languages.First();
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
            set => SetProperty(_selectedLanguage, value, callback: OnSelectedLanguageChanging);
        }

        private void OnSelectedLanguageChanging(Language? value)
        {
            _selectedLanguage = value;

            if (value != null)
            {
                Localization.LocalizationSource.Instance.CurrentCulture = new CultureInfo(value.Code);
                Messenger.Send(new LanguageChangedMessage());
            }
        }
    }
}
