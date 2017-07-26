using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Structures
{
    public class Message
    {
        internal string Text { get; set; }
        protected bool addBreak = false;
        internal bool AddBreak { get { return addBreak; } set { addBreak = value; if (value) { BreakCount = 1; } } }
        internal int BreakCount { get; set; }

        internal Message() { }

        internal Message(string text, bool addBreak)
        {
            Text = text;
            AddBreak = true;
            BreakCount = 1;
        }

        internal Message(string text, int breakCount)
        {
            Text = text;
            AddBreak = true;
            BreakCount = breakCount;
        }
    }
}
