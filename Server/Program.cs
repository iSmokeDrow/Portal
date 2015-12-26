using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Server.Functions;
using Server.Network;

namespace Server
{
    class Program
    {
        /// <summary>
        /// Server and client must have the same RC4 key
        /// it's used to encrypt packet data
        /// </summary>
        public static string RC4Key = "password1";

        /// <summary>
        /// Server and client must have the same DES Key
        /// It's used to encrypt passwords
        /// </summary>
        public static string DesKey = "password2";

        static XDes DesCipher;
        
        static int clientCount = 0;
        
        static void Main(string[] args)
        {
            OPT.LoadSettings();
            DesCipher = new XDes(DesKey);

            Console.Write("Initializing client listener... ");
            if (ClientManager.Instance.Start())
                Console.WriteLine("[OK]");

            Console.ReadLine();
        }

        public static void OnUserLogin(Client client, string userId, byte[] pPassword, string fingerPrint)
        {
            string password = DesCipher.Decrypt(pPassword).Trim('\0');
            clientCount++;
            Console.WriteLine("Client is trying to login (UserID: {0} ; Password: {1} ; FingerPrint: {2})", userId, password, fingerPrint);
            Console.WriteLine("Now there're {0} users connected to the launcher server.", clientCount);
            // TODO : Validation

            ClientPackets.Instance.LoginResult(client, 1); // Success
        }

        public static void OnUserLogout(Client client)
        {
            clientCount++;
            Console.WriteLine("User disconnected");
        }
    }
}
