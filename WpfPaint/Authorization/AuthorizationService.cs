using System.Threading;

namespace WpfPaint.Authorization
{
    /// <summary>
    /// The service to authorize a user.
    /// </summary>
    /// <seealso cref="WpfPaint.Authorization.IAuthorizationService" />
    public class AuthorizationService : IAuthorizationService
    {
        /// <summary>
        /// Determines whether the specified role has permission.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>
        ///   <c>true</c> if the specified role has permission; otherwise, <c>false</c>.
        /// </returns>
        public bool HasPermission(string role)
        {
            if (Thread.CurrentPrincipal is CustomPrincipal principal)
            {
                return principal.Identity.IsAuthenticated && principal.IsInRole(role);
            }

            return false;
        }
    }
}
