using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using WpfPaint.Authentication;
using WpfPaint.Authorization;
using WpfPaint.Messages;
using WpfPaint.Resources;

namespace WpfPaint.ViewModels
{
    /// <summary>
    /// The view model for authentication.
    /// </summary>
    /// <seealso cref="CommunityToolkit.Mvvm.ComponentModel.ObservableRecipient" />
    public partial class AuthenticationViewModel : ObservableRecipient
    {
        private readonly IAuthenticationService _authenticationService;
        private Func<string>? _getErrorMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationViewModel"/> class.
        /// </summary>
        /// <param name="authenticationService">The authentication service.</param>
        public AuthenticationViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public AuthenticationUser User { get; } = new();

        /// <summary>
        /// Gets a value indicating whether this instance has errors.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has errors; otherwise, <c>false</c>.
        /// </value>
        public bool HasErrors => User.HasErrors || _getErrorMessage != null;

        /// <summary>
        /// Gets all errors.
        /// </summary>
        /// <value>
        /// All errors.
        /// </value>
        public IEnumerable<ValidationResult> AllErrors
        {
            get
            {
                var result = new List<ValidationResult>(User.GetErrors());

                if (_getErrorMessage != null)
                {
                    result.Add(new ValidationResult(_getErrorMessage()));
                }

                return result;
            }
        }

        [RelayCommand]
        private void Login(System.Windows.Controls.PasswordBox passwordBox)
        {
            SecureString securePassword = passwordBox.SecurePassword;

            User.Validate();
            if (!User.HasErrors)
            {
                try
                {
                    //Validate credentials through the authentication service
                    User user = _authenticationService.AuthenticateUser(User.Username, securePassword);

                    //Authenticate the user
                    CustomPrincipal.SignIn(new CustomIdentity(user.Username, user.Email, user.Roles.ToArray()));

                    // send the authenticatoin message
                    Messenger.Send(AuthenticationMessage.Login(user.Username));
                }
                catch (UnauthorizedAccessException)
                {
                    _getErrorMessage = () => Strings.LoginFailed;
                }
            }

            OnPropertyChanged(nameof(HasErrors));
            OnPropertyChanged(nameof(AllErrors));
        }
    }
}
