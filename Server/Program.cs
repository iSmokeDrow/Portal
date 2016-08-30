using System;
using System.Collections.Generic;
using Server.Functions;
using Server.Network;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    //TODO: Implement HTTP/FTP download engines
    class Program
    {
        /// <summary>
        /// Server and client must have the same RC4 key
        /// it's used to encrypt packet data
        /// </summary>
        public static string RC4Key = "password1";

        static XDes DesCipher;

        static internal int clientCount
        {
            get
            {
                if (clientList != null) { return clientList.Count; }
                else { return 0; }
            }
        }
        static internal Dictionary<int, Client> clientList;

        static internal string updatePath
        {
            get
            {
                if (!string.IsNullOrEmpty(OPT.GetString("update.dir")))
                {
                    return OPT.GetString("update.dir");
                }
                else { return string.Format(@"{0}/{1}", Directory.GetCurrentDirectory(), "/updates/"); }
            }
        }

        static internal string tmpPath = string.Format(@"{0}/{1}", Directory.GetCurrentDirectory(), "/tmp/");

        internal static Timer otpTimer;
        
        static void Main(string[] args)
        {
            OPT.LoadSettings();
            DesCipher = new XDes(OPT.GetString("des.key"));

            clientList = new Dictionary<int, Client>();

            Console.WriteLine("Indexing update files...");
            UpdateHandler.Instance.LoadUpdateList();

            Console.WriteLine("\t- {0} files indexed from the update index!\n\t- {1} of which are legacy updates!", UpdateHandler.Instance.UpdateIndex.Count, UpdateHandler.Instance.UpdateIndex.FindAll(i => i.Legacy == true).Count);
            

            Console.Write("Checking for tmp directory...");
            if (!Directory.Exists(tmpPath)) { Directory.CreateDirectory(tmpPath); }
            Console.Write("[OK]\n\t- Cleaning up temp files...");

            int cleanedCount = 0;

            foreach (string filePath in Directory.GetFiles(tmpPath))
            {
                File.Delete(filePath);
                cleanedCount++;
            }
            Console.WriteLine("[OK] ({0} files cleared!)", cleanedCount);

            Console.Write("Initializing client listener... ");
            if (ClientManager.Instance.Start()) { Console.WriteLine("[OK]"); }

            Console.Write("Starting OTP Reset Service...");
            otpTimer = new Timer(otpTick, null, 0, 300000);
            Console.WriteLine("[OK]");

            Console.ReadLine();
        }

        internal static void otpTick(Object state) { OTP.HandleOTP(); }
    }
}
