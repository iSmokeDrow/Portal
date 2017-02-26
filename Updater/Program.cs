using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.ComponentModel;
using Updater.Functions;
using Updater.Network;

namespace Updater
{
    class Program
    {
        /// <summary>
        /// Server and client must have the same RC4 key
        /// it's used to encrypt packet data
        /// </summary>
        public static string RC4Key = "password1";

        /// <summary>
        /// Server and client must have the same DES Key
        /// It's used to encrypt passwords
        /// </summary>
        public static string DesKey = "password2";

        static void Main(string[] args)
        {
            string processName_1 = "launcher";
            string processName_2 = "client";

            Console.Write("Loading configuration info from config.opt...");
            OPT.Read();
            Console.WriteLine("[OK]");

            Console.Write("Checking for open Launcher...");

            if (Process.GetProcessesByName(processName_1).Length > 0)
            {
                foreach (var process in Process.GetProcessesByName(processName_1)) { process.Kill(); }
            }

            if (Process.GetProcessesByName(processName_2).Length > 0)
            {
                foreach (var process in Process.GetProcessesByName(processName_2)) { process.Kill(); }
            }

            Console.Write("[OK]\nConnecting to the Portal Server...");
            try
            {
                if (ServerManager.Instance.Start(OPT.GetString("ip"), OPT.GetInt("port")))
                {

                    Console.Write("[OK]\nRequesting communication key...");
                    ServerPackets.Instance.US_RequestDesKey();
                }
                else { Console.WriteLine("[FAIL]"); }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errors:\n\t{0}", ex.ToString());
            }

            Console.ReadLine();
            
        }

        internal static void OnDesKeyReceived()
        {
            Console.Write("Requesting launcher info...");
            ServerPackets.Instance.US_RequestLauncherInfo();
        }

        internal static void OnLauncherInfoReceived()
        {
            Console.Write("[OK]\nRequesting launcher download...");
            ServerPackets.Instance.US_RequestLauncherDownload();
        }


        internal static void OnLauncherVerified()
        {
            Console.WriteLine("Starting the Launcher!");
            var p = new ProcessStartInfo();
            p.FileName = string.Concat(Directory.GetCurrentDirectory(), @"\Launcher.exe");
            Process.Start(p);
            Environment.Exit(0);
        }
    }
}
