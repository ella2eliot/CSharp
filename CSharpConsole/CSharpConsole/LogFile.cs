using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpConsole
{
    public class LogFile
    {
        public LogFile() { }

        public static void OutputTxt(string message)
        {
            // false: 覆蓋原本檔案, true: 原有檔案上新增內容
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(@"WriteLines.txt", true))
            {
                file.WriteLine(string.Format("{0} : {1}", DateTime.Now.ToString(), message));
            }

        }

    }
}
