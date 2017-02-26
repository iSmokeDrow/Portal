using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Updater.Functions;

namespace Updater.Network
{
    public class ServerPackets
    {
        public static readonly ServerPackets Instance = new ServerPackets();

        private delegate void PacketAction(PacketStream stream);

        private Dictionary<ushort, PacketAction> PacketsDb;

        private XDes DesCipher;

        public ServerPackets()
        {
            // Loads PacketsDb
            PacketsDb = new Dictionary<ushort, PacketAction>();

            PacketsDb.Add(0x1001, SU_ReceiveLauncherInfo);
            PacketsDb.Add(0x1101, SU_ReceiveLauncherBytes);
            PacketsDb.Add(0x1102, SU_ReceiveLauncherEOF);
            PacketsDb.Add(0x9999, SU_ReceiveDesKey);
            
            #region Packets

            #endregion
        }

        /// <summary>
        /// Called whenever a packet is received from the Server
        /// </summary>
        /// <param name="stream"></param>
        public void PacketReceived(PacketStream stream)
        {
            // Is it a known packet ID?
            if (!PacketsDb.ContainsKey(stream.GetId()))
            {
                Console.WriteLine("Unknown packet Id: {0}", stream.GetId());
                return;
            }

            // Calls this packet parsing function
            Task.Run(() => { PacketsDb[stream.GetId()].Invoke(stream); });
        }

        #region Server-Updater Packets (SU)

        private void SU_ReceiveDesKey(PacketStream stream)
        {
            DesCipher = new XDes(stream.ReadString());
            Console.WriteLine("[OK]");
            Program.OnDesKeyReceived();
        }

        private void SU_ReceiveLauncherInfo(PacketStream stream)
        {
            long fileLength = stream.ReadInt64();
            int encryptionLength = stream.ReadInt32();
            byte[] encryptedBuff = stream.ReadBytes(encryptionLength);
            string fileHash = DesCipher.Decrypt(encryptedBuff).Trim('\0');

            UpdateHandler.Initialize(fileLength, fileHash);
            Program.OnLauncherInfoReceived();
        }

        private void SU_ReceiveLauncherBytes(PacketStream stream)
        {
            try
            {
                int chunkSize = stream.ReadInt32();
                int offset = stream.ReadInt32();
                byte[] bytes = stream.ReadBytes(chunkSize);
                UpdateHandler.WriteToBuffer(chunkSize, offset, bytes);
                
            }
            catch (Exception ex) { Console.WriteLine("{0} SU_ReceiveLauncherBytes() Exception", ex.ToString()); }
        }

        private void SU_ReceiveLauncherEOF(PacketStream stream) { UpdateHandler.WriteFile(); }

        #endregion

        #region Updater-Server Packets (US)

        internal void US_RequestDesKey()
        {
            PacketStream stream = new PacketStream(0x9999);
            ServerManager.Instance.Send(stream);
        }

        internal void US_RequestLauncherInfo() { ServerManager.Instance.Send(new PacketStream(0x1000)); }

        internal void US_RequestLauncherDownload() { ServerManager.Instance.Send(new Network.PacketStream(0x1100)); }

        #endregion
    }
}
