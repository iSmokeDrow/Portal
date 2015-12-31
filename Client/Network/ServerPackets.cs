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

        public ServerPackets()
        {
            // Loads PacketsDb
            PacketsDb = new Dictionary<ushort, PacketAction>();

            #region Packets
            PacketsDb.Add(0x0001, SC_LoginResult);

            PacketsDb.Add(0x0011, SC_UpdateIndex);
            PacketsDb.Add(0x0012, SC_UpdateIndexEnd);
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
        /// Result of login try
        /// </summary>
        /// <param name="stream"></param>
        private void SC_LoginResult(PacketStream stream)
        {
            int result = stream.ReadInt32();

            GUI.LoginResponse(result);
        }

        /// <summary>
        /// UpdateIndex entry received
        /// </summary>
        /// <param name="stream"></param>
        private void SC_UpdateIndex(PacketStream stream)
        {
            short nameLength = stream.ReadInt16();
            string name = stream.ReadString(nameLength);
            string fileHash = stream.ReadString(64);
            bool isLegacy = stream.ReadBool();

            UpdateHandler.Instance.OnUpdateIndexReceived(name, fileHash, isLegacy);
        }

        private void SC_UpdateIndexEnd(PacketStream stream)
        {
            UpdateHandler.Instance.OnUpdateIndexEnd();
        }

        #endregion

        #region Client-Server Packets (CS)

        /// <summary>
        /// Sends a login request
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="fingerprint"></param>
        public void Login(string userId, byte[] password, string fingerprint)
        {
            PacketStream stream = new PacketStream(0x0000);

            stream.WriteString(userId, 61);
            stream.WriteBytes(password);
            stream.WriteString(fingerprint, 60);

            ServerManager.Instance.Send(stream);
        }

        /// <summary>
        /// Requests the list of files
        /// </summary>
        public void RequestUpdateIndex()
        {
            PacketStream stream = new PacketStream(0x0010);
            ServerManager.Instance.Send(stream);
        }

        #endregion
    }
}
