using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.Network
{
    public class ServerManager
    {
        /// <summary>
        /// Holds the instance to server manager
        /// </summary>
        public static readonly ServerManager Instance = new ServerManager();

        Server _Server;

        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Starts the connection with server
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool Start(string ip, short port)
        {
            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            try
            {
                // Parse string IP Address
                IPAddress ipAddress;
                if (!IPAddress.TryParse(ip, out ipAddress))
                {
                    this.ErrorMessage = String.Format("Failed to parse Server IP ({0})", ip);
                    return false;
                }
                // Connect
                //socket.BeginConnect(new IPEndPoint(ipAddress, port), new AsyncCallback(ConnectCallback), socket);
                socket.Connect(new IPEndPoint(ipAddress, port));

                // Initializes a new instance of Server
                this._Server = new Server(socket);

                // Starts to receive data
                socket.BeginReceive(_Server.Buffer, 0, PacketStream.MaxBuffer, SocketFlags.None, new AsyncCallback(ReadCallback), null);
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;

                socket.Close();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Called when an entire packet is received
        /// </summary>
        /// <param name="packetStream"></param>
        private void PacketReceived(PacketStream packetStream)
        {
            // Dumps packet data and process
            ServerPackets.Instance.PacketReceived(packetStream);
        }

        /// <summary>
        /// Sends a packet to a server
        /// </summary>
        /// <param name="packet"></param>
        public void Send(PacketStream packet)
        {
            // Completes the packet and retrieve it
            byte[] data = packet.GetPacket().ToArray();

            // Dump and send
            _Server.ClSocket.BeginSend(
                _Server.OutCipher.DoCipher(ref data),
                0,
                data.Length,
                SocketFlags.None,
                new AsyncCallback(SendCallback),
                null
               );
        }

        #region Internal

        /// <summary>
        /// Triggered when it connects to Server
        /// </summary>
        /// <param name="ar"></param>
        private void ConnectCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            if (!socket.Connected)
            {
                this.ErrorMessage = "Could not stabilish a connection to Launcher Server.";
                return;
            }
            socket.EndConnect(ar);


        }

        /// <summary>
        /// Receives data and split them into packets
        /// </summary>
        /// <param name="ar"></param>
        private void ReadCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieves the ammount of bytes received
                int bytesRead = _Server.ClSocket.EndReceive(ar);
                if (bytesRead > 0)
                {
                    // The offset of the current buffer
                    int curOffset = 0;
                    // Bytes to use in the next write
                    int bytesToRead = 0;

                    byte[] decode = _Server.InCipher.DoCipher(ref _Server.Buffer, bytesRead);
                    do
                    { // While there's data to read

                        if (_Server.PacketSize == 0)
                        { // If we don't have the packet size yet

                            if (_Server.Offset + bytesRead > 3)
                            { // If we can retrieve the packet size with the received data
                              // If yes, we read remaining bytes until we get the packet size
                                bytesToRead = (4 - _Server.Offset);
                                _Server.Data.Write(decode, curOffset, bytesToRead);
                                curOffset += bytesToRead;
                                _Server.Offset = bytesToRead;
                                _Server.PacketSize = BitConverter.ToInt32(_Server.Data.ReadBytes(4, 0, true), 0);
                            }
                            else
                            {
                                // If not, we read everything.
                                _Server.Data.Write(decode, 0, bytesRead);
                                _Server.Offset += bytesRead;
                                curOffset += bytesRead;
                            }
                        }
                        else
                        { // If we have packet size
                          // How many bytes we need to complete this packet
                            int needBytes = _Server.PacketSize - _Server.Offset;

                            // If there's enough bytes to complete this packet
                            if (needBytes <= (bytesRead - curOffset))
                            {
                                _Server.Data.Write(decode, curOffset, needBytes);
                                curOffset += needBytes;
                                // Packet is done, send to server to be parsed
                                // and continue.
                                PacketReceived(_Server.Data);
                                // Do needed clean up to start a new packet
                                _Server.Data = new PacketStream();
                                _Server.PacketSize = 0;
                                _Server.Offset = 0;
                            }
                            else
                            {
                                bytesToRead = (bytesRead - curOffset);
                                _Server.Data.Write(decode, curOffset, bytesToRead);
                                _Server.Offset += bytesToRead;
                                curOffset += bytesToRead;
                            }
                        }
                    } while (bytesRead - 1 > curOffset);

                    // Starts to receive more data
                    _Server.ClSocket.BeginReceive(
                        _Server.Buffer,
                        0,
                        PacketStream.MaxBuffer,
                        SocketFlags.None,
                        new AsyncCallback(ReadCallback),
                        null
                    );
                }
                else
                {
                    this.ErrorMessage = "Connection to server lost.";
                    _Server.ClSocket.Close();
                    return;
                }
            }
            catch (SocketException e)
            {
                // 10054 : Socket closed, not an error
                if (!(e.ErrorCode == 10054))
                    this.ErrorMessage = e.Message;

                this.ErrorMessage += "Connection to server lost.";
                _Server.ClSocket.Close();
            }
            catch (Exception e)
            {
                this.ErrorMessage = e.Message;
                this.ErrorMessage += "Connection to server lost.";
                _Server.ClSocket.Close();
            }
        }

        internal void Close()
        {
            _Server.ClSocket.Close();
        }

        /// <summary>
        /// Sends a packet to a game-server
        /// </summary>
        /// <param name="ar"></param>
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Complete sending the data to the remote device.
                int bytesSent = _Server.ClSocket.EndSend(ar);
            }
            catch (Exception)
            {
                this.ErrorMessage = "Failed to send data to server.";
            }
        }
        #endregion
    }

}