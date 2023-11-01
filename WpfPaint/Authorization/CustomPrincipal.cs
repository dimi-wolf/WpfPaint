using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;

namespace WpfPaint.Authorization
{
    /// <summary>
    /// Defines a custom principal.
    /// </summary>
    /// <seealso cref="System.Security.Principal.IPrincipal" />
    public class CustomPrincipal : IPrincipal
    {
        /// <summary>
        /// Gets the current principal object.
        /// </summary>
        /// <value>
        /// The current principal object.
        /// </value>
        /// <exception cref="System.InvalidOperationException">The application's default thread principal must be set to a CustomPrincipal object on startup.</exception>
        public static CustomPrincipal Current => Thread.CurrentPrincipal as CustomPrincipal
            ?? throw new InvalidOperationException("The application's default thread principal must be set to a CustomPrincipal object on startup.");

        /// <summary>
        /// Changes the current identity to the given identity.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <exception cref="System.ArgumentNullException">identity</exception>
        public static void SignIn(CustomIdentity identity)
        {
            ArgumentNullException.ThrowIfNull(identity, nameof(identity));
            Current.Identity = identity;
        }

        /// <summary>
        /// Changes the current identity to anonymous.
        /// </summary>
        public static void SignOut()
        {
            //change Identity to Anonymous
            Current.Identity = new AnonymousIdentity();
        }

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        public CustomIdentity Identity { get; private set; } = new AnonymousIdentity();

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        IIdentity IPrincipal.Identity => Identity;

        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <param name="role">The name of the role for which to check membership.</param>
        /// <returns>
        ///   <see langword="true" /> if the current principal is a member of the specified role; otherwise, <see langword="false" />.
        /// </returns>
        public bool IsInRole(string role) => Identity.Roles.Contains(role);
    }
}
