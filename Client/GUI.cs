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

        internal readonly string ip = "127.0.0.1";
        internal readonly short port = 13545;
        internal readonly string md5Key = "1337";
        XDes DesCipher;
        public static GUI Instance;
        internal bool validated = false;
        internal bool canStart = false;
        internal string otp = null;
        internal readonly OPT SettingsManager;
        internal int loginFails = 0;

        public GUI()
        {
            InitializeComponent();
            Application.VisualStyleState = VisualStyleState.NoneEnabled;
            DesCipher = new XDes(Program.DesKey);
            Instance = this;
            SettingsManager = OPT.Instance;
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
            Instance.UpdateStatus(0, "Checking for Client Updates...");
            await Task.Run(() => { updateClient(); });
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
            if (string.IsNullOrEmpty(SettingsManager.GetString("clientdirectory")))
            {
                if (File.Exists("SFrame.exe")) { Instance.SettingsManager.Update("clientdirectory", "SFrame.exe"); }
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
            if (!Directory.Exists(SettingsManager.GetString("clientdirectory")))
            {
                while (true)
                {
                    if (MessageBox.Show("The Client Path you provided is invalid!\nIf you want to continue please provide a valid path!", "Update Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Stop) == DialogResult.OK)
                    {
                        launcherSettings_btn_Click(null, EventArgs.Empty);

                        if (Directory.Exists(SettingsManager.GetString("clientdirectory")) && File.Exists(Path.Combine(SettingsManager.GetString("clientdirectory"), "sframe.exe")))
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

        internal void updateClient()
        {
            if (checkForClient())
            {
                // Show splash screen!
                UpdateHandler.Instance.Start();
                if (UpdateHandler.Instance.NetworkError) { return; }
            }
            else
            {
                this.Close();
            }
        }

        // TODO: Find out why I'm invoking controls!
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
            if (validated && canStart)
            {
                ServerPackets.Instance.RequestArguments(Instance.SettingsManager.GetString("username"));
            }
        }

        public static void OnArgumentsReceived(string arguments)
        {
            if (!string.IsNullOrEmpty(arguments))
            {
                string launchArgs = arguments.TrimEnd('\0');
                launchArgs = StringExtension.ReplaceFirst(launchArgs, "?", Instance.SettingsManager.GetString("codepage"));
                launchArgs = StringExtension.ReplaceFirst(launchArgs, "?", Instance.SettingsManager.GetString("country"));
                launchArgs = StringExtension.ReplaceFirst(launchArgs, "?", Instance.otp);

                if (SFrameBypass.Start(10, launchArgs)) { if (Instance.SettingsManager.GetBool("closeonstart")) { Instance.Invoke(new MethodInvoker(delegate { Instance.Close(); })); } }
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
            settingsGUI.ShowDialog(this);
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
