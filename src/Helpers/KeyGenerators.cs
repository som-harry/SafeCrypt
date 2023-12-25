﻿using System;
using System.Security.Cryptography;

namespace SafeCrypt.Helpers
{
    public class KeyGenerators
    {

        /// <summary>
        /// Generates an array of random bytes using a cryptographically secure random number generator.
        /// </summary>
        /// <param name="length">The length of the byte array to generate.</param>
        /// <returns>An array of random bytes or the IV key</returns>
        /// <remarks>
        /// The method uses a cryptographically secure random number generator (RNGCryptoServiceProvider) to generate
        /// a byte array with the specified length, providing a high level of randomness suitable for cryptographic use.
        /// </remarks>
        /// <param name="length">The desired length of the generated byte array.</param>
        /// <returns>An array of random bytes with the specified length.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the specified length is less than or equal to zero.
        /// </exception>
        public static byte[] GenerateRandomIVKeyAsBytes(int length)
        {
            byte[] randomBytes = new byte[length];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            return randomBytes;
        }

        public static string GenerateRandomIVKeyAsString()
        {
            byte[] randomBytes = GenerateRandomIVKeyAsBytes(16);            
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        /// <summary>
        /// Generates a valid AES secret key with the specified key size.
        /// </summary>
        /// <param name="keySize">The desired key size (128, 192, or 256 bits).</param>
        /// <returns>A valid AES secret key as a byte array.</returns>
        public static string GenerateAesSecretKey(int keySize)
        {
            if (keySize != 128 && keySize != 192 && keySize != 256)
            {
                throw new ArgumentException("Invalid key size. Supported sizes are 128, 192, or 256 bits.", nameof(keySize));
            }

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = keySize;
                aesAlg.GenerateKey();
                return Convert.ToBase64String(aesAlg.Key);
            }
        }
    }
}
