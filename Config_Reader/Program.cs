using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config_Reader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Reading config.opt...");
            OPT opt = new OPT();
            opt.Start();
            Console.ReadLine();
        }
    }
}
