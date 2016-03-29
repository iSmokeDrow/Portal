using System;
using Client.Network;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
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
        internal bool isDragging = false;
        internal Point lastLocation;

        private void GUI_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastLocation = e.Location;
        }

        private void GUI_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void GUI_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y
                    );

                this.Update();
            }
        }
        #endregion

        internal readonly string ip = "176.31.181.127";
        internal readonly short port = 13545;
        internal readonly string md5Key = "1337";
        XDes DesCipher;
        internal LoginGUI loginGUI;
        public static GUI Instance;
        internal string fingerPrint;
        internal bool validated = false;
        internal bool canStart = false;
        internal string otp = null;
        public OPT SettingsManager = new OPT();
        internal int loginFails = 0;

        public GUI()
        {
            InitializeComponent();
            Application.VisualStyleState = VisualStyleState.NoneEnabled;
            DesCipher = new XDes(Program.DesKey);
            Instance = this;
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            Task.Run(() => 
            {
                if (Process.GetProcessesByName("Updater").Length > 0)
                {
                    foreach (var process in Process.GetProcessesByName("Updater")) { process.Kill(); }
                }
            });
        }

        private async void GUI_Shown(object sender, EventArgs e)
        {
            Instance.UpdateStatus(0, "Loading settings...");
            await Task.Run(() => { SettingsManager.Start(); });
            Instance.UpdateStatus(0, "Checking for SFrame...");
            await Task.Run(() => { checkForSFrame(); });
            Instance.UpdateStatus(0, "Connecting to Update Server...");
            connectToServer();
            Instance.UpdateStatus(0, "Checking for Updater Update...");
            await Task.Run(() => { checkForUpdater(); });
            Instance.UpdateStatus(0, "Checking for Launcher Update...");
            await Task.Run(() => { checkForSelfUpdate(); });
            Instance.UpdateStatus(0, "Attempting to Login via webservice...");
            attemptLogin();
            Instance.UpdateStatus(0, "");
        }


        internal void connectToServer()
        {
            if (!ServerManager.Instance.Start(ip, port))
            {
                MessageBox.Show(ServerManager.Instance.ErrorMessage, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        internal void checkForSFrame()
        {
            if (string.IsNullOrEmpty(SettingsManager.GetStringValue("clientdirectory")))
            {
                if (File.Exists("SFrame.exe")) { Instance.SettingsManager.UpdateValue("clientdirectory", "SFrame.exe"); }
            }
        }

        internal void checkForUpdater()
        {
            if (File.Exists("Updater.exe")) { ServerPackets.Instance.RequestUpdater(Hash.GetSHA512Hash("Updater.exe")); }
            else { ServerPackets.Instance.RequestUpdater(null); }
        }

        internal void checkForSelfUpdate()
        {
            if (File.Exists("Launcher.exe"))
            {
                ServerPackets.Instance.RequestSelfUpdate(Hash.GetSHA512Hash("Launcher.exe"));
            }
        }

        private void attemptLogin()
        {
            if (SettingsManager.GetBoolValue("remember"))
            {
                Login(SettingsManager.GetStringValue("username"), SettingsManager.GetStringValue("password"), SettingsManager.GetStringValue("pin"));
            }
            else
            {
                using (loginGUI = new LoginGUI())
                {
                    loginGUI.FormClosing += (o, x) =>
                    {
                        if (loginGUI.Cancelled)
                        {
                            MessageBox.Show("Cannot continue without login!\nShutting down.", "Login Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };
                    loginGUI.ShowDialog(this);
                }          
            }
        }

        protected bool checkForClient()
        {
            // Check that the provided client directory exists
            if (!Directory.Exists(SettingsManager.GetStringValue("clientdirectory")))
            {
                while (true)
                {
                    if (MessageBox.Show("The Client Path you provided is invalid!\nIf you want to continue please provide a valid path!", "Update Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK)
                    {
                        launcherSettings_btn_Click(null, EventArgs.Empty);

                        if (Directory.Exists(SettingsManager.GetStringValue("clientdirectory")) && File.Exists(Path.Combine(SettingsManager.GetStringValue("clientdirectory"), "sframe.exe")))
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

        /// <summary>
        /// Executes login validation using the webservice and then routes the browser to the homepage
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public async void Login(string username, string password, string pin)
        {
            // Assign a fingerprint before the login attempt
            fingerPrint = FingerPrint.Value;

            Instance.UpdateStatus(0, "Validating Login Credentials...");

            string loginCode = null;

            UriBuilder uriBuilder = new UriBuilder("http://rappelz.team-vendetta.com/user/login_validation.aspx");
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["username"] = username;
            parameters["password"] = password;
            parameters["fingerprint"] = fingerPrint;
            parameters["pin"] = pin;
            parameters["otp"] = "1";
            uriBuilder.Query = parameters.ToString();
            WebRequest validationRequest = WebRequest.Create(uriBuilder.Uri);

            await Task.Run(() => 
            {             
                // Request the login_code based on previously built Uri
                using (WebResponse response = validationRequest.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8)) { loginCode = sr.ReadToEnd(); }
                    uriBuilder = null;
                    parameters = null;
                    validationRequest = null;
                    response.Dispose();
                }
            });

            if (!string.IsNullOrEmpty(loginCode))
            {
                if (loginCode.StartsWith("ok"))
                {
                    string[] codeParts = loginCode.Split(':');
                    otp = codeParts[1];
                    Instance.UpdateStatus(0, "Login Credentials Validated!");
                    validated = true;
                    updateClient();
                }
                else
                {
                    Instance.UpdateStatus(0, "Failed to validate Login Credentials!");

                    loginFails++;

                    switch (loginCode)
                    {
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

                        case "ban_type_1":
                        case "ban_type_2":
                        case "ban_type_3":
                            MessageBox.Show("Failed to Login!\nYour account has been banned!", "Login Exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }

                    if (loginFails > 0 && SettingsManager.GetBoolValue("remember"))
                    {
                        if (MessageBox.Show("It seems you have failed to login with remembered credentials, would you like to forget them?", "Login Exception", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            SettingsManager.UpdateValue("username", "");
                            SettingsManager.UpdateValue("password", "");
                            SettingsManager.UpdateValue("pin", "");
                            SettingsManager.UpdateValue("remember", false);
                            SettingsManager.writeOPT();
                            attemptLogin();
                        }
                        else { MessageBox.Show("Cannot continue without login!\nShutting down.", "Login Exception", MessageBoxButtons.OK, MessageBoxIcon.Error); this.Close(); }
                    }
                    else
                    {
                        if (MessageBox.Show("Would you like to try again?", "Login Exception", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            attemptLogin();
                        }
                        else { MessageBox.Show("Cannot continue without login!\nShutting down.", "Login Exception", MessageBoxButtons.OK, MessageBoxIcon.Error); this.Close(); }
                    }
                }
            }
            else
            {
                MessageBox.Show("Failed to get valid response from the Web service!", "Critical Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void updateClient()
        {
            if (checkForClient())
            {
                navigateToSplash();

                UpdateHandler.Instance.Start();
                if (UpdateHandler.Instance.NetworkError) { return; }
            }
            else
            {
                MessageBox.Show("Cannot continue without a valid client path, shuttimng down!", "Update Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
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

        public void OnUpdateComplete()
        {
            Instance.Invoke(new MethodInvoker(delegate
            {   
                Instance.totalStatus.ResetText();
                Instance.totalProgress.Maximum = 100;
                Instance.totalProgress.Value = 0;
                Instance.currentStatus.ResetText();
                Instance.currentProgress.Maximum = 100;
                Instance.currentProgress.Value = 0;
                Instance.navigateToHome(SettingsManager.GetStringValue("username"), SettingsManager.GetStringValue("password"), Instance.fingerPrint);
                Instance.canStart = true;
            }));
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
            if (validated && canStart)
            {
                ServerPackets.Instance.RequestArguments(Instance.SettingsManager.GetStringValue("username"));
            }
        }

        public static void OnArgumentsReceived(string arguments)
        {
            if (!string.IsNullOrEmpty(arguments))
            {
                string launchArgs = arguments.TrimEnd('\0');
                launchArgs = StringExtension.ReplaceFirst(launchArgs, "?", Instance.SettingsManager.GetStringValue("codepage"));
                launchArgs = StringExtension.ReplaceFirst(launchArgs, "?", Instance.SettingsManager.GetStringValue("country"));
                launchArgs = StringExtension.ReplaceFirst(launchArgs, "?", Instance.otp);

                if (SFrameBypass.Start(10, launchArgs)) { if (Instance.SettingsManager.GetBoolValue("closeonstart")) { Instance.Invoke(new MethodInvoker(delegate { Instance.Close(); })); } }
                else { MessageBox.Show("The SFrame.exe has failed to start", "Fatal Exception", MessageBoxButtons.OK, MessageBoxIcon.Error); Instance.Close(); }
            }
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
                GeneralSettingsGUI settingsGUI = new GeneralSettingsGUI();
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
                if (MessageBox.Show("This menu is still under construction! Use at your own risk!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand) == DialogResult.OK)
                {
                    Structures.SettingsManager.InitRappelzSettings();
                    RappelzSettingsGUI settings = new RappelzSettingsGUI(Structures.SettingsManager.RappelzSettings);
                    settings.FormClosing += (o, x) => { Structures.SettingsManager.SaveSettings(Structures.SettingsManager.RappelzSettings); };
                    settings.ShowDialog(this);
                }
            }
        }
    }
}
