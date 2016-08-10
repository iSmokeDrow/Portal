using Server.Functions;
using Server.Network;
using System.Globalization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZLibNet;

namespace Server
{
    public class UpdateHandler
    {
        public static readonly UpdateHandler Instance = new UpdateHandler();
        internal static string updatesDir = string.Concat(Directory.GetCurrentDirectory(), @"\updates\");
        internal static string selfUpdatesDir = string.Concat(Directory.GetCurrentDirectory(), @"\self_updates\");
        internal static string selfUpdatePath = Path.Combine(selfUpdatesDir, @"Launcher.exe");
        internal static string updaterPath = Path.Combine(selfUpdatesDir, @"Updater.exe");

        public void OnUserRequestUpdateDateTime(Client client)
        {
            if (Directory.Exists(updatesDir))
            {
                DateTime dateTime = Directory.GetLastWriteTimeUtc(updatesDir);
                //Console.WriteLine("Current DateTime of updates folder: {0}", dateTime.ToString());
                ClientPackets.Instance.UpdateDateTime(client, dateTime.ToString(CultureInfo.InvariantCulture));
            }
            else { Console.WriteLine("Cannot find updates directory: {0}", updatesDir); }
        }

        public void OnUserRequestSelfUpdate(Client client, string remoteHash)
        {
            if (Directory.Exists(selfUpdatesDir))
            {
                if (File.Exists(selfUpdatePath))
                {
                    string hash = Hash.GetSHA512Hash(selfUpdatePath);
                    if (hash != remoteHash)
                    {
                        //Pack the zip into the /tmp/ folder
                        string zipName = compressFile(selfUpdatePath);
                        
                        ClientPackets.Instance.UpdateSelfUpdate(client, zipName);
                    }
                }
            }
        }

        internal void OnUserRequestUpdater(Client client, string remoteHash)
        {
            if (Directory.Exists(selfUpdatesDir))
            {
                if (File.Exists(updaterPath))
                {
                    string hash = Hash.GetSHA512Hash(updaterPath);
                    if (hash != remoteHash || remoteHash == "NO_HASH")
                    {
                        string zipName = compressFile(updaterPath);
                        ClientPackets.Instance.SendUpdater(client, zipName);
                    }
                }
            }
        }

        public void OnUserRequestUpdateIndex(Client client, int indexType)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles("updates/");
            foreach (string filePath in fileEntries)
            {
                string fileName = Path.GetFileName(filePath);
                bool isLegacy = false;

                if (OPT.LegacyUpdateList.Contains(fileName)) { isLegacy = true; }

                switch (indexType)
                {
                    case 1: // New
                        ClientPackets.Instance.UpdateIndex(client, fileName, Hash.GetSHA512Hash(filePath), isLegacy);
                        break;

                    case 2: // Legacy
                        if (OPT.LegacyUpdateList.Contains(fileName)) { ClientPackets.Instance.UpdateIndex(client, fileName, Hash.GetSHA512Hash(filePath), isLegacy); }
                        break;
                }
            }

            // TODO: Send bool saying this is a legacy only list
            ClientPackets.Instance.UpdateIndexEnd(client, indexType);
        }

        internal void OnUserRequestFile(Client client, string name, int offset, string partialHash)
        {
            string filePath = Path.Combine(@"updates/", name);
            Console.WriteLine(filePath);
            if (!File.Exists(filePath)) { return; }
            
            string zipPath = compressFile(filePath);
            client.filesInUse.Add(zipPath);
            
            ClientPackets.Instance.File(client, zipPath);

            //byte[] zipData = File.ReadAllBytes(string.Concat(zipPath, ".zip"));

            /*if (partialHash != Hash.GetSHA512Hash(filePath))
            {
                // Hashes are different, start from the beggining
                //ClientPackets.Instance.File(client, 0, zipData);
                ClientPackets.Instance.File(client, 0, zipData);
            }
            else
            {
                // Hashes are the same, resume
                ClientPackets.Instance.File(client, offset, zipData);
            }*/

            //zipData = null;

            // TODO: Delete the zip file (zipPath)
            //File.Delete(zipPath);
        }

        internal string compressFile(string filePath)
        {
            string name = OTP.GenerateRandomPassword(10);
            string zipPath = Path.Combine(@"tmp/", name);
            
            Zipper z = new Zipper();
            z.ItemList.Add(filePath);
            z.ZipFile = zipPath;
            z.Zip();
            z = null;
            
            return name;
        }

        internal void OnUserDisconnect(Client client)
        {
            foreach (string fileName in client.filesInUse)
            {
                string fName = Path.Combine(@"tmp/", fileName);
                if (File.Exists(fName))
                    File.Delete(fName);
            }
        }
    }
}
