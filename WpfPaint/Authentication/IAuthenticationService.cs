using System;
using System.Security;

namespace WpfPaint.Authentication
{
    /// <summary>
    /// The service to authenticate a user.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// An instance of the <see cref="User"/> or <c>null</c>.
        /// </returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        User AuthenticateUser(string username, SecureString password);
    }
}
