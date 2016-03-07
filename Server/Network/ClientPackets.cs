using Server.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Network
{
    public class ClientPackets
    {
        public static readonly ClientPackets Instance = new ClientPackets();

        private delegate void PacketAction(Client client, PacketStream stream);

        private Dictionary<ushort, PacketAction> PacketsDb;
        private XDes DesCipher = new XDes(Program.DesKey);

        public ClientPackets()
        {
            // Loads PacketsDb
            PacketsDb = new Dictionary<ushort, PacketAction>();

            #region Packets
            PacketsDb.Add(0x0010, CS_RequestUpdateIndex);
            PacketsDb.Add(0x0020, CS_RequestFile);
            PacketsDb.Add(0x0030, CS_RequestArguments);
            #endregion
        }

        /// <summary>
        /// Called whenever a packet is received from a client
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        public void PacketReceived(Client client, PacketStream stream)
        {
            // Is it a known packet ID?
            if (!PacketsDb.ContainsKey(stream.GetId()))
            {
                Console.WriteLine("Unknown packet Id: {0}", stream.GetId());
                return;
            }

            // Calls this packet parsing function
            Task.Run(() => { PacketsDb[stream.GetId()].Invoke(client, stream); });
        }

        #region Client-Server Packets (CS)
        
        /// <summary>
        /// Client wants the file list
        /// </summary>
        /// <param name="client">the client</param>
        /// <param name="stream">data</param>
        private void CS_RequestUpdateIndex(Client client, PacketStream stream)
        {
            UpdateHandler.Instance.OnUserRequestUpdateIndex(client);
        }

        /// <summary>
        /// Request a file
        /// </summary>
        /// <param name="client">the client</param>
        /// <param name="stream">data</param>
        private void CS_RequestFile(Client client, PacketStream stream)
        {
            string name = stream.ReadString();
            int offset = stream.ReadInt32();
            string partialHash = "";
            if (offset > 0)
                partialHash = stream.ReadString();

            UpdateHandler.Instance.OnUserRequestFile(client, name, offset, partialHash);
        }

        /// <summary>
        /// Request launch arguments
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        /// TODO: Fix the username part???
        private void CS_RequestArguments(Client client, PacketStream stream)
        {
            using (StreamReader sr = new StreamReader(stream))
            {
                string username = sr.ReadToEnd();
                Program.OnUserRequestArguments(client, username);
            }
        }

        #endregion

        #region Server-Client Packets (SC)

        /// <summary>
        /// Sends UpdateIndex of a file
        /// </summary>
        /// <param name="client">target client</param>
        /// <param name="fileName">file name (hashed)</param>
        /// <param name="fileHash">SHA512 hash of this file</param>
        /// <param name="isLegacy"></param>
        public void UpdateIndex(Client client, string fileName, string fileHash, bool isLegacy)
        {
            PacketStream stream = new PacketStream(0x0011);

            stream.WriteString(fileName, fileName.Length + 1);
            stream.WriteString(fileHash, fileHash.Length + 1);
            stream.WriteBool(isLegacy);

            ClientManager.Instance.Send(client, stream);
        }

        /// <summary>
        /// Informs the end of UpdateIndex sending
        /// </summary>
        /// <param name="client"></param>
        public void UpdateIndexEnd(Client client)
        {
            PacketStream stream = new PacketStream(0x0012);
            ClientManager.Instance.Send(client, stream);
        }

        /// <summary>
        /// Sends bytes of a file
        /// </summary>
        /// <param name="client">target client</param>
        /// <param name="offset">data offset</param>
        /// <param name="data">file data</param>
        public void File(Client client, int offset, byte[] data)
        {
            const int bytesPerPacket = 256;

            for (; offset < data.Length; offset += bytesPerPacket)
            {
                int byteCount;

                PacketStream s = new PacketStream(0x0021);

                s.WriteInt32(offset);
                if (offset + bytesPerPacket > data.Length)
                {
                    s.WriteBool(true);
                    byteCount = data.Length - offset;
                }
                else
                {
                    s.WriteBool(false);
                    byteCount = bytesPerPacket;
                }

                byte[] sendData = new byte[byteCount];
                Array.Copy(data, offset, sendData, 0, byteCount);
                s.WriteBytes(sendData);

                ClientManager.Instance.Send(client, s);
            }
        }

        /// <summary>
        /// Informs the client of required start arguments
        /// </summary>
        /// <param name="client"></param>
        public void Arguments(Client client, string arguments)
        {
            PacketStream stream = new PacketStream(0x0031);

            byte[] argEncrypt = DesCipher.Encrypt(arguments);
            stream.WriteInt32(argEncrypt.Length);
            stream.WriteBytes(argEncrypt);

            ClientManager.Instance.Send(client, stream);
        }

        #endregion
    }
}
