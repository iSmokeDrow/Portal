using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Client
{
    public class Network : IDisposable
    {
        internal readonly TcpClient client;
        internal NetworkStream stream;

        public Network()
        {
            client = new TcpClient();
        }

        public void Start(string ip, int port)
        {
            client.Connect(IPAddress.Parse(ip), port);
            if (client.Connected)
            {
                stream = client.GetStream();
            }
        }

        public bool Login(string username, string password, string fingerprint)
        {
            byte[] buffer = Encoding.Default.GetBytes(string.Format("U_LOGIN|{0}|{1}|{2}", username, password, fingerprint));
            if (stream != null)
            {
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();

                Thread.Sleep(100);

                if (ReadString() == "U_VALID") { return true; }
            }

            return false;
        }

        public string ReadString()
        {
            StringBuilder sb = new StringBuilder();

            while (true)
            {
                if (stream.DataAvailable)
                {
                    int read = stream.ReadByte();
                    if (read > 0)
                    {

                    }
                }
            }
        }

        public void ReadByte()
        {

        }

        public void ReadByteArray()
        {

        }

        public void Dispose()
        {

        }
    }
}
