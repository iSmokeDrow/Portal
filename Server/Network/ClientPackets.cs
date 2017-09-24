using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Functions;
using Server.Structures;

namespace Server.Network
{
    // TODO: Update sending file info to always send a DES encoded hash of the file
    public class ClientPackets
    {
        public static readonly ClientPackets Instance = new ClientPackets();

        public ushort currentPacketId = 0x0000;

        private delegate void PacketAction(Client client, PacketStream stream);

        private Dictionary<ushort, PacketAction> PacketsDb;
        private XDes DesCipher = new XDes(OPT.GetString("des.key"));

        protected bool debug = false;

        public ClientPackets()
        {
            debug = OPT.GetBool("debug");

            // Loads PacketsDb
            PacketsDb = new Dictionary<ushort, PacketAction>();

            #region Client Packets
            PacketsDb.Add(0x000A, CS_RegisterClient);
            PacketsDb.Add(0x000B, CS_UnregisterClient);
            PacketsDb.Add(0x00AA, US_RegisterUpdater);
            PacketsDb.Add(0x00BB, US_UnregisterUpdater);
            PacketsDb.Add(0x000C, CS_InitializeHeartbeat);
            PacketsDb.Add(0x0001, CS_RequestUpdateDateTime);
            PacketsDb.Add(0x0002, CS_RequestSelfUpdateRequired);
            PacketsDb.Add(0x0008, CS_RequestUpdatesDisabled);
            PacketsDb.Add(0x0010, CS_RequestDataUpdateIndex);
            PacketsDb.Add(0x0110, CS_RequestResourceUpdateIndex);
            PacketsDb.Add(0x0040, CS_RequestSendType);
            PacketsDb.Add(0x0041, CS_RequestFileSize);
            PacketsDb.Add(0x0042, CS_RequestFileTransfer);
            PacketsDb.Add(0x0030, CS_RequestArguments);
            PacketsDb.Add(0x0100, CS_RequestValidation);
            PacketsDb.Add(0x00DC, CS_RequestDisconnect);
            PacketsDb.Add(0x0999, CS_RequestAuthenticationType);
            PacketsDb.Add(0x9999, CS_RequestDesKey);
            #endregion

            #region Updater Packets
            PacketsDb.Add(0x1000, US_RequestLauncherInfo);
            PacketsDb.Add(0x1100, US_RequestLauncherDownload);
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
                Output.Write(new Message() { Text = string.Format("Unknown packet Id: {0}", stream.GetId()), AddBreak = true });
                return;
            }

            currentPacketId = stream.GetId();

            // Calls this packet parsing function
            Task.Run(() => { PacketsDb[currentPacketId].Invoke(client, stream); });
        }

        #region Client-Server Packets (CS)

        private void CS_RegisterClient(Client client, PacketStream stream)
        {
            Statistics.UpdateClientCount(true);
            ClientManager.Instance.Send(client, new PacketStream(0x00AA));
        }

        private void CS_UnregisterClient(Client client, PacketStream stream) { Statistics.UpdateClientCount(false); }

        private void CS_InitializeHeartbeat(Client client, PacketStream stream)
        {
            throw new NotImplementedException();
        }

        private void US_RegisterUpdater(Client client, PacketStream stream)
        {
            Statistics.UpdateUpdaterCount(true);
            ClientManager.Instance.Send(client, new PacketStream(0x0AAA));
        }

        private void US_UnregisterUpdater(Client client, PacketStream stream) { Statistics.UpdateUpdaterCount(false); }

        private void CS_RequestDesKey(Client client, PacketStream stream) { UserHandler.Instance.OnUserRequestDesKey(client); }

        private void CS_RequestAuthenticationType(Client client, PacketStream stream) { UserHandler.Instance.OnAuthenticationTypeRequest(client); }

        private void CS_RequestValidation(Client client, PacketStream stream)
        {
            int userLength = stream.ReadInt32();
            string username = DesCipher.Decrypt(stream.ReadBytes(userLength));
            int passLength = stream.ReadInt32();
            string password = DesCipher.Decrypt(stream.ReadBytes(passLength));
            string fingerprint = stream.ReadString();

            UserHandler.Instance.OnValidateUser(client, username, password, fingerprint);
        }

        private void CS_RequestUpdatesDisabled(Client client, PacketStream stream) { UpdateHandler.Instance.OnUserRequestUpdatesEnabled(client); }

        private void CS_RequestUpdateDateTime(Client client, PacketStream stream) { UpdateHandler.Instance.OnUserRequestUpdateDateTime(client); }

        private void CS_RequestSelfUpdateRequired(Client client, PacketStream stream) { UpdateHandler.Instance.OnUserRequestSelfUpdateRequired(client, stream.ReadString()); }

