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
using Client.Structures;

namespace Client
{
    public partial class GUI : Form
    {
        #region Drag Hack
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        ///
        /// Handling the window messages
        ///
        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
                message.Result = (IntPtr)HTCAPTION;
        }
        #endregion

        internal readonly string ip = "127.0.0.1";
        internal readonly short port = 13545;
        XDes DesCipher;
        internal Properties.Settings settings;
        internal LoginGUI loginGUI;
        internal LoginCredentials loginCreds;

        public GUI()
        {
            InitializeComponent();
            DesCipher = new XDes(Program.DesKey);
            settings = Properties.Settings.Default;
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            if (!ServerManager.Instance.Start(this.ip, this.port))
            {
                MessageBox.Show(ServerManager.Instance.ErrorMessage, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

#if DEBUG
            MessageBox.Show("Connected");
#endif

        }

        private void GUI_Shown(object sender, EventArgs e)
        {
            loginCreds = GetCredentials();
            if (loginCreds != null)
            {
                Login(loginCreds.Username, loginCreds.Password);
            }
        }

        private LoginCredentials GetCredentials()
        {
            LoginCredentials tempCreds = null;

            if (settings.remember)
            {
                tempCreds = new LoginCredentials
                {
                    Username = settings.username,
                    Password = settings.password
                };
            }
            else
            {
                loginGUI = new LoginGUI();

                // When form is hidden 
                loginGUI.FormClosing += (o, x) =>
                {
                    if (loginGUI.LoginClicked)
                    {
                        tempCreds = new LoginCredentials
                        {
                            Username = loginGUI.Username,
                            Password = loginGUI.Password
                        };
                    }
                    else { /*Close connection, state cannot continue without login*/ }

                    loginGUI.Dispose();
                };

                loginGUI.ShowDialog(this);
            }

            return tempCreds;
        }

        /// <summary>
        /// Prepares a login request
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        private void Login(string username, string password)
        {
            // Encrypt password (it must be a 56 characters string)
            byte[] pass = DesCipher.Encrypt(password.PadRight(56, '\0'));
            // Sets the fingerprint
            string fingerprint = FingerPrint.Value;

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
                    UpdateHandler.Instance.Start();
                    break;

                case 1: // Failed
                    MessageBox.Show("Check your data and try again");
                    break;
            }
        }

        public static void OnUpdateComplete()
        {
            MessageBox.Show("Patching process completed.");
        }

        private void close_Click(object sender, EventArgs e)
        {
            ServerManager.Instance.Close();
            this.Dispose();
        }
    }
}
