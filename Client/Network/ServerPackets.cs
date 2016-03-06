using Client.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Network
{
    public class ServerPackets
    {
        public static readonly ServerPackets Instance = new ServerPackets();

        private delegate void PacketAction(PacketStream stream);

        private Dictionary<ushort, PacketAction> PacketsDb;
        private XDes DesCipher = new XDes(Program.DesKey);

        public ServerPackets()
        {
            // Loads PacketsDb
            PacketsDb = new Dictionary<ushort, PacketAction>();

            #region Packets
            PacketsDb.Add(0x0011, SC_UpdateIndex);
            PacketsDb.Add(0x0012, SC_UpdateIndexEnd);
            PacketsDb.Add(0x0021, SC_File);
            PacketsDb.Add(0x0031, SC_Arguments);
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

        #region Server-Client Packets (SC)

        /// <summary>
        /// UpdateIndex entry received
        /// </summary>
        /// <param name="stream"></param>
        private void SC_UpdateIndex(PacketStream stream)
        {
            string name = stream.ReadString();
            string fileHash = stream.ReadString();
            bool isLegacy = stream.ReadBool();

            UpdateHandler.Instance.OnUpdateIndexReceived(name, fileHash, isLegacy);
        }

        private void SC_UpdateIndexEnd(PacketStream stream)
        {
            UpdateHandler.Instance.OnUpdateIndexEnd();
        }

        private void SC_File(PacketStream stream)
        {
            int offset = stream.ReadInt32();
            bool endOfFile = stream.ReadBool();
            byte[] data = stream.ReadBytes(stream.GetSize() - PacketStream.HeaderLength - 5);

            UpdateHandler.Instance.OnFileDataReceived(offset, endOfFile, data);
        }

        private void SC_Arguments(PacketStream stream)
        {
            int len = stream.ReadInt32();
            byte[] arguments = stream.ReadBytes(len);

            GUI.OnArgumentsReceived(DesCipher.Decrypt(arguments));
        }

        #endregion

        #region Client-Server Packets (CS)

        /// <summary>
        /// Requests the list of files
        /// </summary>
        internal void RequestUpdateIndex()
        {
            PacketStream stream = new PacketStream(0x0010);
            ServerManager.Instance.Send(stream);
        }

        internal void RequestFile(string name, int offset, string partialHash)
        {
            PacketStream stream = new PacketStream(0x0020);

            stream.WriteString(name, name.Length + 1);
            stream.WriteInt32(offset);
            stream.WriteString(partialHash, partialHash.Length + 1);

            ServerManager.Instance.Send(stream);
        }

        internal void RequestArguments(string username)
        {
            PacketStream stream = new PacketStream(0x0030);
            stream.WriteString(username, username.Length + 1);
            ServerManager.Instance.Send(stream);
        }

        #endregion
    }
}
