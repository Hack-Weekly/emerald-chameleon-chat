using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using System.Security.Cryptography;


namespace EmeraldChameleonChat.Services.Shared
{
    public class Util
    {

        private const int _iterCount = 10000;
        private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

        public static string GetFile(string filePath, IWebHostEnvironment env)
        {
            var builder = new BodyBuilder();

            var fp = env.WebRootPath + Path.DirectorySeparatorChar.ToString() + filePath;
            // var fp = Path.Combine(_env.ApplicationName + "\\EmailTemplates", "Registration.html");
            using (var sourceReader = File.OpenText(fp))
                builder.HtmlBody = sourceReader.ReadToEnd();
            return builder.HtmlBody;
        }



        public static string CreateRandomPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                password = GenerateRandomPassword();

            return Convert.ToBase64String(HashPassword(password, RandomNumberGenerator.Create()));
        }
        public static string GenerateRandomPassword(int length = 8)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            return new string(chars);
        }

        public static bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword == null)
            {
                throw new ArgumentNullException(nameof(hashedPassword));
            }

            if (providedPassword == null)
            {
                throw new ArgumentNullException(nameof(providedPassword));
            }

            byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);

            // read the format marker from the hashed password
            if (decodedHashedPassword.Length == 0)
            {
                return false;
            }

            int embeddedIterCount;

            if (VerifyHashedPassword(decodedHashedPassword, providedPassword, out embeddedIterCount))
            {
                // If this hasher was configured with a higher iteration count, change the entry now.
                return true;
            }

            return false;
        }
        private static bool VerifyHashedPassword(byte[] hashedPassword, string password, out int iterCount)
        {
            iterCount = default;

            // Read header information
            KeyDerivationPrf prf = (KeyDerivationPrf)ReadNetworkByteOrder(hashedPassword, 1);
            iterCount = (int)ReadNetworkByteOrder(hashedPassword, 5);
            int saltLength = (int)ReadNetworkByteOrder(hashedPassword, 9);

            // Read the salt: must be >= 128 bits
            if (saltLength < 128 / 8)
            {
                return false;
            }

            byte[] salt = new byte[saltLength];
            Buffer.BlockCopy(hashedPassword, 13, salt, 0, salt.Length);

            // Read the subkey (the rest of the payload): must be >= 128 bits
            int subkeyLength = hashedPassword.Length - 13 - salt.Length;

            if (subkeyLength < 128 / 8)
            {
                return false;
            }

            byte[] expectedSubkey = new byte[subkeyLength];
            Buffer.BlockCopy(hashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            // Hash the incoming password and verify it
            byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, subkeyLength);

            return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
        }
        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return (uint)buffer[offset + 0] << 24
                | (uint)buffer[offset + 1] << 16
                | (uint)buffer[offset + 2] << 8
                | buffer[offset + 3];
        }
        public static byte[] HashPassword(string password, RandomNumberGenerator rng)
        {
            return HashPassword(password, rng,
                prf: KeyDerivationPrf.HMACSHA256,
                iterCount: _iterCount,
                saltSize: 128 / 8,
                numBytesRequested: 256 / 8);
        }
        private static byte[] HashPassword(string password, RandomNumberGenerator rng, KeyDerivationPrf prf, int iterCount, int saltSize, int numBytesRequested)
        {
            // Produce a version 3 (see comment above) text hash.
            byte[] salt = new byte[saltSize];
            rng.GetBytes(salt);
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, numBytesRequested);
            var outputBytes = new byte[13 + salt.Length + subkey.Length];
            outputBytes[0] = 0x01; // format marker

            WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
            WriteNetworkByteOrder(outputBytes, 5, (uint)iterCount);
            WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);

            Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
            Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);

            return outputBytes;
        }
        private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset + 0] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)(value >> 0);
        }

    }
}
