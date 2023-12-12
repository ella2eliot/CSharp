using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SYS.Utilities.IO
{
    public class FileTool
    {
        private const string BIG5 = "^[\x00-\xFF]";
        private const string Unicode = "^\xFF\xFE";
        private const string UTF8 = "^\xEF\xBB\xBF";

        private static Dictionary<Encoding, EncodingMapper> CodeLists { get; set; }

        public FileTool()
        {
            CodeLists = new Dictionary<Encoding, EncodingMapper>
            {
                { Encoding.UTF8, new EncodingMapper(UTF8, 3) },
                { Encoding.Unicode, new EncodingMapper(Unicode, 2)},
                { Encoding.GetEncoding("big5"), new EncodingMapper(BIG5, 1) }
            };
        }

        public static FileContext GetFileContext(string fileName)
        {
            var context = new FileContext { Context = new List<byte>() };

            using(var stream=new FileStream(fileName, FileMode.Append, FileAccess.Read))
            {
                var bytes = new byte[512];
                var read = stream.Read(bytes, 0, bytes.Length);

                while (read > 0)
                {
                    var read1 = read;

                    context.Context.AddRange(bytes.Where((b, i)=>i < read1));
                    read = stream.Read(bytes, 0, bytes.Length);
                }
            }
            return context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadAllText(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var encoding = GetEncoding(bytes);
            var length = CodeLists[encoding].Length;

            return encoding.GetString(bytes, length, bytes.Length - length);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Encoding GetEncoding(byte[] data)
        {
            var charList = new List<char>();

            foreach (var b in data)
            {
                charList.Add((char)b);
            }

            var s = new string(charList.ToArray());
            var result = Encoding.GetEncoding("big5");

            foreach (var code in CodeLists)
            {
                if(Regex.IsMatch(s, code.Value.EncodingString))
                {
                    result = code.Key;
                    break;
                }
            }

            return result;
        }
        /// <summary>
        /// Replace special character to underline
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string FixFileName(string fileName)
        {
            return Regex.Replace(fileName, @"[\\/:\*\?""<>\|]", "_", RegexOptions.IgnoreCase);
        }
        public static void CreateZipFile(string zipFileName, List<string> zipFileLists)
        {
            using (var zip=new ZipFile(zipFileName))
            {
                foreach (var file in zipFileLists)
                {
                    if (File.Exists(file))
                    {
                        zip.AddFile(file, "");
                    }
                    else if(Directory.Exists(file))
                    {
                        var directoryName = Path.GetFileName(file);
                        zip.AddDirectory(file, directoryName);
                    }
                    zip.Save();
                }
            }
        }

        public static void ExtractZipFiles(string storedPath, string fileName)
        {
            using (var zip=new ZipFile(fileName))
            {
                zip.ExtractAll(storedPath, ExtractExistingFileAction.OverwriteSilently);
            }
        }
    }
}
