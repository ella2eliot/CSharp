using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYS.Utilities.Data
{
    public class StoredProcedureParams
    {
        // Summary
        //     Parameter name.
        public string Name { get; set; }
        // Summary:
        //     Parameter value.
        public object Value { get; set; }
    }

    public class IndexParams
    {
        // Summary
        //     Parameter name.
        public string Name { get; set; }
        // Summary:
        //     Parameter value.
        public object Value { get; set; }
    }
}
