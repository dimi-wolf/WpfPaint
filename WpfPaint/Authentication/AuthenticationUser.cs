using System.ComponentModel.DataAnnotations;
using System.Security;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfPaint.Resources;

namespace WpfPaint.Authentication
{
    /// <summary>
    /// Represents the authentication user.
    /// </summary>
    /// <seealso cref="CommunityToolkit.Mvvm.ComponentModel.ObservableValidator" />
    public class AuthenticationUser : ObservableValidator
    {
        private string _username = string.Empty;

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Strings), ErrorMessageResourceName = nameof(Strings.ErrorNameRequired))]
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        /// <summary>
        /// Gets or sets the secure password.
        /// </summary>
        /// <value>
        /// The secure password.
        /// </value>
        public SecureString? SecurePassword { get; set; }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        public void Validate() => ValidateAllProperties();
    }
}
