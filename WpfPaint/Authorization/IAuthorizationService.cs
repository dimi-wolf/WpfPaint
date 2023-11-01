namespace WpfPaint.Authorization
{
    /// <summary>
    /// The service to authorize a user.
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Determines whether the specified role has permission.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>
        ///   <c>true</c> if the specified role has permission; otherwise, <c>false</c>.
        /// </returns>
        bool HasPermission(string role);
    }
}
