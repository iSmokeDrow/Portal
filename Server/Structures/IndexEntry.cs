using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Structures
{
    public class IndexEntry
    {
        public string FileName { get; set; }
        public string SHA512 { get; set; }
        public bool Legacy { get; set; }
        public bool Delete { get; set; }
    }
}
