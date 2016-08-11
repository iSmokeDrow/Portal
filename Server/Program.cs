using System;
using System.Collections.Generic;
using Server.Functions;
using Server.Network;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Server
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
        /// It's used to encrypt arguments
        /// </summary>
        public static string DesKey = "password2";

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
                if (!string.IsNullOrEmpty(OPT.GetSetting("update.dir")))
                {
                    return OPT.GetSetting("update.dir");
                }
                else { return string.Format(@"{0}/{1}", Directory.GetCurrentDirectory(), "/updates/"); }
            }
        }

        static internal string tmpPath = string.Format(@"{0}/{1}", Directory.GetCurrentDirectory(), "/tmp/");
        
        static void Main(string[] args)
        {
            OPT.LoadSettings();
            DesCipher = new XDes(DesKey);

            clientList = new Dictionary<int, Client>();

            Console.WriteLine("Checking for Updates directory...");
            if (!Directory.Exists(updatePath))
            {
                Console.WriteLine("\t- Failed to locate: {0}\n!!!Folder has been generated!!!");
                Directory.CreateDirectory(updatePath);
            }
            Console.WriteLine("\t- {0} files loaded from the update folder!\n\t- {1} of which are legacy updates!", Directory.GetFiles(updatePath).Length, OPT.LegacyUpdateList.Count);
            

            Console.Write("Checking for tmp directory...");
            if (!Directory.Exists(tmpPath)) { Directory.CreateDirectory(tmpPath); }
            Console.Write("[OK]\n\t- Cleaning up temp files...");

            int cleanedCount = 0;

            foreach (string filePath in Directory.GetFiles(@"/tmp/"))
            {
                File.Delete(filePath);
                cleanedCount++;
            }
            Console.WriteLine("[OK] ({0} files cleared!)", cleanedCount);

            Console.Write("Initializing client listener... ");
            if (ClientManager.Instance.Start()) { Console.WriteLine("[OK]"); }

            Console.ReadLine();
        }

        internal static void otpTick(Object state) { OTP.HandleOTP(); }

        internal static void OnUserRequestArguments(Client client, string username)
        {
            // TODO : Replace placeholder launch arguments here
            ClientPackets.Instance.Arguments(client, string.Format("/auth_ip:176.31.181.127 /auth_port:13544 /locale:? /country:? /use_nprotect:0 /cash /commercial_shop /allow_double_exec:1 /imbclogin /account:{0} /password:?", username));
        }
    }
}
