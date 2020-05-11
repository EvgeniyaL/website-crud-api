using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using TitanGate.Website.Api.Handlers.ServicesContracts;

namespace TitanGate.Website.Api.Handlers.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        private const char Delimiter = '#';

        public string HashWithSaltPassword(string password)
        {
            byte[] saltBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            string hashed = GetHash(password, saltBytes);

            return $"{Convert.ToBase64String(saltBytes)}{Delimiter}{hashed}";
        }


        public bool VerifyPassword(string password, string passwordHash)
        {
            var saltAndHash = passwordHash.Split(Delimiter);
            if (saltAndHash.Length != 2)
            {
                return false;
            }

            var saltBytes = Convert.FromBase64String(saltAndHash[0]);
            var hash = saltAndHash[1];
            var reHashPassword = GetHash(password, saltBytes);
            if (hash.Equals(reHashPassword, StringComparison.Ordinal))
            {
                return true;
            }

            return false;
        }

        private static string GetHash(string password, byte[] saltBytes)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}
