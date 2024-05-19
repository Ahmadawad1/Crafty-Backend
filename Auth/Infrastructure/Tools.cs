using System.Security.Cryptography;

namespace Auth.Infrastructure
{
    public class Tools
    {
        public static string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            byte[] salt = new byte[16]; // 16 bytes is a common salt size
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Use a strong key derivation function (KDF) like PBKDF2
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
