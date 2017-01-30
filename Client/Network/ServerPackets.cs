using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Client.Functions;
using System.Text;

namespace Client.Network
{
    public class ServerPackets
    {
        public static readonly ServerPackets Instance = new ServerPackets();

        private delegate void PacketAction(PacketStream stream);

        private Dictionary<ushort, PacketAction> PacketsDb;
        private XDes desCipher = new XDes(Program.DesKey);

        private byte[] tempBuffer;

        internal void SetDES(string key) { desCipher = new XDes(key); }

        public ServerPackets()
        {
            // Loads PacketsDb
            PacketsDb = new Dictionary<ushort, PacketAction>();

            #region Packets
            PacketsDb.Add(0x000A, SC_UpdateDateTime);
            PacketsDb.Add(0x000B, SC_UpdateSelfUpdate);
            PacketsDb.Add(0x001E, SC_UpdateUpdater);
            PacketsDb.Add(0x0011, SC_UpdateIndex);
            PacketsDb.Add(0x0012, SC_UpdateIndexEnd);
            PacketsDb.Add(0x0013, SC_UserValidated);
            PacketsDb.Add(0x0014, SC_UserBanned);
            PacketsDb.Add(0x0015, SC_AccountNull);
            PacketsDb.Add(0x0040, SC_ReceiveSendType);
            PacketsDb.Add(0x0041, SC_ReceiveFileInfo);
            PacketsDb.Add(0x0042, SC_ReceiveFile);
            PacketsDb.Add(0x0043, SC_ReceiveEOF);
            PacketsDb.Add(0x0031, SC_ReceiveArguments);
            PacketsDb.Add(0x0099, SC_Disconnect);
            PacketsDb.Add(0x0999, SC_AuthenticationType);
            PacketsDb.Add(0x9999, SC_DesKey);
            #endregion
        }

        private void SC_AuthenticationType(PacketStream stream) { GUI.Instance.OnAuthenticationTypeReceived(stream.ReadInt32()); }

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

        private void SC_DesKey(PacketStream stream) { GUI.Instance.OnDesKeyReceived(stream.ReadString()); }

        private void SC_UpdateDateTime(PacketStream stream)
        {
            string dateString = stream.ReadString();
            DateTime time = DateTime.Parse(dateString, CultureInfo.InvariantCulture);

            UpdateHandler.Instance.OnUpdateDateTimeReceived(time);
        }

        private void SC_UpdateSelfUpdate(PacketStream stream)
        {
            UpdateHandler.Instance.ExecuteSelfUpdate(stream.ReadString());
        }

        private void SC_UpdateUpdater(PacketStream stream)
        {
            UpdateHandler.Instance.ExecuteUpdaterUpdate(stream.ReadString());
        }

        /// <summary>
        /// UpdateIndex entry received
        /// </summary>
        /// <param name="stream"></param>
        private void SC_UpdateIndex(PacketStream stream)
        {
            string name = stream.ReadString();
            string fileHash = stream.ReadString();
            bool isLegacy = stream.ReadBool();
            bool isDelete = stream.ReadBool();

            UpdateHandler.Instance.OnUpdateIndexReceived(name, fileHash, isLegacy);
        }

        private void SC_UpdateIndexEnd(PacketStream stream)
        {
            UpdateHandler.Instance.OnUpdateIndexEnd();
        }

        private void SC_UserBanned(PacketStream stream)
        {
            GUI.Instance.OnUserBannedReceived(stream.ReadInt32());
        }

        private void SC_AccountNull(PacketStream stream)
        {
            GUI.Instance.OnUserAccountNullReceived();
        }

        private void SC_UserValidated(PacketStream stream)
        {
            int otpLength = stream.ReadInt32();
            string otpHash = desCipher.Decrypt(stream.ReadBytes(otpLength));

            GUI.Instance.OnValidationResultReceived(otpHash);
        }

        private void SC_ReceiveSendType(PacketStream stream)
        {
            UpdateHandler.Instance.OnSendTypeReceived(stream.ReadInt32());
        }

        //TODO: Move tempBuffer to UpdateHandler?
        private void SC_ReceiveFileInfo(PacketStream stream)
        {
            string tmpName = stream.ReadString();
            int fileSize = stream.ReadInt32();
            if (fileSize > 0)
            {
                tempBuffer = new byte[fileSize];
                GUI.Instance.UpdateProgressMaximum(1, fileSize);
                GUI.Instance.UpdateProgressValue(1, 0);
                UpdateHandler.Instance.OnFileInfoReceived(tmpName);
            }
        }

