using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater.Structures
{
    public class LauncherSetting
    {
        string _type;
        string _name;
        object _value;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}
