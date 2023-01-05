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
        private byte[] GetComplexCombineArray(string data, string key)
        {
            var arrays = new List<byte>();
            var dataBytes = Encoding.Unicode.GetBytes(data);
            var keyBytes = key == null ? new byte[0] : Encoding.Unicode.GetBytes(key);
            var maxLength = dataBytes.Length > keyBytes.Length ? dataBytes.Length : keyBytes.Length;

            for (var i = 0; i < maxLength; i++)
            {
                if (i < dataBytes.Length)
                {
                    arrays.Add(dataBytes[i]);
                }

                if (i < keyBytes.Length)
                {
                    arrays.Add(keyBytes[i]);
                }
            }

            return arrays.ToArray();
        }

        private byte[] GetSimpleCombineArray(string data, string key)
        {
            var input = data + key;

            return Encoding.Unicode.GetBytes(input);
        }
        public string DecryptData(string data, string key)
        {
            throw new ApplicationException("SHA not support decrypt action.");
        }

        public string EncryptData(string data, string key)
        {
            SHA256 SHA256Hasher = SHA256.Create();
            byte[] byte_data = SHA256Hasher.ComputeHash(GetComplexCombineArray(data, key));
            
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < byte_data.Length; i++)
            {
                sb.Append(byte_data[i].ToString("x2"));
            }
            return sb.ToString();
        }
        //public string EncryptData_1(string data, string key)
        //{
        //    byte[] SHA256Data = Encoding.UTF8.GetBytes(data);
        //    SHA256Managed Sha256 = new SHA256Managed();
        //    byte[] by = Sha256.ComputeHash(SHA256Data);
        //    return BitConverter.ToString(by).Replace("-", "");
        //}
    }
}
