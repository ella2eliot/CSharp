using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Security.Cryptography
{
    /// <summary>
    /// 
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class SHA256Wrapper : IEncryption
    {
        public string DecryptData(string data, string key)
        {
            throw new NotImplementedException();
        }

        public string EncryptData(string data, string key)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(data);
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] by = Sha256.ComputeHash(SHA256Data);
            return BitConverter.ToString(by).Replace("-", "");
        }
    }
}
