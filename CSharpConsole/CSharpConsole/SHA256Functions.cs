using SYS.Utilities.Security.Cryptography;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CSharpConsole
{
    public static class SHA256Functions
    {
        public static string TencentMPTSHA256(string clientId, string secretKey, int ts)
        {
            string str = string.Format("{0},{1},{2}", clientId, secretKey, ts);
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] by = Sha256.ComputeHash(SHA256Data);
            return BitConverter.ToString(by).Replace("-", "");
        }
        public static string EncryptData (string data, string key)
        {
            var sha256 = new SHA256Wrapper();
            return sha256.EncryptData(data, key);
        }

    }
}
