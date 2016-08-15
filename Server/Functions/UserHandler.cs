using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Network;

namespace Server.Functions
{
    // TODO: Implement database user validation
    public class UserHandler
    {
        protected static UserHandler instance;
        public static UserHandler Instance
        {
            get
            {
                if (instance == null) { instance = new UserHandler(); }

                return instance;
            }
        }

        public void OnUserRequestDesKey(Client client)
        {
            if (OPT.SettingExists("des.key"))
            {
                string desKey = OPT.GetString("des.key");

                PacketStream stream = new PacketStream(0x9999);
                stream.WriteString(desKey);

                if (OPT.GetBool("debug")) { Console.WriteLine("[{0}] Sent!", desKey); }

                ClientManager.Instance.Send(client, stream);
            }
            else { /*Report Error*/ }
        }

        public void OnValidateUser(Client client, string name, string password, string FingerPrint)
        {

        }
    }
}
