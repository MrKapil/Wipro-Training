using System.Security.Cryptography;
using System.Text;

namespace SecureWebApp.Utils
{
    public static class HMACHelper
    {
        private static readonly string SecretKey = "Replace_With_Long_Secret_Key";

        public static string ComputeHMAC(string data)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SecretKey));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(hash);
        }
    }
}
