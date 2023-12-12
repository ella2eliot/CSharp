using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.IO
{
    public class DirectoryTool
    {
        /// <summary>
        /// Create directory structure for specific path.
		/// Ex: c:\aaa\bbb\ccc.
		/// Will create each folder in its folder level if folder not exists.
        /// </summary>
        /// <param name="path"></param>
        public static void CreateDirectory(string path)
        {
            CreateDirectory(path, false);
        }
        /// <summary>
        /// Create directory structure for specific path.
        /// Ex: c:\aaa\bbb\ccc.
        /// Will create each folder in its folder level if folder not exists.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="hasFileName"></param>
        public static void CreateDirectory(string path, bool hasFileName)
        {
            List<string> lists = PathTool.ParseFilePathHierarchy(path);

            // remove last one if has filename
            int upper = hasFileName ? lists.Count-1 : lists.Count;

            // check if is network path
            if (path.StartsWith(@"\\"))
            {
                for (int index = 1; index < upper; index++)
                {
                    string directory = lists[index];
                    try
                    {
                        Directory.GetDirectories(directory);
                    }
                    catch (DirectoryNotFoundException)
                    {
                        Directory.CreateDirectory(directory);
                    }
                }
            }
            else
            {
                for (int index = 1; index < upper; index++)
                {
                    string directory = lists[index];
                    try
                    {
                        Directory.GetDirectories(directory);
                    }
                    catch (DirectoryNotFoundException)
                    {
                        Directory.CreateDirectory(directory);
                    }
                }
            }
        }
    }
}
