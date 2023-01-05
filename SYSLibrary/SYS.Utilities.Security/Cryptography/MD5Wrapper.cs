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
    public class MD5Wrapper : IEncryption
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string EncryptData(string data, string key)
        {

            var sb = new StringBuilder();
            var md5Hasher = MD5.Create();
            var input = GetComplexCombineArray(data, key);
            var computeHash = md5Hasher.ComputeHash(input);

            sb.Clear();

            for (var i = 0; i < computeHash.Length; i++)
            {
                sb.Append(computeHash[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string DecryptData(string data, string key)
        {
            throw new ApplicationException("MD5 not support decrypt action.");
        }
    }
}
