using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Server.Functions;

namespace Server
{
    class Program
    {
        static TcpListener listener;
        static IPAddress ip = null;
        static int port = 0;
        static int clientCount = 0;

        static void Main(string[] args)
        {
            OPT.LoadSettings();
            Console.ReadLine();
        }
    }
}
