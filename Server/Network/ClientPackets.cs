using System;
using System.Collections.Generic;
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

        public ClientPackets()
        {
            // Loads PacketsDb
            PacketsDb = new Dictionary<ushort, PacketAction>();

            #region Packets
            PacketsDb.Add(0x0000, CS_Login);
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
        /// Client wants to LogIn
        /// </summary>
        /// <param name="client">the client</param>
        /// <param name="stream">data</param>
        private void CS_Login(Client client, PacketStream stream)
        {
            string userId = stream.ReadString(60);
            byte[] password = stream.ReadBytes(8);
            string fingerprint = stream.ReadString(60);

            // TODO : Call Server.Login(userId, password, fingerprint);
        }

        #endregion

        #region Server-Client Packets (SC)

        /// <summary>
        /// Sends the result of a login try to this client
        /// </summary>
        /// <param name="client">the client</param>
        /// <param name="result">result code</param>
        public void LoginResult(Client client, int result)
        {
            PacketStream stream = new PacketStream(0x0001);

            stream.WriteInt32(result);

            ClientManager.Instance.Send(client, stream);
        }

        #endregion
    }
}
