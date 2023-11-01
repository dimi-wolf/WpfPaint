using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using WpfPaint.Authorization;
using WpfPaint.Messages;
using WpfPaint.Model;

namespace WpfPaint.ViewModels
{
    /// <summary>
    /// The view model for the header.
    /// </summary>
    /// <seealso cref="CommunityToolkit.Mvvm.ComponentModel.ObservableRecipient" />
    public partial class HeaderViewModel : ObservableRecipient, IRecipient<AuthenticationMessage>
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
            IsActive = true;
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

        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Is used as Binding!")]
        public string CurrentUser => GetUsername();

        /// <summary>
        /// Gets a value indicating whether this instance is authenticated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Is used as Binding!")]
        public bool IsAuthenticated => Thread.CurrentPrincipal?.Identity?.IsAuthenticated == true;

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        [RelayCommand]
        public void Logout()
        {
            Messenger.Send(AuthenticationMessage.Logout());
        }

        /// <summary>
        /// Receives a given <typeparamref name="TMessage" /> message instance.
        /// </summary>
        /// <param name="message">The message being received.</param>
        public void Receive(AuthenticationMessage message)
        {
            OnPropertyChanged(string.Empty);
        }

        private void OnSelectedLanguageChanging(Language? value)
        {
            _selectedLanguage = value;

            if (value != null)
            {
                Localization.LocalizationSource.Instance.CurrentCulture = new CultureInfo(value.Code);
                Messenger.Send(new LanguageChangedMessage());
                OnPropertyChanged(string.Empty);
            }
        }

        private static string GetUsername()
        {
            string result = Resources.Strings.PleaseLogin;

            if (CustomPrincipal.Current.Identity.IsAuthenticated)
            {
                result = CustomPrincipal.Current.Identity.Name;
            }

            return result;
        }
    }
}
