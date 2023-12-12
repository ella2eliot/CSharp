using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.IO
{
    public class FileContext
    {
        public List<byte> Context { get; set; }
        public int CodePage { get; set; }

        public FileContext()
        {
            CodePage = 0;
        }   
    }
}
