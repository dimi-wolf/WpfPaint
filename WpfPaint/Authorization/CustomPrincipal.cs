using System;
using System.Linq;
using System.Security.Principal;

namespace WpfPaint.Authorization
{
    /// <summary>
    /// Defines a custom principal.
    /// </summary>
    /// <seealso cref="System.Security.Principal.IPrincipal" />
    public class CustomPrincipal : IPrincipal
    {
        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        public CustomIdentity Identity { get; set; } = new AnonymousIdentity();

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
