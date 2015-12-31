using System;
using System.Collections.Generic;
using Server.Functions;
using Server.Network;
using System.IO;

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

        static internal int clientCount
        {
            get
            {
                if (clientList != null) { return clientList.Count; }
                else { return 0; }
            }
        }
        static internal Dictionary<int, Client> clientList;
        
        static void Main(string[] args)
        {
            OPT.LoadSettings();
            DesCipher = new XDes(DesKey);

            clientList = new Dictionary<int, Client>();

            Console.Write("Initializing client listener... ");
            if (ClientManager.Instance.Start()) { Console.WriteLine("[OK]"); }
                
            Console.ReadLine();
        }

        public static void OnUserLogin(Client client, string userId, byte[] pPassword, string fingerPrint)
        {
            string username = userId;
            string password = DesCipher.Decrypt(pPassword);
            string fingerprint = fingerPrint;
#if DEBUG
            Console.WriteLine("User login (UserID: {0} ; Password: {1} ; FingerPrint: {2})", userId, password, fingerPrint);
#endif

            switch (User.ValidateCredentials(username, password, fingerprint))
            {
                case 0:
                    client.Id = Client.GetNewUserId();
                    clientList.Add(client.Id, client);
                    ClientPackets.Instance.LoginResult(client, 0); // Success
                    break;

                case 1:
                    ClientPackets.Instance.LoginResult(client, 1);
                    break;

                case 2:
                    ClientPackets.Instance.LoginResult(client, 2);
                    break;

                case 3:
                    ClientPackets.Instance.LoginResult(client, 3);
                    break;

                case 4:
                    ClientPackets.Instance.LoginResult(client, 4);
                    break;
            }        
            
#if DEBUG
    Console.WriteLine("Now there're {0} users connected to the launcher server.", clientCount);
#endif
        }

        public static void OnUserLogout(Client client)
        {
            clientList.Remove(client.Id);
            client.Dispose();
            Console.WriteLine("User disconnected");
        }
    }
}
