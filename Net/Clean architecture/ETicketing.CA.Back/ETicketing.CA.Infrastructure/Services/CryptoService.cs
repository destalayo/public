using ETicketing.CA.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ETicketing.CA.Infrastructure.Services
{
    public class CryptoService: ICryptoService
    {
        private const int SaltSize = 16; 
        private const int KeySize = 32; 
        private const int Iterations = 100_000;
        public string CreateHash(string text)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(
                text,
                salt,
                Iterations,
                HashAlgorithmName.SHA256);

            byte[] key = pbkdf2.GetBytes(KeySize);

            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
        }

        public bool VerifyHash(string text, string hash)
        {
            var parts = hash.Split('.');
            if (parts.Length != 2)
                return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] key = Convert.FromBase64String(parts[1]);

            using var pbkdf2 = new Rfc2898DeriveBytes(
                text,
                salt,
                100_000,
                HashAlgorithmName.SHA256);

            byte[] keyToCheck = pbkdf2.GetBytes(key.Length);

            return CryptographicOperations.FixedTimeEquals(keyToCheck, key);
        }

    }
}

