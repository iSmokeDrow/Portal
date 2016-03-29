using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.ComponentModel;

namespace Updater
{
    class Program
    {
        static string zipName;
        static string zipPath;

        static void Main(string[] args)
        {
            zipName = string.Concat(args[1], ".zip");
            zipPath = Path.Combine(string.Concat(Directory.GetCurrentDirectory(), "/tmp/"), zipName);
            string processName_1 = "launcher";
            string processName_2 = "client";
            bool close = false;

            Console.WriteLine("Checking for open Launcher...");

            if (Process.GetProcessesByName(processName_1).Length > 0)
            {
                foreach (var process in Process.GetProcessesByName(processName_1)) { process.Kill(); }
            }

            if (Process.GetProcessesByName(processName_2).Length > 0)
            {
                foreach (var process in Process.GetProcessesByName(processName_1)) { process.Kill(); }
            }

            Console.WriteLine("Downloading Launcher update from: {0}", args[0]);

            WebClient client = new WebClient();
            client.DownloadFileCompleted += (o, x) =>
            {
                client.Dispose();

                Console.WriteLine("Updating your Launcher...");

                if (File.Exists("Launcher.exe")) { File.Delete("Launcher.exe"); }

                ZIP.Unpack(zipPath, Directory.GetCurrentDirectory());

                if (File.Exists("Launcher.exe"))
                {
                    Console.WriteLine("Update completed!");
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = @"Launcher.exe";
                    Process.Start(startInfo);
                    close = true;
                }
            };
            client.DownloadFileAsync(new Uri(args[0]), zipPath, client);

            if (!close) { Console.ReadLine(); }
        }
    }
}
