using Updater.Network;
using System.Net.Sockets;

namespace Updater.Structures
{
    public class Server
    {
        public Socket ClSocket { get; set; }
        public byte[] Buffer;
        public PacketStream Data { get; set; }
        public int PacketSize { get; set; }
        public int Offset { get; set; }
        public XRC4Cipher InCipher { get; set; }
        public XRC4Cipher OutCipher { get; set; }

        public Server(Socket socket)
        {
            this.ClSocket = socket;
            this.Buffer = new byte[PacketStream.MaxBuffer];
            this.Data = new PacketStream();
            this.InCipher = new XRC4Cipher(Program.RC4Key);
            this.OutCipher = new XRC4Cipher(Program.RC4Key);
        }
    }
}
