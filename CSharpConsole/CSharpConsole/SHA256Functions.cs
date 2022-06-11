using System;
using System.Security.Cryptography;
using System.Text;

namespace CSharpConsole
{
    public class SHA256Functions
    {
        public SHA256Functions() {         
        }

        public string TencentMPTSHA256(string clientId, string secretKey, int ts)
        {
            string str = string.Format("{0},{1},{2}", clientId, secretKey, ts);
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] by = Sha256.ComputeHash(SHA256Data);
            return BitConverter.ToString(by).Replace("-", "");
        }

    }
}
