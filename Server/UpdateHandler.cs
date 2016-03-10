using Server.Functions;
using Server.Network;
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

        public void OnUserRequestUpdateIndex(Client client)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles("updates/");
            foreach (string filePath in fileEntries)
            {
                string fileName = Path.GetFileName(filePath);
                bool isLegacy = false;

                if (OPT.LegacyUpdateList.Contains(fileName)) { isLegacy = true; }

                ClientPackets.Instance.UpdateIndex(client, fileName, Hash.GetSHA512Hash(filePath), isLegacy);
            }
            ClientPackets.Instance.UpdateIndexEnd(client);
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
