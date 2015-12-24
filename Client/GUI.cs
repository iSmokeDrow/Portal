using System;
using Client.Network;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Functions;

namespace Client
{
    public partial class GUI : Form
    {
        XDes DesCipher;

        public GUI()
        {
            InitializeComponent();
            DesCipher = new XDes(Program.DesKey);
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            if (!ServerManager.Instance.Start("127.0.0.1", 13545))
            {
                MessageBox.Show(ServerManager.Instance.ErrorMessage);
                return;
            }
            MessageBox.Show("Connected");
            
            // Calls the method to prepare a login request
            Login("itoki", "shadows1");
            MessageBox.Show("Sent");
        }

        /// <summary>
        /// Prepares a login request
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        private void Login(string username, string password)
        {
            // Encrypt password
            byte[] pass = DesCipher.Encrypt(password);
            // Sets the fingerprint
            string fingerprint = "5D61-1131-B2D5-3619-EF76-0885-57D3-1972";

            // Try to connect
            ServerPackets.Instance.Login(username, pass, fingerprint);
        }

        /// <summary>
        /// When server sends a response of the login
        /// </summary>
        /// <param name="code"></param>
        public static void LoginResponse(int code)
        {
            switch (code)
            {
                case 0: // Success
                    MessageBox.Show("You're logged.");
                    break;

                case 1: // Failed
                    MessageBox.Show("Check your data and try again");
                    break;
            }
        }
    }
}
