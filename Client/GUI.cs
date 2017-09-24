using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.IO;
using Client.Functions;
using Client.Network;
using Client.Structures;
using ProgressODoom;

namespace Client
{
    // TODO: Add Shader menu
    // TODO: Implement PLAY_WINLOCK
    // TODO: Implement PLAY_WINALPHA ???
    // TODO: Implement PLAY_PARTYDM ???
    // TODO: Remove instancing from update handler class (redundant)
    // TODO: Reorganize packet ids on client/server sides for continuity
    // TODO: Trigger graceful disconnect before launching updater.exe
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

        protected string ip = "127.0.0.1";
        protected int port = 13545;
        public static GUI Instance;
        protected bool canStart = false;
        protected string otp = string.Empty;
        protected int authenticationType = 0;

        public GUI()
        {
            InitializeComponent();
            Application.VisualStyleState = VisualStyleState.NoneEnabled;
            Instance = this;
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                if (Process.GetProcessesByName("Updater").Length > 0) { foreach (var process in Process.GetProcessesByName("Updater")) { process.Kill(); } }
            });
        }

        private async void GUI_Shown(object sender, EventArgs e)
        {
            Instance.UpdateStatus(0, "Initializing settings...");
            await Task.Run(() => { OPT.Instance.Start(); });
            await Task.Run(() => { assignIP(); });
            Instance.UpdateStatus(0, "Checking for SFrame...");
            await Task.Run(() => { clientExists(); });
            Instance.UpdateStatus(0, "Connecting to Update Server...");
            connectToServer();
        }

        private void assignIP()
        {
            ip = OPT.Instance.GetString("ip");
            port = OPT.Instance.GetInt("port");
        }

        private void connectToServer()
        {
            if (!ServerManager.Instance.Start(ip, port))
            {
                this.Close();
            }

            registerClient();
        }

        internal void registerClient() { ServerPackets.Instance.CS_RegisterClient(); }

        internal void OnRegisterCompleteReceived()
        {
            // TODO: Initialize heartbeat

            Instance.UpdateStatus(0, "Checking for Launcher update...");
            checkForSelfUpdate();

        }

        internal void checkAuthenticationType() { ServerPackets.Instance.CS_RequestAuthenticationType(); }

        internal void checkForSFrame()
        {
            if (string.IsNullOrEmpty(OPT.Instance.GetString("clientdirectory")))
            {
                if (File.Exists("SFrame.exe")) { OPT.Instance.Update("clientdirectory", "SFrame.exe"); }
            }
        }

        protected bool clientExists()
        {
            string sframePath = string.Format(@"{0}\{1}", OPT.Instance.GetString("clientdirectory"), "sframe.exe");

            if (!File.Exists(sframePath))
            {
                while (true)
                {
                    if (MessageBox.Show("The Client Path you provided is invalid!\nIf you want to continue please provide a valid path!", "Update Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        launcherSettings_btn_Click(null, EventArgs.Empty);

                        if (Directory.Exists(OPT.Instance.GetString("clientdirectory")) && File.Exists(Path.Combine(OPT.Instance.GetString("clientdirectory"), "sframe.exe")))
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

        internal void checkForSelfUpdate()
        {
            if (File.Exists("Launcher.exe"))
            {
                ServerPackets.Instance.CS_RequestSelfUpdate(Hash.GetSHA512Hash("Launcher.exe")); }
        }

        internal void OnSelfUpdateCheckCompleted(bool updateRequired)
        {
            if (updateRequired) { UpdateHandler.Instance.ExecuteSelfUpdate(); }
            else { RequestDesKey(); }
        }

        internal void RequestDesKey()
        {
            ServerPackets.Instance.RequestDESKey();
            Instance.UpdateStatus(0, "");
        }

        internal void OnDesKeyReceived(string desKey)
        {
            ServerPackets.Instance.SetDES(desKey);
            checkAuthenticationType();
        }

        internal void OnAuthenticationTypeReceived(int type)
        {
            this.authenticationType = type;

            switch (type)
            {
                case 0: //imbc off
                    doUpdateTasks();
                    canStart = true;
                    break;

                case 1: //imbc on
                    Instance.UpdateStatus(0, "Waiting for User Credentials...");
                    login();
                    break;
            }
        }

        internal void login()
        {
            using (LoginGUI login = new LoginGUI())
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    login.ShowDialog(this);

                    if (!login.Cancelled)
                    {
                        UpdateStatus(0, "Validating your credentials...");
                        ServerPackets.Instance.CS_ValidateUser(login.Username, login.Password, FingerPrint.Value);
                    }
                    else
                    {
                        MessageBox.Show("Cannot continue without proper login credentials! Shutting down!", "Fatal Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ServerManager.Instance.Close();
                        this.Close();
                    }
                }));
            }
        }

        internal void OnUserBannedReceived(int banType)
        {
            MessageBox.Show(string.Format("Your {0} has been banned!", (banType == 0) ? "account" : "FingerPrint"), "Login Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            GUI.Instance.UpdateStatus(0, "Disconnected!");
            ServerManager.Instance.Close();
        }

        internal void OnUserAccountNullReceived()
        {
            if (MessageBox.Show("The username or password you entered is incorrect, would you like to try again?", "Login Exception", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                login();
            }
            else
            {
                GUI.Instance.UpdateStatus(0, "Disconnected!");
                ServerManager.Instance.Close();
            }
        }

        internal void OnValidationResultReceived(string otp)
        {
            if (otp != null)
            {
                this.otp = otp;
                doUpdateTasks();
                canStart = true;
            }
        }

        internal void doUpdateTasks() { ServerPackets.Instance.CS_RequestUpdatesEnabled(); }

        // TODO: Redo for continuity?
        internal async void OnUpdatesEnabledReceived(int updatesDisabled)
        {
            if (updatesDisabled == 0)
            {
                Instance.UpdateStatus(0, "Checking for Client Updates...");
                await Task.Run(() => { UpdateHandler.Instance.Start(); });
            }
            else { OnUpdateComplete(); }
        }

        internal void OnUpdateComplete()
        {
            Instance.Invoke(new MethodInvoker(delegate
            {
                Instance.totalStatus.ResetText();
                Instance.totalProgress.Maximum = 100;
                Instance.totalProgress.Value = 0;
                Instance.currentStatus.ResetText();
                Instance.currentProgress.Maximum = 100;
                Instance.currentProgress.Value = 0;
                Instance.enableStart();
                canStart = true;
            }));
        }

        private void enableStart()
        {

            start_btn.Image = Client.Properties.Resources.start_on;
            start_btn.Enabled = true;
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

        public void ResetProgressStatus(int type)
        {
            switch (type)
            {
                case 0: // total
                    this.Invoke(new MethodInvoker(delegate
                    {
                        totalProgress.Value = 0;
                        totalProgress.Maximum = 100;
                        totalStatus.Text = string.Empty;
                    }));
                    break;

                case 1: // current
                    this.Invoke(new MethodInvoker(delegate
                    {
                        currentProgress.Value = 0;
                        currentProgress.Maximum = 100;
                        currentStatus.Text = string.Empty;
                    }));
                    break;

                case 2: // both
                    this.Invoke(new MethodInvoker(delegate
                    {
                        totalProgress.Value = 0;
                        totalProgress.Maximum = 100;
                        totalStatus.Text = string.Empty;

                        currentProgress.Value = 0;
                        currentProgress.Maximum = 100;
                        currentStatus.Text = string.Empty;
                    }));
                    break;
            }
        }

        private void start_btn_Click(object sender, EventArgs e) { if (canStart) { ServerPackets.Instance.CS_RequestArguments(); } }

        internal void OnArgumentsReceived(string arguments, int startType, bool isMaintenance)
        {
            if (!string.IsNullOrEmpty(arguments))
            {
                string launchArgs = arguments.TrimEnd('\0');
                launchArgs = StringExtension.ReplaceFirst(launchArgs, "?", OPT.Instance.GetString("codepage"));
                launchArgs = StringExtension.ReplaceFirst(launchArgs, "?", OPT.Instance.GetString("country"));

                if (authenticationType == 1)
                {
                    launchArgs = StringExtension.ReplaceFirst(launchArgs, "?", OPT.Instance.GetString("username"));
                    launchArgs = StringExtension.ReplaceFirst(launchArgs, "?", otp);
                }

                if (OPT.Instance.GetBool("showfps")) { launchArgs += " /winfps"; }

                if (!isMaintenance)
                {
                    if (startType == 1) // Use SFrameBypass
                    {
                        if (!SFrameBypass.Start(10, launchArgs)) { MessageBox.Show("The SFrame.exe has failed to start", "Fatal Exception", MessageBoxButtons.OK, MessageBoxIcon.Error); Instance.close_Click(null, EventArgs.Empty); }
                    }
                    else
                    {
                        var p = new ProcessStartInfo();
                        p.FileName = string.Concat(OPT.Instance.GetString("clientdirectory"), @"\SFrame.exe");
                        p.WorkingDirectory = OPT.Instance.GetString("clientdirectory");
                        p.UseShellExecute = false;
                        p.Arguments = launchArgs;
                        Process.Start(p);
                    }

                    if (OPT.Instance.GetBool("closeonstart")) { Instance.Invoke(new MethodInvoker(delegate { Instance.close_Click(null, EventArgs.Empty); })); }
                }
                else { MessageBox.Show("Cannot start because the server is currently in maintenance!\n\nTry again in a little bit!", "Maintenance Exception", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            Instance.UpdateStatus(0, "Requesting graceful disconnection...");
            ServerPackets.Instance.RequestDisconnect();
        }

        internal void launcherSettings_btn_Click(object sender, EventArgs e)
        {
            GeneralSettingsGUI settingsGUI = new GeneralSettingsGUI();
            this.Invoke(new MethodInvoker(delegate { settingsGUI.ShowDialog(this); }));
        }

        private void GUI_DoubleClick(object sender, EventArgs e) { /* Dummy Method */ }

        private void gameSettings_lb_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This menu is still under construction! Use at your own risk!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand) == DialogResult.OK)
            {
                RappelzSettings.InitRappelzSettings();

                using (RappelzSettingsGUI settingsGUI = new RappelzSettingsGUI(RappelzSettings.Settings))
                {
                    settingsGUI.ShowDialog();
                }
            }
        }
    }
}
