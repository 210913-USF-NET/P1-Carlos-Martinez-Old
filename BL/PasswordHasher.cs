using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

/// Handles all the hash related password shenanigans. 
/// Sourced from: https://stackoverflow.com/questions/4181198/how-to-hash-a-password
/// 
/// HASH
/// var hash = PasswordHasher.Hash("mypassword");
/// 
/// VERIFY
/// var result = SecurePasswordHasher.Verify("mypassword", hash);

namespace BL
{
    class PasswordHasher
    {
        // Size of salt
        private const int SaltSize = 16;

        // Size of hash
        private const int HashSize = 20;

        // Create a hash
        public static string Hash(string password, int iterations)
        {
            // create salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            // create hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            // combine salt and hash
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            // convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);

            // format hash with extra information
            return string.Format($"$MYHASH$V1${iterations}${base64Hash}");
        }

        // creates a hash from a password with 1000 iterations
        public static string Hash(string password)
        {
            return Hash(password, 10000);
        }

        // check if hash is supported
        public static bool IsHashSupported(string hashString)
        {
            return hashString.Contains("$MYHASH$V1$");
        }

        // verifies password against hash
        public static bool Verify(string password, string hashedPassword)
        {
            // check hash
            if(!IsHashSupported(hashedPassword))
            {
                throw new NotSupportedException("The hashtype is not supported");
            }

            // extract iteration and base64 string
            var splittedHashString = hashedPassword.Replace("$MYHASH$V1$", "").Split("$");
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            // get hash bytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            // get salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // results
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i+SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
