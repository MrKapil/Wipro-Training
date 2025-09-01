using System.Security.Cryptography;
using System.Text;

namespace UserManagement.Services
{
    public class CryptoService
    {
        private readonly byte[] _aesKey;
        private const int Pbkdf2Iterations = 100_000;
        private const int SaltSize = 16; // 128-bit
        private const int KeySize = 32;  // 256-bit

        public CryptoService(byte[] aesKey)
        {
            if (aesKey.Length != KeySize)
                throw new ArgumentException("AES key must be 32 bytes (256-bit).");
            _aesKey = aesKey;
        }

        public static CryptoService FromEnvironmentOrRandom()
        {
            var env = Environment.GetEnvironmentVariable("APP_AES_KEY");
            if (!string.IsNullOrWhiteSpace(env))
            {
                var key = Convert.FromBase64String(env);
                return new CryptoService(key);
            }
            // For demo only: generate a random key; log a warning in Program
            return new CryptoService(RandomNumberGenerator.GetBytes(KeySize));
        }

        public string HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            using var derive = new Rfc2898DeriveBytes(password, salt, Pbkdf2Iterations, HashAlgorithmName.SHA256);
            var hash = derive.GetBytes(KeySize);
            return $"PBKDF2$SHA256${Pbkdf2Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
        }

        public bool VerifyPassword(string password, string stored)
        {
            var parts = stored.Split('$');
            if (parts.Length != 5 || parts[0] != "PBKDF2" || parts[1] != "SHA256")
                return false;

            var iterations = int.Parse(parts[2]);
            var salt = Convert.FromBase64String(parts[3]);
            var expected = Convert.FromBase64String(parts[4]);

            using var derive = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var actual = derive.GetBytes(expected.Length);

            return CryptographicOperations.FixedTimeEquals(actual, expected);
        }

        public string Encrypt(string plaintext)
        {
            using var aes = Aes.Create();
            aes.Key = _aesKey;
            aes.GenerateIV(); // random IV for each encryption
            using var enc = aes.CreateEncryptor(aes.Key, aes.IV);
            var plain = Encoding.UTF8.GetBytes(plaintext);
            var cipher = enc.TransformFinalBlock(plain, 0, plain.Length);

            var output = new byte[aes.IV.Length + cipher.Length];
            Buffer.BlockCopy(aes.IV, 0, output, 0, aes.IV.Length);
            Buffer.BlockCopy(cipher, 0, output, aes.IV.Length, cipher.Length);
            return Convert.ToBase64String(output);
        }

        public string Decrypt(string base64)
        {
            var data = Convert.FromBase64String(base64);
            using var aes = Aes.Create();
            aes.Key = _aesKey;

            var iv = new byte[aes.BlockSize / 8]; // 16 bytes
            Buffer.BlockCopy(data, 0, iv, 0, iv.Length);
            aes.IV = iv;

            var cipher = new byte[data.Length - iv.Length];
            Buffer.BlockCopy(data, iv.Length, cipher, 0, cipher.Length);

            using var dec = aes.CreateDecryptor(aes.Key, aes.IV);
            var plain = dec.TransformFinalBlock(cipher, 0, cipher.Length);
            return Encoding.UTF8.GetString(plain);
        }
    }
}
