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
            // TODO : Keep these file entries in a text file so we don't have to calculate their hashes everytime?

            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles("updates/");
            foreach (string fileName in fileEntries)
            {
                ClientPackets.Instance.UpdateIndex(client, Path.GetFileName(fileName), Hash.GetSHA512Hash(fileName), true);
            }
            ClientPackets.Instance.UpdateIndexEnd(client);
        }
    }
}
