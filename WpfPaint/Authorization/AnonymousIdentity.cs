using System;

namespace WpfPaint.Authorization
{
    /// <summary>
    /// Represents the anonymous identity.
    /// </summary>
    /// <seealso cref="WpfPaint.Authorization.CustomIdentity" />
    public class AnonymousIdentity : CustomIdentity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousIdentity"/> class.
        /// </summary>
        public AnonymousIdentity()
            : base(string.Empty, string.Empty, Array.Empty<string>())
        {
        }
    }
}
