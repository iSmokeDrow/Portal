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
using Awesomium.Core;

namespace Client
{
    // TODO: Make it so that if validation fails (while rememberMe) user has option to forget credentials
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

        internal readonly string ip = "176.31.181.127";
        internal readonly short port = 13545;
        internal readonly string md5Key = "1337";
        XDes DesCipher;
        internal Properties.Settings settings;
        internal LoginGUI loginGUI;
        internal LoginCredentials loginCreds;
        public static GUI Instance;
        internal string fingerPrint;
        internal bool validated = false;
        internal WebSession webSession;

        public GUI()
        {
            InitializeComponent();
            DesCipher = new XDes(Program.DesKey);
            settings = Properties.Settings.Default;
            Instance = this;
            webSession = WebCore.CreateWebSession(string.Concat(Directory.GetCurrentDirectory(), @"\web\"), WebPreferences.Default);
            //browser.WebSession = webSession;
        }

        private void GUI_Load(object sender, EventArgs e)
        {
        }

        private void GUI_Shown(object sender, EventArgs e)
        {
            int failedAttempts = 0;

            while (!attemptLogin())
            {
                if (failedAttempts == 2 && settings.remember)
                {
                    if (MessageBox.Show("Yould you like to forget your Login Credentials?", "Login Exception", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        settings.username = string.Empty;
                        settings.password = string.Empty;
                        settings.remember = false;
                        settings.Save();
                        settings.Reload();
                    }
                    else
                    {
                        MessageBox.Show("Cannot continue without login, shutting down!", "Login Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }

                failedAttempts++;

                attemptLogin();
            }

            validated = true;

            if (checkForClient())
            {
                navigateToSplash();

                totalStatus.Text = "Updating the client...";
                UpdateHandler.Instance.Start();

                //if (UpdateHandler.Instance.NetworkError) { return; }
            }
            else
            {
                MessageBox.Show("Cannot continue without a valid client path, shuttimng down!", "Update Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        protected bool attemptLogin()
        {
            // Assign a fingerprint before the login attempt
            fingerPrint = FingerPrint.Value;

            loginCreds = GetCredentials();

            if (loginCreds != null)
            {
                if (Login(loginCreds.Username, loginCreds.Password, loginCreds.Pin, loginCreds.Remember))
                {
                    return true;
                }
            }

            return false;
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
            bool close = false;

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
                    else if (loginGUI.CancelClicked)
                    {
                        MessageBox.Show("Cannot continue without login, shutting down!", "Login Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        close = true;
                    }
                    else if (!loginGUI.LoginClicked) { MessageBox.Show("You didn't provide proper credentials, please try again!", "Credentials Exception", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                    loginGUI.Dispose();
                };

                loginGUI.ShowDialog(this);
            }

            if (close) { this.Close(); }
            return tempCreds;
        }

        /// <summary>
        /// Executes login validation using the webservice and then routes the browser to the homepage
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        private bool Login(string username, string password, string pin, bool remembered)
        {            
            string loginCode = null;

            UriBuilder uriBuilder = new UriBuilder("http://rappelz.team-vendetta.com/user/login_validation.aspx");
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["username"] = username;
            parameters["password"] = password;
            parameters["fingerprint"] = fingerPrint;
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
                        MessageBox.Show("You have not set your Username, please do so before trying again", "Login Exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case "no_password":
                        MessageBox.Show("You have not set your Password, please do so before trying again", "Login Exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("Failed to Login!\nThe pin you provided is invalid!", "Login Exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case "account_locked":
                        MessageBox.Show("Failed to Login!\nYour account is current locked!", "Login Exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case "ban_type_1": case "ban_type_2": case "ban_type_3":
                        MessageBox.Show("Failed to Login!\nYour account has been banned!", "Login Exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            browser.WebSession.SetCookie(new Uri("http://rappelz.team-vendetta.com"), "LOGIN=FAKE", true, true);
            browser.WebSession.SetCookie(new Uri("http://rappelz.team-vendetta.com"), string.Format("P_SESSION_ID={0};", username), true, true);
            browser.WebSession.SetCookie(new Uri("http://rappelz.team-vendetta.com"), string.Format("P_VAR_1={0};", PasswordCipher.CreateHash(md5Key, password)), true, true);
            browser.WebSession.SetCookie(new Uri("http://rappelz.team-vendetta.com"), string.Format("P_VAR_2={0};", fingerprint), true, true);

            // Router the user to the homepage
            browser.Source = new Uri("http://rappelz.team-vendetta.com");
        }

        public static void OnUpdateComplete()
        {
            Instance.totalStatus.ResetText();
            Instance.totalProgress.Maximum = 100;
            Instance.totalProgress.Value = 0;
            Instance.currentStatus.ResetText();
            Instance.currentProgress.Maximum = 100;
            Instance.currentProgress.Value = 0;
            Instance.navigateToHome(Instance.loginCreds.Username, Instance.loginCreds.Password, Instance.fingerPrint);
        }

        public void UpdateStatus(int type, string text)
        {
            switch (type)
            {
                case 0: // total
                    this.Invoke(new MethodInvoker(delegate { totalStatus.Text = text; }));
                    break;

                case 1: // current
                    this.Invoke(new MethodInvoker(delegate { currentStatus.Text = text; }));
                    break;
            }
        }

        public void UpdateProgressMaximum(int type, int maximum)
        {
            switch (type)
            {
                case 0: // total
                    this.Invoke(new MethodInvoker(delegate { totalProgress.Maximum = maximum; }));
                    break;

                case 1: // current
                    this.Invoke(new MethodInvoker(delegate { currentProgress.Maximum = maximum; }));
                    break;
            }
        }

        public void UpdateProgressValue(int type, int value)
        {
            switch (type)
            {
                case 0: // total
                    this.Invoke(new MethodInvoker(delegate { totalProgress.Value = value; }));
                    break;

                case 1: // current
                    this.Invoke(new MethodInvoker(delegate { currentProgress.Value = value; }));
                    break;
            }
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            if (validated)
            {
                ServerPackets.Instance.RequestArguments(loginCreds.Username);
            }
        }

        public static void OnArgumentsReceived(string arguments)
        {
            // TODO : What to do with start arguments
            MessageBox.Show(string.Format("Start arguments received:\n{0}", arguments));
        }

        private void close_Click(object sender, EventArgs e)
        {
            ServerManager.Instance.Close();
            this.Dispose();
        }

        private void launcherSettings_btn_Click(object sender, EventArgs e)
        {
            if (validated)
            {
                UserSettings settings = new UserSettings();
                GeneralSettingsGUI settingsGUI = new GeneralSettingsGUI(settings);
                settingsGUI.FormClosing += (o, x) => { settings = settingsGUI.userSettings; };
                settingsGUI.FormClosed += (o, x) => { settings.Save(); };
                settingsGUI.ShowDialog(this);
            }
        }

        private void GUI_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gameSettings_lb_Click(object sender, EventArgs e)
        {
            if (validated)
            {
                SettingsManager.InitRappelzSettings();
                RappelzSettingsGUI settings = new RappelzSettingsGUI(SettingsManager.RappelzSettings);
                settings.FormClosing += (o, x) => { SettingsManager.SaveSettings(SettingsManager.RappelzSettings); };
                settings.ShowDialog(this);
            }
        }
    }
}
