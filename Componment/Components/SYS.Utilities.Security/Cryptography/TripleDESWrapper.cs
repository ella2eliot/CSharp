using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SYS.Utilities.Security.Cryptography
{
    /// <summary>
	/// 
	/// </summary>
	[ClassInterface(ClassInterfaceType.AutoDual)]
    public class TripleDESWrapper : IEncryption
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string EncryptData(string data, string key)
        {
            // Create a MemoryStream.
            MemoryStream mStream = new MemoryStream();
            // Create a CryptoStream using the MemoryStream 
            // and the passed key and initialization vector (IV).
            var tdes = new TripleDESCryptoServiceProvider();
            var md5 = new MD5CryptoServiceProvider();
            var keyBytes = Encoding.ASCII.GetBytes(key);
            var keySb = new StringBuilder();
            var lvSb = new StringBuilder();

            foreach (var b in keyBytes)
            {
                lvSb.Append(b.ToString("x2").ToLower());

                if (keySb.Length % 2 == 0)
                {
                    continue;
                }

                keySb.Append(b.ToString("x2").ToLower());
            }

            var rgbKey = md5.ComputeHash(Encoding.ASCII.GetBytes(keySb.ToString()));
            var rgbIv = md5.ComputeHash(Encoding.ASCII.GetBytes(lvSb.ToString()));
            var encryptor = tdes.CreateEncryptor(rgbKey, rgbIv);
            CryptoStream cStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write);
            // Convert the passed string to a byte array.
            var buffer = Encoding.Unicode.GetBytes(data);
            // Write the byte array to the crypto stream and flush it.
            cStream.Write(buffer, 0, buffer.Length);
            cStream.FlushFinalBlock();
            // Get an array of bytes from the 
            // MemoryStream that holds the 
            // encrypted data.
            var ret = mStream.ToArray();
            // Close the streams.
            cStream.Close();
            mStream.Close();
            // Return the encrypted buffer.
            return Convert.ToBase64String(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string DecryptData(string data, string key)
        {
            // Create a MemoryStream.
            var encryptData = Convert.FromBase64String(data);
            var mStream = new MemoryStream(encryptData);
            // Create a CryptoStream using the MemoryStream 
            // and the passed key and initialization vector (IV).
            var tdes = new TripleDESCryptoServiceProvider();
            var md5 = new MD5CryptoServiceProvider();
            var keyBytes = Encoding.ASCII.GetBytes(key);
            var keySb = new StringBuilder();
            var lvSb = new StringBuilder();

            foreach (var b in keyBytes)
            {
                lvSb.Append(b.ToString("x2").ToLower());

                if (keySb.Length % 2 == 0)
                {
                    continue;
                }

                keySb.Append(b.ToString("x2").ToLower());
            }

            var rgbKey = md5.ComputeHash(Encoding.ASCII.GetBytes(keySb.ToString()));
            var rgbIv = md5.ComputeHash(Encoding.ASCII.GetBytes(lvSb.ToString()));
            var decryptor = tdes.CreateDecryptor(rgbKey, rgbIv);
            var csDecrypt = new CryptoStream(mStream, decryptor, CryptoStreamMode.Read);
            // Create buffer to hold the decrypted data.
            byte[] buffer = new byte[encryptData.Length];
            // Read the decrypted data out of the crypto stream
            // and place it into the temporary buffer.
            csDecrypt.Read(buffer, 0, buffer.Length);
            var decryptData = Encoding.Unicode.GetString(buffer);

            decryptData = Regex.Replace(decryptData, "\\u0000", "");

            return decryptData;
        }
    }
}
