using System.Collections.Generic;
using Server.Structures;
using Server.Network;
using System.Globalization;
using System;
using System.IO;
using ZLibNet;

namespace Server.Functions
{
    public class UpdateHandler
    {
        public static readonly UpdateHandler Instance = new UpdateHandler();
        internal static string selfUpdatesDir = string.Concat(Directory.GetCurrentDirectory(), @"\self_updates\");
        internal static string selfUpdatePath = Path.Combine(selfUpdatesDir, @"Launcher.exe");
        internal static string updaterPath = Path.Combine(selfUpdatesDir, @"Updater.exe");
        internal static string indexPath = Path.Combine(Directory.GetCurrentDirectory(), @"index.opt");
        public List<IndexEntry> UpdateIndex = new List<IndexEntry>();

        public UpdateHandler()
        {

        }

        public void LoadUpdateList()
        {
            using (StreamReader sr = new StreamReader(File.Open(string.Format(@"{0}\{1}", Directory.GetCurrentDirectory(), "index.opt"), FileMode.Open, FileAccess.Read)))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] optBlocks = line.Split('|');
                    if (optBlocks.Length == 3)
                    {
                        UpdateIndex.Add(new IndexEntry { FileName = optBlocks[0], SHA512 = optBlocks[1], Legacy = Convert.ToBoolean(Convert.ToInt32(optBlocks[2])) });
                    }
                }
            }
        }

        public void OnUserRequestUpdateDateTime(Client client)
        {
            if (File.Exists(indexPath))
            {
                DateTime dateTime = Directory.GetLastWriteTimeUtc(indexPath);
                ClientPackets.Instance.UpdateDateTime(client, dateTime.ToString(CultureInfo.InvariantCulture));
            }
            else { Console.WriteLine("Cannot find updates file: {0}", indexPath); }
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
            foreach (IndexEntry indexEntry in UpdateIndex)
            {
                ClientPackets.Instance.UpdateIndex(client, indexEntry.FileName, indexEntry.SHA512, indexEntry.Legacy);
            }

            ClientPackets.Instance.UpdateIndexEnd(client, indexType);
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
