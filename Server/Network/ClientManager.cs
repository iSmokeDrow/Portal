using Server.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Network
{
    public class ClientManager
    {
        public static readonly ClientManager Instance = new ClientManager();

        /// <summary>
        /// Initializes the client listener
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            Socket listener = new Socket(SocketType.Stream, ProtocolType.Tcp);

            try
            {
                IPAddress ip;
                if (!IPAddress.TryParse(OPT.SettingsList["io.ip"], out ip))
                {
                    Console.WriteLine("Failed to parse Server IP ({0})", OPT.SettingsList["io.ip"]);
                    return false;
                }
                listener.Bind(new IPEndPoint(ip, int.Parse(OPT.SettingsList["io.port"])));
                listener.Listen(100);
                listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("At ClientManager.Start()");

                listener.Close();
                return false;
            }

            return true;
        }

        /// <summary>
		/// Called when an entire packet is received
		/// </summary>
		/// <param name="client">the client</param>
		/// <param name="packetStream">the packet</param>
		private void PacketReceived(Client client, PacketStream packetStream)
        {
            ClientPackets.Instance.PacketReceived(client, packetStream);
        }

        /// <summary>
        /// Sends a packet to a client
        /// </summary>
        /// <param name="target">the client which will receive the packet</param>
        /// <param name="packet">packet data</param>
        public void Send(Client target, PacketStream packet)
        {
            byte[] data = packet.GetPacket().ToArray();

            target.ClSocket.BeginSend(
                target.OutCipher.DoCipher(ref data),
                0,
                data.Length,
                0,
                new AsyncCallback(SendCallback),
                target
            );
        }

        #region Internal
        /// <summary>
        /// Triggered when a client tries to connect
        /// </summary>
        /// <param name="ar"></param>
        private void AcceptCallback(IAsyncResult ar)
        {
            Socket listener = (Socket)ar.AsyncState;
            Socket socket = listener.EndAccept(ar);

            // Starts to accept another connection
            listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

            Client client = new Client(socket);

            socket.BeginReceive(
                client.Buffer, 0, PacketStream.MaxBuffer, SocketFlags.None,
                new AsyncCallback(ReadCallback), client
            );
        }

        /// <summary>
        /// Receives data and split them into packets
        /// </summary>
        /// <param name="ar"></param>
        private void ReadCallback(IAsyncResult ar)
        {
            Client client = (Client)ar.AsyncState;

            try
            {
                int bytesRead = client.ClSocket.EndReceive(ar);
                if (bytesRead > 0)
                {
                    int curOffset = 0;
                    int bytesToRead = 0;
                    byte[] decode = client.InCipher.DoCipher(ref client.Buffer, bytesRead);

                    do
                    {

                        if (client.PacketSize == 0)
                        {
                            if (client.Offset + bytesRead > 3)
                            {
                                bytesToRead = (4 - client.Offset);
                                client.Data.Write(decode, curOffset, bytesToRead);
                                curOffset += bytesToRead;
                                client.Offset = bytesToRead;
                                client.PacketSize = BitConverter.ToInt32(client.Data.ReadBytes(4, 0, true), 0);
                            }
                            else
                            {
                                client.Data.Write(decode, 0, bytesRead);
                                client.Offset += bytesRead;
                                curOffset += bytesRead;
                            }
                        }
                        else
                        {
                            int needBytes = client.PacketSize - client.Offset;

                            // If there's enough bytes to complete this packet
                            if (needBytes <= (bytesRead - curOffset))
                            {
                                client.Data.Write(decode, curOffset, needBytes);
                                curOffset += needBytes;
                                // Packet is done, send to server to be parsed
                                // and continue.
                                PacketReceived(client, client.Data);
                                // Do needed clean up to start a new packet
                                client.Data = new PacketStream();
                                client.PacketSize = 0;
                                client.Offset = 0;
                            }
                            else
                            {
                                bytesToRead = (bytesRead - curOffset);
                                client.Data.Write(decode, curOffset, bytesToRead);
                                client.Offset += bytesToRead;
                                curOffset += bytesToRead;
                            }
                        }
                    } while (bytesRead - 1 > curOffset);

                    client.ClSocket.BeginReceive(
                        client.Buffer, 0, PacketStream.MaxBuffer, SocketFlags.None,
                        new AsyncCallback(ReadCallback), client
                    );

                }
                else
                {
                    Console.WriteLine("Client disconected.");
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Sends a packet to a client
        /// </summary>
        /// <param name="ar"></param>
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Client client = (Client)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = client.ClSocket.EndSend(ar);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to send packet to client.");
            }
        }
        #endregion

    }
}
