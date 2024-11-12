using System.Security.Cryptography;
using System.Text;

namespace irede.shared.Extensions
{
    public static class StringExtensions
    {
        const int keySize = 64;
        const int iterations = 350000;

        public static string ToFormat(this string mensagem, params object[] parametros)
        {
            return string.Format(mensagem, parametros);
        }

        public static string HashPasword(this string password, out byte[] salt)
        {

            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }

        
        public static bool VerifyPassword(this string password, string hash, byte[] salt)
        {
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

            return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
        }

    }
}
