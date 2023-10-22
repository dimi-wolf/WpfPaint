using System.Collections.Generic;
using System.Security.Principal;

namespace WpfPaint.Authorization
{
    /// <summary>
    /// Represents a custom identity.
    /// </summary>
    /// <seealso cref="System.Security.Principal.IIdentity" />
    public class CustomIdentity : IIdentity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomIdentity"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="email">The email.</param>
        /// <param name="roles">The roles.</param>
        public CustomIdentity(string name, string email, string[] roles)
        {
            Name = name;
            Email = email;
            Roles = roles;
        }

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public IEnumerable<string> Roles { get; private set; }

        /// <summary>
        /// Gets the type of authentication used.
        /// </summary>
        public string AuthenticationType => "Custom authentication";

        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        public bool IsAuthenticated => !string.IsNullOrEmpty(Name);
    }
}
