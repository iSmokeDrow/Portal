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

        internal static string tmpPath = string.Format(@"{0}/{1}", Directory.GetCurrentDirectory(), "/tmp/");

        internal static Timer indexTimer;

        internal static Timer otpTimer;

        public static bool Wait = false;

        static void Main(string[] args)
        {
            OPT.LoadSettings();
            DesCipher = new XDes(OPT.GetString("des.key"));

            clientList = new Dictionary<int, Client>();

            Console.WriteLine("Indexing legacy file names...");
            OPT.LoadLegacyFiles();
            Console.WriteLine("\t- {0} legacy files indexed!", OPT.LegacyCount);

            Console.WriteLine("Indexing delete file names...");
            OPT.LoadDeleteFiles();
            Console.WriteLine("\t- {0} delete files indexed!", OPT.DeleteCount);

            IndexManager.Build(false);

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

            Console.Write("Initializing Index Rebuild Service...");
            int rebuildInterval = OPT.GetInt("rebuild.interval") * 1000;
            indexTimer = new Timer(indexTick, null, rebuildInterval, rebuildInterval);
            Console.WriteLine("[OK]");

            Console.Write("Initializing OTP Reset Service...");
            otpTimer = new Timer(otpTick, null, 0, 300000);
            Console.WriteLine("[OK]");

            Console.ReadLine();
        }

        private static async void indexTick(object state) { await Task.Run(() => { IndexManager.Build(true); }); }

        internal static void otpTick(object state) { OTP.HandleOTP(); }
    }
}