        /// <summary>
        /// Client wants the file list
        /// </summary>
        /// <param name="client">the client</param>
        /// <param name="stream">data</param>
        private void CS_RequestDataUpdateIndex(Client client, PacketStream stream) { UpdateHandler.Instance.OnRequestDataUpdateIndex(client); }

        private void CS_RequestResourceUpdateIndex(Client client, PacketStream stream) { UpdateHandler.Instance.OnRequestResourceUpdateIndex(client); }

        private void CS_RequestSendType(Client client, PacketStream stream)
        {
            if (debug) { Output.Write(new Message() { Text = string.Format("Client [{0}] requested send.type", client.Id), AddBreak = true }); }

            PacketStream outStream = new PacketStream(0x0040);
            outStream.WriteInt32(OPT.GetInt("send.type"));
            ClientManager.Instance.Send(client, outStream);
        }

        private void CS_RequestFileSize(Client client, PacketStream stream) { UpdateHandler.Instance.OnRequestFileInfo(client, stream.ReadString()); }

        private void CS_RequestFileTransfer(Client client, PacketStream stream) { UpdateHandler.Instance.OnUserRequestFile(client, stream.ReadString()); }

        private void CS_RequestArguments(Client client, PacketStream stream) { UserHandler.Instance.OnUserRequestArguments(client); }

        private void CS_RequestDisconnect(Client client, PacketStream stream)
        {
            ClientManager.Instance.Remove(client);
            Statistics.UpdateClientCount(false);
            SC_SendOkDisconnect(client);
            client.ClSocket.Close();
            if (debug) { Output.Write(new Message() { Text = string.Format("Client [{0}] disconnected!", client.Id), AddBreak = true }); }
        }

        #endregion

        #region Server-Client Packets (SC)

        internal void SC_SendUpdateTime(Client client, string DateTime)
        {
            PacketStream stream = new PacketStream(0x000A);
            stream.WriteString(DateTime, DateTime.Length + 1);
            ClientManager.Instance.Send(client, stream);
        }

        internal void SC_SendUpdatesDisabled(Client client, int updatingDisabled)
        {
            PacketStream stream = new PacketStream(0x0008);
            stream.WriteInt32(updatingDisabled);
            ClientManager.Instance.Send(client, stream);
        }

        internal void SC_SendSelfUpdateRequired(Client client, bool updateRequired)
        {
            PacketStream stream = new PacketStream(0x000B);
            stream.WriteBool(updateRequired);
            ClientManager.Instance.Send(client, stream);
        }

        internal void SC_SendSelfUpdate(Client client, string tmpName)
        {
            PacketStream stream = new PacketStream(0x000B);
            stream.WriteString(tmpName, tmpName.Length + 1);
            ClientManager.Instance.Send(client, stream);
        }

        internal void SC_SendDataEntry(Client client, string fileName, string fileHash)
        {
            PacketStream stream = new PacketStream(0x0011);

            stream.WriteString(fileName, fileName.Length + 1);
            stream.WriteString(fileHash, fileHash.Length + 1);

            ClientManager.Instance.Send(client, stream);
        }

        internal void SC_SendResourceEntry(Client client, string fileName, string fileHash, bool isDelete)
        {
            PacketStream stream = new PacketStream(0x0111);

            stream.WriteString(fileName, fileName.Length + 1);
            stream.WriteString(fileHash, fileHash.Length + 1);
            stream.WriteBool(isDelete);

            ClientManager.Instance.Send(client, stream);
        }

        internal void SC_SendDataIndexEOF(Client client) { ClientManager.Instance.Send(client, new PacketStream(0x0012)); }

        internal void SC_SendResourceIndexEOF(Client client) { ClientManager.Instance.Send(client, new PacketStream(0x0112)); }

        internal void SC_SendOTP(Client client, string otp)
        {
            PacketStream stream = new PacketStream(0x0013);

            byte[] encOTP = DesCipher.Encrypt(otp);
            stream.WriteInt32(encOTP.Length);
            stream.WriteBytes(encOTP);

            ClientManager.Instance.Send(client, stream);
        }

        internal void SC_SendBanStatus(Client client, int banType)
        {
            PacketStream stream = new PacketStream(0x0014);
            stream.WriteInt32(banType);

            ClientManager.Instance.Send(client, stream);
        }

        internal void SC_SendStartType(Client client, int startType)
        {
            PacketStream stream = new PacketStream(0x0032);
            stream.WriteInt32(startType);
            ClientManager.Instance.Send(client, stream);
        }

        internal void SC_SendAccountNull(Client client) { ClientManager.Instance.Send(client, new PacketStream(0x0015)); }

