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
using System.Net;
using System.Web;
using System.IO;
using System.Security.Cryptography;
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
        internal readonly string md5Key = "1337";
        XDes DesCipher;
        internal Properties.Settings settings;
        internal LoginGUI loginGUI;
        internal LoginCredentials loginCreds;
        public static GUI Instance;

        public GUI()
        {
            InitializeComponent();
            DesCipher = new XDes(Program.DesKey);
            settings = Properties.Settings.Default;
            Instance = this;
        }

        private void GUI_Load(object sender, EventArgs e)
        {
        }

        private void GUI_Shown(object sender, EventArgs e)
        {
            bool close = false;

            // Start a connection to the server, if failed exit
            if (!ServerManager.Instance.Start(this.ip, this.port))
            {
                MessageBox.Show(ServerManager.Instance.ErrorMessage, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Gather the users login credentials
            loginCreds = GetCredentials();
            if (loginCreds != null)
            {
                // Attempt a login
                if (Login(loginCreds.Username, loginCreds.Password, loginCreds.Pin, loginCreds.Remember))
                {
                    if (checkForClient())
                    {
                        //TODO: navigateToSplash();

                        UpdateHandler.Instance.Start();

                        // TODO: navigateToHome();
                    }
                    else { close = true; }
                }
            }

            if (close) { this.Close(); }
        }

        protected bool checkForClient()
        {
            // Check that the provided client directory exists
            if (!Directory.Exists(settings.clientDirectory))
            {
                while (true)
                {
                    if (MessageBox.Show("The Client Path you provided is invalid!\nIf you want to continue please provide a valid path!", "Update Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK)
                    {
                        launcherSettings_btn_Click(null, EventArgs.Empty);

                        if (Directory.Exists(settings.clientDirectory) && File.Exists(Path.Combine(settings.clientDirectory, "sframe.exe")))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot continue without a valid client directory, shutting down!", "Fatal Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }
            }
            else { return true; }

            return false;
        }

        private LoginCredentials GetCredentials()
        {
            LoginCredentials tempCreds = null;

            if (settings.remember)
            {
                tempCreds = new LoginCredentials
                {
                    Username = settings.username,
                    Password = settings.password,
                    Pin = settings.pin
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
                            Password = loginGUI.Password,
                            Pin = loginGUI.Pin,
                            Remember = loginGUI.RememberMe
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
        /// Executes login validation using the webservice and then routes the browser to the homepage
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        private bool Login(string username, string password, string pin, bool remembered)
        {            
            // Sets the fingerprint
            string fingerprint = FingerPrint.Value;

            // TODO: Remove in favor of website based authentication
            //ServerPackets.Instance.Login(username, pass, fingerprint);

            string loginCode = null;

            UriBuilder uriBuilder = new UriBuilder("http://rappelz.team-vendetta.com/user/login_validation.aspx");
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["username"] = username;
            parameters["password"] = password;
            parameters["fingerprint"] = fingerprint;
            parameters["pin"] = pin;
            uriBuilder.Query = parameters.ToString();
            WebRequest validationRequest = WebRequest.Create(uriBuilder.Uri);

            // Request the login_code based on previously built Uri
            using (WebResponse response = validationRequest.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8)) { loginCode = sr.ReadToEnd(); }
                uriBuilder = null;
                parameters = null;
                validationRequest = null;
                response.Dispose();
            }

            if (!string.IsNullOrEmpty(loginCode))
            {
                switch (loginCode)
                {
                    case "ok":
                        return true;

                    case "no_username":
                        break;

                    case "no_password":
                        break;

                    case "invalid_username":
                        MessageBox.Show("Failed to Login!\nThe username you provided is invalid!", "Login Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case "invalid_password":
                        MessageBox.Show("Failed to Login!\nThe password you provided is invalid!", "Login Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case "require_pin":
                        MessageBox.Show("You have not set your pin, please do so before trying again", "Login Exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case "invalid_pin":
                        break;

                    case "account_locked":
                        break;

                    case "ban_type_1": case "ban_type_2": case "ban_type_3":
                        break;
                }
            }
            else
            {
                MessageBox.Show("Failed to get valid response from the Web service!", "Critical Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        protected void navigateToSplash()
        {
            // TODO: Navigate to splash url while updating
            browser.Source = new Uri("http://rappelz.team-vendetta.com/others/launcher_error.aspx?title=Updating...&sub=Please%20wait%20while%20we%20update%20your%20client");
        }

        protected void navigateToHome(string username, string password, string fingerprint)
        {
            browser.WebSession.SetCookie(new Uri("http://rappelz.team-vendetta.com"), string.Format("P_SESSION_ID={0};", username), true, true);
            browser.WebSession.SetCookie(new Uri("http://rappelz.team-vendetta.com"), string.Format("P_VAR_1={0};", PasswordCipher.CreateHash(md5Key, password)), true, true);
            browser.WebSession.SetCookie(new Uri("http://rappelz.team-vendetta.com"), string.Format("P_VAR_2={0};", fingerprint), true, true);

            // Router the user to the homepage
            browser.Source = new Uri("http://rappelz.team-vendetta.com");
        }

        /// TODO: Remove me
        public static void LoginResponse(int code)
        {
            //switch (code)
            //{
            //    case 0: // Success
            //        MessageBox.Show("You're logged.");
            //        UpdateHandler.Instance.Start();
            //        break;

            //    case 1: // Failed
            //        MessageBox.Show("Check your data and try again");
            //        break;
            //}
        }

        public static void OnUpdateComplete()
        {
            MessageBox.Show("Patching process completed.");

            // TODO: Fetch start arguments
        }

        private void close_Click(object sender, EventArgs e)
        {
            ServerManager.Instance.Close();
            this.Dispose();
        }

        private void launcherSettings_btn_Click(object sender, EventArgs e)
        {
            UserSettings settings = new UserSettings();
            GeneralSettingsGUI settingsGUI = new GeneralSettingsGUI(settings);
            settingsGUI.FormClosing += (o, x) => { settings = settingsGUI.userSettings; };
            settingsGUI.FormClosed += (o, x) => { settings.Save(); };
            settingsGUI.ShowDialog();
        }
    }
}
