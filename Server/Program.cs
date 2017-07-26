using System;
using System.Collections.Generic;
using Server.Functions;
using Server.Network;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Server.Structures;

namespace Server
{
    //TODO: Implement HTTP/FTP download engines
    //TODO: Rename bool wait for clarity (possibly to delay?)
    class Program
    {
        public static string RC4Key = "password1";

        public static bool Wait = false;

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI());
        }
    }
}