        internal void SC_SendFileInfo(Client client, string fileName, long fileSize)
        {
            PacketStream stream = new PacketStream(0x0041);
            stream.WriteString(fileName, fileName.Length + 1);
            stream.WriteInt64(fileSize);
            ClientManager.Instance.Send(client, stream);
        }

        internal void SC_SendFile(Client client, string filePath)
        {
            int chunkSize = 64000;

            string fileName = Path.GetFileName(filePath);

            byte[] buffer = (File.Exists(filePath)) ? File.ReadAllBytes(filePath) : null;
            if (buffer.Length > 0)
            {
                // Just incase file is smaller than initial chunkSize
                chunkSize = Math.Min(64000, buffer.Length);

                int i = 0;

                while (true)
                {
                    if (i == buffer.Length) { break; }

                    PacketStream fStream = new PacketStream(0x0042);

                    fStream.Flush();

                    fStream.WriteInt32(chunkSize);
                    fStream.WriteInt32(i);

                    byte[] tempBuffer = new byte[chunkSize];
                    Array.Copy(buffer, i, tempBuffer, 0, chunkSize);

                    fStream.WriteBytes(tempBuffer);

                    ClientManager.Instance.Send(client, fStream);

                    // Increase the index position
                    i = i + chunkSize;

                    // Refresh the chunkSize
                    chunkSize = Math.Min(64000, buffer.Length - i);
                }

                SC_SendFileEOF(client, fileName);
            }
        }

        internal void SC_SendFileEOF(Client client, string fileName) 
        {
            PacketStream stream = new PacketStream(0x0043);
            stream.WriteString(fileName);
            ClientManager.Instance.Send(client, stream);
        }

        internal void SC_SendArguments(Client client, string arguments, int startType, bool isMaintenance)
        {
            PacketStream stream = new PacketStream(0x0031);

            byte[] argEncrypt = DesCipher.Encrypt(arguments);
            stream.WriteInt32(argEncrypt.Length);
            stream.WriteBytes(argEncrypt);
            stream.WriteInt32(startType);
            stream.WriteBool(isMaintenance);

            ClientManager.Instance.Send(client, stream);
        }

        internal void SC_SendAuthenticationType(Client client, int type)
        {
            PacketStream stream = new PacketStream(0x0999);
            stream.WriteInt32(type);
            ClientManager.Instance.Send(client, stream);
        }

        internal void SC_SendOkDisconnect(Client client) { ClientManager.Instance.Send(client, new PacketStream(0x0099)); }

        internal void SC_SendWait(Client client, ushort currentPacketId, int period)
        {
            PacketStream stream = new PacketStream(0x0050);
            stream.WriteUInt16(currentPacketId);
            stream.WriteInt32(period);
            ClientManager.Instance.Send(client, stream);
        }

        #endregion

        #region Updater-Server Packets (US)

        private void US_RequestLauncherInfo(Client client, PacketStream stream) { UpdateHandler.Instance.OnRequestLauncherInfo(client); }

        private void US_RequestLauncherDownload(Client client, PacketStream stream) { UpdateHandler.Instance.OnRequestLauncherDownload(client); }

        #endregion

        #region Server-Updater Packets (SU)

        internal void SU_SendLauncherInfo(Client client, long length, string hash)
        {
            PacketStream stream = new PacketStream(0x1001);
            stream.WriteInt64(length);
            byte[] hashEncrypt = DesCipher.Encrypt(hash);
            stream.WriteInt32(hashEncrypt.Length);
            stream.WriteBytes(hashEncrypt);
            ClientManager.Instance.Send(client, stream);
        }

        internal void SU_SendLauncher(Client client, string filePath)
        {
            int chunkSize = 64000;

            byte[] buffer = File.ReadAllBytes(filePath);
            if (buffer.Length > 0)
            {
                // Just incase file is smaller than initial chunkSize
                chunkSize = Math.Min(64000, buffer.Length);

                int i = 0;

                while (true)
                {
                    if (i == buffer.Length) { break; }

                    PacketStream fStream = new PacketStream(0x1101);

                    fStream.Flush();

                    fStream.WriteInt32(chunkSize);
                    fStream.WriteInt32(i);

                    byte[] tempBuffer = new byte[chunkSize];
                    Array.Copy(buffer, i, tempBuffer, 0, chunkSize);

                    fStream.WriteBytes(tempBuffer);

                    ClientManager.Instance.Send(client, fStream);

                    // Increase the index position
                    i = i + chunkSize;

                    // Refresh the chunkSize
                    chunkSize = Math.Min(64000, buffer.Length - i);
                }

                SU_SendLauncherEOF(client);
            }
        }

        private void SU_SendLauncherEOF(Client client) { ClientManager.Instance.Send(client, new PacketStream(0x1102)); }

        #endregion
    }
}