        private void SC_ReceiveFile(PacketStream stream)
        {
            int chunkSize = stream.ReadInt32();
            int offset = stream.ReadInt32();
            byte[] chunks = stream.ReadBytes(chunkSize);
            Buffer.BlockCopy(chunks, 0, tempBuffer, offset, chunkSize);
            GUI.Instance.UpdateProgressValue(1, offset);
        }

        private void SC_ReceiveEOF(PacketStream stream)
        {
            string fileName = stream.ReadString();
            string filePath = String.Format(@"{0}\Downloads\{1}", Directory.GetCurrentDirectory(), fileName);

            if (File.Exists(filePath)) { File.Delete(filePath); }

            GUI.Instance.UpdateProgressValue(1, tempBuffer.Length);

            using (FileStream fs = new FileStream(filePath, FileMode.Create)) { fs.Write(tempBuffer, 0, tempBuffer.Length); }

            UpdateHandler.Instance.OnFileTransfered(fileName);
        }

        private void SC_ReceiveArguments(PacketStream stream)
        {
            int len = stream.ReadInt32();
            byte[] arguments = stream.ReadBytes(len);

            GUI.Instance.OnArgumentsReceived(desCipher.Decrypt(arguments));
        }

        private void SC_Disconnect(PacketStream stream)
        {
            GUI.Instance.UpdateStatus(0, "Disconnection complete!");
            GUI.Instance.Invoke(new System.Windows.Forms.MethodInvoker(delegate { GUI.Instance.Close(); }));
        }

        #endregion

        #region Client-Server Packets (CS)

        internal void RequestDESKey()
        {
            PacketStream stream = new PacketStream(0x9999);
            ServerManager.Instance.Send(stream);
        }

        internal void CS_RequestAuthenticationType() { ServerManager.Instance.Send(new PacketStream(0x0999)); }

        internal void  CS_ValidateUser(string name, string password, string fingerprint)
        {
            PacketStream stream = new PacketStream(0x0100);
            byte[] encName = desCipher.Encrypt(name);
            byte[] encPass = desCipher.Encrypt(password);
            stream.WriteInt32(encName.Length);
            stream.WriteBytes(encName);
            stream.WriteInt32(encPass.Length);
            stream.WriteBytes(encPass);
            stream.WriteString(fingerprint);
            ServerManager.Instance.Send(stream);
        }

        internal void CS_GetUpdateDateTime()
        {
            PacketStream stream = new PacketStream(0x0001);
            ServerManager.Instance.Send(stream);
            
        }

        internal void CS_RequestSelfUpdate(string hash)
        {
            PacketStream stream = new PacketStream(0x0002);
            stream.WriteString(hash, hash.Length + 1);
            ServerManager.Instance.Send(stream);
        }

        internal void CS_RequestSelfUpdater(string hash)
        {
            PacketStream stream = new PacketStream(0x0003);
            if (hash != null)
            {
                stream.WriteString(hash, hash.Length + 1);
            }
            else
            {
                string tmp = "NO_HASH";
                stream.WriteString(tmp, tmp.Length + 1);
            }
            ServerManager.Instance.Send(stream);
        }

        /// <summary>
        /// Requests the list of files
        /// </summary>
        internal void CS_RequestUpdateIndex()
        {
            PacketStream stream = new PacketStream(0x0010);
            ServerManager.Instance.Send(stream);
        }

        internal void CS_RequestTransferType() { ServerManager.Instance.Send(new PacketStream(0x0040)); }

        internal void CS_RequestFileSize(string fileName)
        {
            PacketStream stream = new PacketStream(0x0041);
            stream.WriteString(fileName);
            ServerManager.Instance.Send(stream);
        }

        internal void CS_RequestFileTransfer(string fileName)
        {
            PacketStream stream = new PacketStream(0x0042);
            stream.WriteString(fileName);
            ServerManager.Instance.Send(stream);
        }

        internal void CS_RequestArguments()
        {
            PacketStream stream = new PacketStream(0x0030);
            ServerManager.Instance.Send(stream);
        }

        internal void RequestDisconnect()
        {
            PacketStream stream = new PacketStream(0x00DC);
            ServerManager.Instance.Send(stream);
        }

        #endregion
    }
}
