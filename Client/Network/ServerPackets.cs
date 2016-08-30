using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Client.Functions;
using System.Text;

namespace Client.Network
{
    public class ServerPackets
    {
        protected static XDes des;

        public static readonly ServerPackets Instance = new ServerPackets();

        private delegate void PacketAction(PacketStream stream);

        private Dictionary<ushort, PacketAction> PacketsDb;
        private XDes DesCipher = new XDes(Program.DesKey);

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
            PacketsDb.Add(0x0031, SC_Arguments);
            PacketsDb.Add(0x0099, SC_Disconnect);
            PacketsDb.Add(0x9999, SC_DesKey);
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

            UpdateHandler.Instance.OnUpdateIndexReceived(name, fileHash, isLegacy);
        }

        private void SC_UpdateIndexEnd(PacketStream stream)
        {
            UpdateHandler.Instance.OnUpdateIndexEnd(stream.ReadInt32());
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
            string otpHash = DesCipher.Decrypt(stream.ReadBytes(otpLength));

            GUI.Instance.OnValidationResultReceived(otpHash);
        }

        private void SC_Arguments(PacketStream stream)
        {
            int len = stream.ReadInt32();
            byte[] arguments = stream.ReadBytes(len);

            GUI.Instance.OnArgumentsReceived(DesCipher.Decrypt(arguments));
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

        internal void  RequestUserValidation(string desKey, string name, string password, string fingerprint)
        {
            des = new XDes(desKey);
            PacketStream stream = new PacketStream(0x0100);
            byte[] encName = des.Encrypt(name);
            byte[] encPass = des.Encrypt(password);
            stream.WriteInt32(encName.Length);
            stream.WriteBytes(encName);
            stream.WriteInt32(encPass.Length);
            stream.WriteBytes(encPass);
            stream.WriteString(fingerprint);
            ServerManager.Instance.Send(stream);
        }

        internal void RequestUpdateDateTime()
        {
            PacketStream stream = new PacketStream(0x0001);
            ServerManager.Instance.Send(stream);
            
        }

        internal void RequestSelfUpdate(string hash)
        {
            PacketStream stream = new PacketStream(0x0002);
            stream.WriteString(hash, hash.Length + 1);
            ServerManager.Instance.Send(stream);
        }

        internal void RequestUpdater(string hash)
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
        internal void RequestUpdateIndex(int type)
        {
            PacketStream stream = new PacketStream(0x0010);
            stream.WriteInt32(type);
            ServerManager.Instance.Send(stream);
        }

        internal void RequestArguments(string username)
        {
            PacketStream stream = new PacketStream(0x0030);
            stream.WriteString(username, username.Length + 1);
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
