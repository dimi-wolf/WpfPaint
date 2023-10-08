using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using WpfPaint.Messages;

namespace WpfPaint.Model
{
    /// <summary>
    /// Represents a language.
    /// </summary>
    /// <seealso cref="CommunityToolkit.Mvvm.ComponentModel.ObservableRecipient" />
    /// <seealso cref="CommunityToolkit.Mvvm.Messaging.IRecipient&lt;WpfPaint.Messages.LanguageChangedMessage&gt;" />
    public partial class Language : ObservableRecipient, IRecipient<LanguageChangedMessage>
    {
        private readonly string _code;
        private readonly Func<string> _getName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="name">The name.</param>
        public Language(string code, Func<string> name)
        {
            _code = code;
            _getName = name;
            IsActive = true;
        }

        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code => _code;

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => _getName();

        /// <summary>
        /// Receives a given <typeparamref name="TMessage" /> message instance.
        /// </summary>
        /// <param name="message">The message being received.</param>
        public void Receive(LanguageChangedMessage message)
        {
            OnPropertyChanged(nameof(Name));
        }
    }
}
