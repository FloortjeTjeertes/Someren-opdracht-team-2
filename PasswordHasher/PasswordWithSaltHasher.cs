using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace PasswordHasher
{
    public class PasswordWithSaltHasher
    {
        public HashWithSaltResult HashWithSalt(string password, byte[] saltBytes, HashAlgorithm hashAlgo)
        {
            byte[] passwordAsBytes = Encoding.UTF8.GetBytes(password);
            List<byte> passwordWithSaltBytes = new List<byte>();
            passwordWithSaltBytes.AddRange(passwordAsBytes);
            passwordWithSaltBytes.AddRange(saltBytes);
            byte[] digestBytes = hashAlgo.ComputeHash(passwordWithSaltBytes.ToArray());
            return new HashWithSaltResult(Convert.ToBase64String(saltBytes), Convert.ToBase64String(digestBytes));
        }

        public byte[] GenerateSalt(int saltLength)
        {
            RNG rng = new RNG();
            byte[] saltBytes = rng.GenerateRandomCryptographicBytes(saltLength);
            return saltBytes;
        }

        public byte[] GetSaltBytes()
        {
            byte[] saltBytes;
            if (ConfigurationManager.AppSettings["Salt"] == null)
            {
                saltBytes = this.GenerateSalt(64);
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Add("Salt", Convert.ToBase64String(saltBytes));
                config.Save(ConfigurationSaveMode.Modified);
            }
            else
            {
                saltBytes = Convert.FromBase64String(ConfigurationManager.AppSettings["Salt"]);
            }
            return saltBytes;
        }
    }
}
