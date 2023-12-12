using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.IO
{
    public class EncodingMapper
    {
        public string EncodingString { get; set; }
        public int Length { get; set; }
        public EncodingMapper(string encodingString, int length)
        {
            this.EncodingString = encodingString;
            this.Length = length;
        }

    }
}
