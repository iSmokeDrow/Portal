using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Functions;

namespace Server.Network
{
    public class ClientPackets
    {
        public static readonly ClientPackets Instance = new ClientPackets();

        private delegate void PacketAction(Client client, PacketStream stream);

        private Dictionary<ushort, PacketAction> PacketsDb;
        private XDes DesCipher = new XDes(OPT.GetString("des.key"));

        protected bool debug = false;

        public ClientPackets()
        {
            debug = OPT.GetBool("debug");

            // Loads PacketsDb
            PacketsDb = new Dictionary<ushort, PacketAction>();

            #region Packets
            PacketsDb.Add(0x0001, CS_RequestUpdateDateTime);
            PacketsDb.Add(0x0002, CS_RequestSelfUpdate);
            PacketsDb.Add(0x0003, CS_RequestUpdater);
            PacketsDb.Add(0x0010, CS_RequestUpdateIndex);
            PacketsDb.Add(0x0030, CS_RequestArguments);
            PacketsDb.Add(0x0100, CS_RequestValidation);
            PacketsDb.Add(0x00DC, CS_RequestDisconnect);
            PacketsDb.Add(0x9999, CS_RequestDesKey);
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

        private void CS_RequestDesKey(Client client, PacketStream stream)
        {
            if (debug)
            {
                Console.Write("Client [{0}] requested des.key...", client.Id);
            }

            UserHandler.Instance.OnUserRequestDesKey(client);
        }

        private void CS_RequestValidation(Client client, PacketStream stream)
        {
            int userLength = stream.ReadInt32();
            string username = DesCipher.Decrypt(stream.ReadBytes(userLength));
            int passLength = stream.ReadInt32();
            string password = DesCipher.Decrypt(stream.ReadBytes(passLength));
            string fingerprint = stream.ReadString();

            UserHandler.Instance.OnValidateUser(client, username, password, fingerprint);
        }

        private void CS_RequestUpdateDateTime(Client client, PacketStream stream)
        {
            UpdateHandler.Instance.OnUserRequestUpdateDateTime(client);
        }

        private void CS_RequestSelfUpdate(Client client, PacketStream stream)
        {
            UpdateHandler.Instance.OnUserRequestSelfUpdate(client, stream.ReadString());
        }

        private void CS_RequestUpdater(Client client, PacketStream stream)
        {
            UpdateHandler.Instance.OnUserRequestUpdater(client, stream.ReadString());          
        }

        /// <summary>
        /// Client wants the file list
        /// </summary>
        /// <param name="client">the client</param>
        /// <param name="stream">data</param>
        private void CS_RequestUpdateIndex(Client client, PacketStream stream)
        {
            UpdateHandler.Instance.OnUserRequestUpdateIndex(client, stream.ReadInt32());
        }

        /// <summary>
        /// Request launch arguments
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stream"></param>
        private void CS_RequestArguments(Client client, PacketStream stream)
        {
            string name = stream.ReadString();
            UserHandler.Instance.OnUserRequestArguments(client, name);
        }

        private void CS_RequestDisconnect(Client client, PacketStream stream)
        {
            UserHandler.ClientList.Remove(client);
            OkDisconnect(client);
            client.ClSocket.Close();
            if (debug) { Console.WriteLine("Client [{0}] disconnected!", client.Id); }
        }

        #endregion

        #region Server-Client Packets (SC)

        public void UpdateDateTime(Client client, string DateTime)
        {
            PacketStream stream = new PacketStream(0x000A);

            stream.WriteString(DateTime, DateTime.Length + 1);

            ClientManager.Instance.Send(client, stream);
        }

        public void UpdateSelfUpdate(Client client, string tmpName)
        {
            PacketStream stream = new PacketStream(0x000B);
            stream.WriteString(tmpName, tmpName.Length + 1);
            ClientManager.Instance.Send(client, stream);
        }

        internal void SendUpdater(Client client, string tmpName)
        {
            PacketStream stream = new PacketStream(0x001E);
            stream.WriteString(tmpName, tmpName.Length + 1);
            ClientManager.Instance.Send(client, stream);
        }

        /// <summary>
        /// Sends UpdateIndex of a file
        /// </summary>
        /// <param name="client">target client</param>
        /// <param name="fileName">file name (hashed)</param>
        /// <param name="fileHash">SHA512 hash of this file</param>
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
        public void UpdateIndexEnd(Client client, int type)
        {
            PacketStream stream = new PacketStream(0x0012);
            stream.WriteInt32(type);
            ClientManager.Instance.Send(client, stream);
        }

        /// <summary>
        /// Sends bytes of a file
        /// </summary>
        /// <param name="client">target client</param>
        /// <param name="path">zip file path</param>
        public void File(Client client, string path)
        {
            PacketStream s = new PacketStream(0x0021);
            s.WriteString(path, path.Length + 1);
            ClientManager.Instance.Send(client, s);
        }

        public void OTP(Client client, string otp)
        {
            PacketStream stream = new PacketStream(0x0013);

            byte[] encOTP = DesCipher.Encrypt(otp);
            stream.WriteInt32(encOTP.Length);
            stream.WriteBytes(encOTP);

            ClientManager.Instance.Send(client, stream);
        }

        public void SendBanStatus(Client client, int banType)
        {
            PacketStream stream = new PacketStream(0x0014);
            stream.WriteInt32(banType);

            ClientManager.Instance.Send(client, stream);
        }

        public void SendAccountNull(Client client) { ClientManager.Instance.Send(client, new PacketStream(0x0015)); }

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

        public void OkDisconnect(Client client) { ClientManager.Instance.Send(client, new PacketStream(0x0099)); }

        #endregion
    }
}
