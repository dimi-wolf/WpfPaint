using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using WpfPaint.Messages;

namespace WpfPaint.ViewModels
{
    /// <summary>
    /// The view model for the footer.
    /// </summary>
    /// <seealso cref="CommunityToolkit.Mvvm.ComponentModel.ObservableRecipient" />
    /// <seealso cref="CommunityToolkit.Mvvm.Messaging.IRecipient&lt;WpfPaint.Messages.LanguageChangedMessage&gt;" />
    public class FooterViewModel : ObservableRecipient, IRecipient<LanguageChangedMessage>
    {
        private readonly Func<string> _getStatusText;

        /// <summary>
        /// Initializes a new instance of the <see cref="FooterViewModel"/> class.
        /// </summary>
        public FooterViewModel()
        {
            _getStatusText = () => Resources.Strings.Ready;
            IsActive = true;
        }

        /// <summary>
        /// Gets the status text.
        /// </summary>
        /// <value>
        /// The status text.
        /// </value>
        public string StatusText => _getStatusText();

        /// <summary>
        /// Receives a given <typeparamref name="TMessage" /> message instance.
        /// </summary>
        /// <param name="message">The message being received.</param>
        public void Receive(LanguageChangedMessage message)
        {
            OnPropertyChanged(nameof(StatusText));
        }
    }
}
