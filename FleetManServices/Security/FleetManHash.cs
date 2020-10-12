using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FleetManServices.Security
{
    public class FleetManHash
    {
        private byte[] GetHash(string saltedpassword)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
            {
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltedpassword));
            }
        }

        public string GetSalt()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[512];

            provider.GetBytes(buffer);
            string salt = BitConverter.ToString(buffer);
            return salt.Replace("-", "");
        }

        public string GetHashedPassword(string password, string salt)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(password + salt))
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
