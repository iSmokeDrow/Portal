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
        internal static bool wait = false;
        public static readonly UpdateHandler Instance = new UpdateHandler();
        internal static string selfUpdatesDir = string.Format(@"{0}\{1}", Directory.GetCurrentDirectory(), @"self_updates");
        internal static string selfUpdatePath = string.Format(@"{0}\{1}", selfUpdatesDir, "Launcher.exe");
        internal static string updaterPath = Path.Combine(selfUpdatesDir, @"Updater.exe");
        internal static string indexPath = Path.Combine(Directory.GetCurrentDirectory(), @"index.opt");
        internal static string tmpPath = string.Format(@"{0}/{1}", Directory.GetCurrentDirectory(), "/tmp/");
        protected List<string> legacyIndex = new List<string>();

        public void OnUserRequestUpdateDateTime(Client client)
        {
            DateTime dateTime = default(DateTime);

            switch (OPT.GetInt("send.type"))
            {
                case 0:
                    dateTime = Directory.GetLastWriteTimeUtc(indexPath);
                    break;

                case 1:
                    break;

                case 2:
                    break;

                case 3:
                    dateTime = Directory.GetLastWriteTimeUtc(IndexManager.UpdatesDirectory);
                    break;
            }

            if (dateTime != default(DateTime)) { ClientPackets.Instance.SC_SendUpdateTime(client, dateTime.ToString(CultureInfo.InvariantCulture)); }
            else { Output.Write(new Message() { Text = string.Format("Failed to get proper update time for Client [{0}]", client.Id), AddBreak = true }); }
        }

        public void OnUserRequestSelfUpdateRequired(Client client, string remoteHash)
        {         
            if (Directory.Exists(selfUpdatesDir))
            {
                if (File.Exists(selfUpdatePath))
                {
                    string hash = Hash.GetSHA512Hash(selfUpdatePath);
                    ClientPackets.Instance.SC_SendSelfUpdateRequired(client, hash != remoteHash);
                }
            }
        }

        internal void OnUserRequestUpdatesEnabled(Client client) { if (OPT.SettingExists("disable.updating")) { ClientPackets.Instance.SC_SendUpdatesDisabled(client, OPT.GetInt("disable.updating")); } }

        public void OnRequestDataUpdateIndex(Client client)
        {
            if (Program.Wait) { ClientPackets.Instance.SC_SendWait(client, ClientPackets.Instance.currentPacketId, OPT.GetInt("wait.period")); }
            else
            {
                List<IndexEntry> filteredIndex = IndexManager.Filter(FilterType.Data);

                foreach (IndexEntry indexEntry in filteredIndex) { ClientPackets.Instance.SC_SendDataEntry(client, indexEntry.FileName, indexEntry.SHA512); }

                ClientPackets.Instance.SC_SendDataIndexEOF(client);
            }
        }

        internal void OnRequestResourceUpdateIndex(Client client)
        {
            if (Program.Wait) { ClientPackets.Instance.SC_SendWait(client, ClientPackets.Instance.currentPacketId, OPT.GetInt("wait.period")); }
            else
            {
                List<IndexEntry> filteredIndex = IndexManager.Filter(FilterType.Resource);

                foreach (IndexEntry indexEntry in filteredIndex) { ClientPackets.Instance.SC_SendResourceEntry(client, indexEntry.FileName, indexEntry.SHA512, indexEntry.Delete); }

                ClientPackets.Instance.SC_SendResourceIndexEOF(client);
            }
        }

        internal void OnRequestFileInfo(Client client, string fileName)
        {
            if (OPT.GetBool("debug")) { Output.Write(new Message() { Text = string.Format("Client [{0}] requested file info for: {1}", client.Id, fileName), AddBreak = true }); }

            string updatePath = string.Format(@"{0}\{1}", IndexManager.UpdatesDirectory, fileName);
            string archiveName = compressFile(updatePath);
            string archivePath = string.Format(@"{0}\{1}.zip", tmpPath, archiveName);

            if (File.Exists(archivePath)) { ClientPackets.Instance.SC_SendFileInfo(client, archiveName, new FileInfo(archivePath).Length); }
            else { /*TODO: Send File Does Not Exist Error?*/ }           
        }

        internal void OnUserRequestFile(Client client, string archiveName)
        {
            if (OPT.GetBool("debug")) { Output.Write(new Message() { Text = string.Format("Client [{0}] requested archive {1}", client.Id, archiveName), AddBreak = true}); }

            ClientPackets.Instance.SC_SendFile(client, string.Format(@"{0}\{1}.zip", tmpPath, archiveName));
        }

        internal string compressFile(string filePath)
        {
            string name = OTP.GenerateRandomPassword(10);
            string zipPath = Path.Combine(tmpPath, string.Concat(name, ".zip"));

            Zipper z = new Zipper();
            z.ItemList.Add(filePath);
            z.ZipFile = zipPath;
            z.Zip();
            z = null;

            return name;
        }

        #region Updater Methods

        internal void OnRequestLauncherInfo(Client client)
        {
            if (OPT.GetBool("debug")) { Output.Write(new Message() { Text = string.Format("Updater [{0}] requested Launcher info", client.Id), AddBreak = true }); }

            if (File.Exists(selfUpdatePath)) { ClientPackets.Instance.SU_SendLauncherInfo(client, new FileInfo(selfUpdatePath).Length, Hash.GetSHA512Hash(selfUpdatePath)); }
        }

        internal void OnRequestLauncherDownload(Client client)
        {
            if (OPT.GetBool("debug")) { Output.Write(new Message() { Text = string.Format("Updater [{0}] requested Launcher download", client.Id), AddBreak = true }); }

            if (File.Exists(selfUpdatePath)) { ClientPackets.Instance.SU_SendLauncher(client, selfUpdatePath); }
        }

        #endregion
    }
}
