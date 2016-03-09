using System;
using System.Collections.Generic;
using Server.Functions;
using Server.Network;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

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
        /// It's used to encrypt arguments
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

            HandleOTP();
            Console.WriteLine("OTP Handler Started.");

                
            Console.ReadLine();
        }

        internal static void HandleOTP()
        {
            Task.Run(() =>
            {
                TimerCallback otpTimerCallback = new TimerCallback(otpTick);
                Timer otpTimer = new Timer(otpTimerCallback, null, 0, Convert.ToInt32(OPT.SettingsList["otp.check.interval"]) * 1000);
                for (;;) { }
            });
        }

        internal static void otpTick(Object state) { OTP.HandleOTP(); }

        internal static void OnUserRequestArguments(Client client, string username)
        {
            // Set the OTP for this username
            string otp = OTP.SetOTP(username);

            if (!string.IsNullOrEmpty(otp))
            {
                // TODO : Replace placeholder launch arguments here
                ClientPackets.Instance.Arguments(client, string.Format("Start SFrame.exe /auth_ip:192.168.0.101 /auth_port:8841 /locale:ASCII /country:US /cash /commercial_shop /imbc_login /username:{0} /password:{1}", username, otp));
            }
            else { Console.Write("Failed to send OTP for Username: {0}", username); }
        }
    }
}
