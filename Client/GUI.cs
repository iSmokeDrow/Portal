﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.IO;
using Client.Functions;
using Client.Network;
using Client.Structures;

namespace Client
{
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

        // TODO: Add ip/port to General Settings menu
        internal readonly string ip = "127.0.0.1";
        internal readonly short port = 13545;
        public static GUI Instance;
        internal bool canStart = false;

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
                if (Process.GetProcessesByName("Updater").Length > 0)
                {
                    foreach (var process in Process.GetProcessesByName("Updater")) { process.Kill(); }
                }
            });
        }

        private async void GUI_Shown(object sender, EventArgs e)
        {
            Instance.UpdateStatus(0, "Loading settings...");
            await Task.Run(() => { OPT.Instance.Start(); });
            Instance.UpdateStatus(0, "Checking for SFrame...");
            await Task.Run(() => { checkForSFrame(); });
            Instance.UpdateStatus(0, "Connecting to Update Server...");
            connectToServer();
            Instance.UpdateStatus(0, "Waiting for User Credentials...");
            Login();
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

        private void Login()
        {
            ServerPackets.Instance.RequestDESKey();
        }

        internal void checkForSFrame()
        {
            if (string.IsNullOrEmpty(OPT.Instance.GetString("clientdirectory")))
            {
                if (File.Exists("SFrame.exe")) { OPT.Instance.Update("clientdirectory", "SFrame.exe"); }
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

        protected bool checkForClient()
        {
            // Check that the provided client directory exists
            if (!Directory.Exists(OPT.Instance.GetString("clientdirectory")))
            {
                while (true)
                {
                    if (MessageBox.Show("The Client Path you provided is invalid!\nIf you want to continue please provide a valid path!", "Update Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK)
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

        public void OnDesKeyReceived(string desKey)
        {
            using (LoginGUI login = new LoginGUI())
            {
                this.Invoke(new MethodInvoker(delegate 
                {
                    login.ShowDialog(this);

                    if (!login.Cancelled)
                    {
                        UpdateStatus(0, "Validating your credentials...");
                        ServerPackets.Instance.RequestUserValidation(desKey, login.Username, login.Password, FingerPrint.Value);
                    }
                }));
            }
        }

        public async void OnValidationResultReceived(bool validated)
        {
            if (validated)
            {                
                canStart = true;
                Instance.UpdateStatus(0, "Checking for Updater Update...");
                await Task.Run(() => { checkForUpdater(); });
                Instance.UpdateStatus(0, "Checking for Launcher Update...");
                await Task.Run(() => { checkForSelfUpdate(); });
                Instance.UpdateStatus(0, "Checking for Client Updates...");
                await Task.Run(() => { updateClient(); });
            }
        }

        internal void updateClient()
        {
            if (checkForClient())
            {
                UpdateHandler.Instance.Start();
                if (UpdateHandler.Instance.NetworkError) { return; }
            }
            else
            {
                this.Invoke(new MethodInvoker(delegate { this.Close(); }));
            }
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
            if (canStart)
            {
                ServerPackets.Instance.RequestArguments(OPT.Instance.GetString("username"));
            }
        }

        // TODO: Server needs to decide the opt and send it with the arguments
        public static void OnArgumentsReceived(string arguments)
        {
            if (!string.IsNullOrEmpty(arguments))
            {
                string launchArgs = arguments.TrimEnd('\0');
                launchArgs = StringExtension.ReplaceFirst(launchArgs, "?", OPT.Instance.GetString("codepage"));
                launchArgs = StringExtension.ReplaceFirst(launchArgs, "?", OPT.Instance.GetString("country"));

                if (SFrameBypass.Start(10, launchArgs)) { if (OPT.Instance.GetBool("closeonstart")) { Instance.Invoke(new MethodInvoker(delegate { Instance.Close(); })); } }
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
            GeneralSettingsGUI settingsGUI = new GeneralSettingsGUI();
            this.Invoke(new MethodInvoker(delegate { settingsGUI.ShowDialog(this); }));
        }

        private void GUI_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gameSettings_lb_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This menu is still under construction! Use at your own risk!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand) == DialogResult.OK)
            {
                RappelzSettings.InitRappelzSettings();
                // TODO: Rebuild how the client settings are read/saved
            }
        }
    }
}
