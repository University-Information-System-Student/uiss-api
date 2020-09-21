namespace UISS.Security
{
    using System;
    using System.Security.Cryptography;

    public static class PasswordHasher
    {
        private const int IterationCount = 10000;
        private const int SubkeyLength = 256 / 8;
        private const int SaltSize = 128 / 8;

        public static string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException();
            }

            byte[] salt;
            byte[] subkey;

            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, IterationCount))
            {
                salt = deriveBytes.Salt;
                subkey = deriveBytes.GetBytes(SubkeyLength);
            }

            var outputBytes = new byte[1 + SaltSize + SubkeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, SubkeyLength);
            return Convert.ToBase64String(outputBytes);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            if (hashedPasswordBytes.Length != (1 + SaltSize + SubkeyLength) || hashedPasswordBytes[0] != 0x00)
            {
                return false;
            }

            var salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, SaltSize);
            var storedSubkey = new byte[SubkeyLength];
            Buffer.BlockCopy(hashedPasswordBytes, 1 + SaltSize, storedSubkey, 0, SubkeyLength);

            byte[] generatedSubkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, IterationCount))
            {
                generatedSubkey = deriveBytes.GetBytes(SubkeyLength);
            }
            return ByteArraysEqual(storedSubkey, generatedSubkey);
        }

        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }
    }
}
