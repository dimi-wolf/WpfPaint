using System.ComponentModel.Design;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using WpfPaint.Authorization;
using WpfPaint.Messages;

namespace WpfPaint.ViewModels
{
    /// <summary>
    /// The view model for the properties page.
    /// </summary>
    /// <seealso cref="CommunityToolkit.Mvvm.ComponentModel.ObservableRecipient" />
    /// <seealso cref="CommunityToolkit.Mvvm.Messaging.IRecipient&lt;WpfPaint.Messages.SelectedObjectChangedMessage&gt;" />
    public partial class PropertiesViewModel : ObservableRecipient, IRecipient<SelectedObjectChangedMessage>
    {
        private readonly IAuthorizationService _authorizationService;

        /// <summary>
        /// Gets or sets the current object.
        /// </summary>
        [ObservableProperty]
        private object? _currentObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertiesViewModel"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor will produce an instance that will use the <see cref="CommunityToolkit.Mvvm.Messaging.WeakReferenceMessenger.Default" /> instance
        /// to perform requested operations. It will also be available locally through the <see cref="CommunityToolkit.Mvvm.ComponentModel.ObservableRecipient.Messenger" /> property.
        /// </remarks>
        public PropertiesViewModel(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            IsActive = true;
        }

        /// <summary>
        /// Receives a given <typeparamref name="TMessage" /> message instance.
        /// </summary>
        /// <param name="message">The message being received.</param>
        public void Receive(SelectedObjectChangedMessage message)
        {
            CurrentObject = message?.SelectedObject;
        }

        /// <summary>
        /// Gets a value indicating whether this instance has edit permission.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has edit permission; otherwise, <c>false</c>.
        /// </value>
        public bool HasEditPermission => _authorizationService.HasPermission(Roles.Designers);
    }
}
