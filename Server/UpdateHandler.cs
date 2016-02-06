using Client.Functions;
using Server.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class UpdateHandler
    {
        public static readonly UpdateHandler Instance = new UpdateHandler();


        public void OnUserRequestUpdateIndex(Client client)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles("updates/");
            foreach (string fileName in fileEntries)
            {
                ClientPackets.Instance.UpdateIndex(client, Path.GetFileName(fileName), Hash.GetSHA512Hash(fileName), true);
            }
            ClientPackets.Instance.UpdateIndexEnd(client);
        }

        internal void OnUserRequestFile(Client client, string name, int offset, string partialHash)
        {
            if (!File.Exists("updates/"+name))
            {
                // TODO : What if file doesn't exists?
                return;
            }

            byte[] data = File.ReadAllBytes("updates/" + name);
            if (partialHash != Hash.GetSHA512Hash(data, offset))
            {
                // Hashes are different, start from the beggining
                ClientPackets.Instance.File(client, 0, data);
            }
            else
            {
                // Hashes are the same, resume
                ClientPackets.Instance.File(client, offset, data);
            }
        }
    }
}
