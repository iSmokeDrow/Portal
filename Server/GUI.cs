using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Server.Functions;
using Server.Network;
using Server.Structures;

namespace Server
{
    // TODO: Establish heartbeat with Client to check for disconnections on either side
    public partial class GUI : Form
    {
        /// <summary>
        /// Server and client must have the same RC4 key
        /// it's used to encrypt packet data
        /// </summary>
        public static string RC4Key = "password1";

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

        internal static string tmpPath = string.Format(@"{0}/{1}", Directory.GetCurrentDirectory(), "/tmp/");

        internal static Timer indexTimer;

        internal static Timer otpTimer;

        public static bool Wait = false;

        public static Commands CMDManager = new Commands();

        public static GUI Instance;

        public static Stopwatch upTimeSW = new Stopwatch();

        public GUI()
        {
            InitializeComponent();
            Instance = this;
        }

        private async void GUI_Shown(object sender, EventArgs e)
        {
            OPT.LoadSettings();
            DesCipher = new XDes(OPT.GetString("des.key"));

            Statistics.SetIO();
            Statistics.StartUptime();

            clientList = new Dictionary<int, Client>();

            Output.Write(new Structures.Message() { Text = "Indexing legacy file names..." });
            OPT.LoadLegacyIndex();
            Output.Write(new Structures.Message() { Text = string.Format("[OK]\n\t- {0} legacy files indexed!", OPT.LegacyCount), AddBreak = true });

            Output.Write(new Structures.Message() { Text = "Indexing delete file names..." });
            OPT.LoadDeleteFiles();
            Output.Write(new Structures.Message() { Text = string.Format("[OK]\n\t- {0} delete files indexed!", OPT.DeleteCount), AddBreak = true });

            await Task.Run(() => { IndexManager.Build(false); });

            Output.Write(new Structures.Message() { Text = "Checking for tmp directory..." });
            if (!Directory.Exists(tmpPath)) { Directory.CreateDirectory(tmpPath); }
            Output.Write(new Structures.Message() { Text = "[OK]", AddBreak = true });

            clearTmp();

            Output.Write(new Structures.Message() { Text = string.Format("Initializing client listener...{0}", (ClientManager.Instance.Start()) ? "[OK]" : "[FAIL]"), AddBreak = true });

            Output.Write(new Structures.Message() { Text = "Initializing Index Rebuild Service..." });
            int rebuildInterval = OPT.GetInt("rebuild.interval") * 1000;
            indexTimer = new Timer() { Enabled = true, Interval = rebuildInterval };
            indexTimer.Tick += indexTick;
            indexTimer.Start();
            Output.Write(new Structures.Message() { Text = "[OK]", AddBreak = true });

            Output.Write(new Structures.Message() { Text = "Initializing OTP Reset Service..." });
            otpTimer = new Timer() { Enabled = true, Interval = 300000 };
            otpTimer.Tick += otpTick;
            Output.Write(new Structures.Message() { Text = "[OK]", AddBreak = true });

            Output.Write(new Structures.Message() { Text = "Initializing Statistics Update Service..." });
            Statistics.StartUpdating();
            Output.Write(new Structures.Message() { Text = "[OK]", AddBreak = true });
        }

        private void indexTick(object sender, EventArgs e) { IndexManager.Build(true); }

        private void otpTick(object sender, EventArgs e) { Task.Run(() => { OTP.HandleOTP(); }); }

        private void clearTmp()
        {
            int cleanedCount = 0;

            Output.WriteAndLock(new Structures.Message() { Text = "\t- Cleaning up temp files..." });

            foreach (string filePath in Directory.GetFiles(tmpPath))
            {
                File.Delete(filePath);
                cleanedCount++;
            }

            Output.WriteAndUnlock(new Structures.Message() { Text = string.Format("[OK] ({0} files cleared!)", cleanedCount), AddBreak = true });
        }

        private void settingsMenu_Click(object sender, EventArgs e)
        {
            using (settingsGUI settingsGUI = new settingsGUI())
            {
                settingsGUI.Initialize();
                settingsGUI.ShowDialog();
            }
        }

        private void networkListenerMaintenance_Click(object sender, EventArgs e) { OPT.UpdateSetting("maintenance", (OPT.GetBool("maintenance") ? "0" : "1")); }

        private void updatesClearTmp_Click(object sender, EventArgs e) { clearTmp(); }

        private void updatesView_Click(object sender, EventArgs e)
        {
            using (updatesManagerGUI updatesGUI = new updatesManagerGUI())
            {
                updatesGUI.ShowDialog(this);
            }
        }
    }
}
