using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Client
    {
        private static List<int> idPool = new List<int>();
        private static int lastId = 0;

        public static int GetNewUserId()
        {
            int newId;
            if (idPool.Count > 0)
            {
                newId = idPool[0];
                idPool.RemoveAt(0);
            }
            else
            {
                newId = ++lastId;
            }
            return newId;
        }

        public int Id { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public Socket ClSocket { get; set; }
        public byte[] Buffer;
        public PacketStream Data { get; set; }
        public int PacketSize { get; set; }
        public int Offset { get; set; }
        public XRC4Cipher InCipher { get; set; }
        public XRC4Cipher OutCipher { get; set; }

        public List<string> filesInUse = new List<string>();

        public Client(Socket socket)
        {
            this.ClSocket = socket;
            this.Ip = ((IPEndPoint)socket.RemoteEndPoint).Address.ToString();
            this.Port = ((IPEndPoint)socket.RemoteEndPoint).Port;
            this.Buffer = new byte[PacketStream.MaxBuffer];
            this.Data = new PacketStream();
            this.InCipher = new XRC4Cipher(Program.RC4Key);
            this.OutCipher = new XRC4Cipher(Program.RC4Key);
        }

        internal void Dispose()
        {
            idPool.Add(this.Id);
        }
    }
}
