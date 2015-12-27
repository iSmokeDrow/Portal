using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Structures
{
    /// <summary>
    /// Stores information regarding a data.000 entry
    /// </summary>
    public class IndexEntry
    {
        /// <summary>
        /// The hashed file name
        /// </summary>
        public string FileHash
        {
            get;
            set;
        }
        /// <summary>
        /// The size of the file
        /// </summary>
        public int Size
        {
            get;
            set;
        }
        /// <summary>
        /// The offset the file will begin @ inside it's data.xxx housing
        /// </summary>
        public int Offset
        {
            get;
            set;
        }
        /// <summary>
        /// Data.XXX file this entry belongs to
        /// </summary>
        public int DataID { get; set;}
    }
}
