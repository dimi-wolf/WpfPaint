namespace WpfPaint.Messages
{
    /// <summary>
    /// A message which is sent while login or logout.
    /// </summary>
    public sealed class AuthenticationMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationMessage"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        private AuthenticationMessage(string username)
        {
            Username = username;
        }

        /// <summary>
        /// Creates a message to login the specified username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>
        /// An instance of the <see cref="AuthenticationMessage"/>.
        /// </returns>
        public static AuthenticationMessage Login(string username) => new(username);

        /// <summary>
        /// Creates a message to logout the current user.
        /// </summary>
        /// <returns>
        /// An instance of the <see cref="AuthenticationMessage"/>.
        /// </returns>
        public static AuthenticationMessage Logout() => new(string.Empty);

        /// <summary>
        /// Gets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; } = string.Empty;

        /// <summary>
        /// Gets a value indicating whether the user is logged in.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the user is logged in; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoggedIn => !string.IsNullOrWhiteSpace(Username);
    }
}
