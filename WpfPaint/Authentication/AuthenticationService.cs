using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using WpfPaint.Authorization;

namespace WpfPaint.Authentication
{
    /// <summary>
    /// The service to authenticate a user.
    /// </summary>
    /// <seealso cref="WpfPaint.Authentication.IAuthenticationService" />
    public class AuthenticationService : IAuthenticationService
    {
        private class InternalUserData
        {
            public InternalUserData(string username, string email, string hashedPassword, string[] roles)
            {
                Username = username;
                Email = email;
                HashedPassword = hashedPassword;
                Roles = roles;
            }

            public string Username { get; }

            public string Email { get; }

            public string HashedPassword { get; }

            public string[] Roles { get; }
        }

        private static readonly List<InternalUserData> _users = new()
        {
            new InternalUserData("Administrator", "admin@company.com", "qQLu9aTCiuu0AaJg4ORcDLtCS7xiwfcr2SIV00Ue0dA=", new[] { Roles.Administrators, Roles.Designers, Roles.Operators }),
            new InternalUserData("Designer", "designer@company.com", "gRB1D2eCSWW/IgurMkpu4HHB7wz5PSZNNtlsdi52JEc=", new[] { Roles.Designers, Roles.Operators }),
            new InternalUserData("Operator", "operator@company.com", "KREBoH/pgOk7kAroXJ64JPnn6T0NdUvnRAuThrYVytc=", new[] { Roles.Operators })
        };

        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// An instance of the <see cref="User" /> or <c>null</c>.
        /// </returns>
        public User AuthenticateUser(string username, SecureString password)
        {
            string hashedPassword = CalculateHash(Decode(password), username);
            InternalUserData? userData = _users.FirstOrDefault(u => u.Username == username && u.HashedPassword == hashedPassword);
            return userData == null
                ? throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.")
                : new User(userData.Username, userData.Email, userData.Roles);
        }

        private static string Decode(SecureString secureString)
        {
            string result = string.Empty;
            IntPtr valuePtr = IntPtr.Zero;

            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                result = Marshal.PtrToStringUni(valuePtr) ?? string.Empty;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }

            return result;
        }

        private static string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);

            // Use the hash algorithm to calculate the hash
            using HashAlgorithm algorithm = SHA256.Create();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);

            // Return the hash as a base64 encoded string to be compared to the stored password
            return Convert.ToBase64String(hash);
        }
    }
}
