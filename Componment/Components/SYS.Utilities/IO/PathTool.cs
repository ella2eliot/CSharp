using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.IO
{
    /// <summary>
	/// 
	/// </summary>
	public static class PathTool
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeBase"></param>
        /// <returns></returns>
        public static string RemoveCodeBase(string codeBase)
        {
            var result = codeBase.Replace("file:///", "");

            result = result.Replace("/", "\\");

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static string RemoveCodeBase(Assembly assembly)
        {
            return RemoveCodeBase(assembly.CodeBase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static string GetDirectoryName(Assembly assembly)
        {
            var codebase = RemoveCodeBase(assembly);
            return Path.GetDirectoryName(codebase);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<string> ParseFilePathHierarchy(string filePath)
        {
            const string remotePrompt = @"\\";
            var parseResults = new List<string>();

            if (filePath.StartsWith(remotePrompt))
            {
                // remote share folder
                var temp1 = filePath.Substring(2);
                var directories = temp1.Split(new[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

                parseResults.AddRange(directories.Select((t, index) => remotePrompt + string.Join(@"\", directories, 0, index + 1)));
            }
            else
            {
                var directories = filePath.Split(new[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);

                parseResults.AddRange(directories.Select((t, index) => string.Join(@"\", directories, 0, index + 1)));
            }

            return parseResults;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileLocation"></param>
        /// <returns></returns>
        public static List<string> ParseFolder(string fileLocation)
        {
            var directories = fileLocation.Split(new[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            var parseResults = new List<string>();

            for (var index = 1; index < directories.Length; index++)
            {
                var temp = string.Join("/", directories, index, directories.Length - index);
                var location = string.Format("{0} : {1}", index, temp);

                parseResults.Add(location);
            }

            return parseResults;
        }
    }
}
