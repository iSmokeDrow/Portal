using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Client.Structures;
using Client.Functions;

namespace Client
{
    public partial class GeneralSettingsGUI : Form
    {
        public UserSettings userSettings;
        public GUI guiInstance = GUI.Instance;

        public GeneralSettingsGUI()
        {
            InitializeComponent();

            fps.Checked = guiInstance.SettingsManager.GetBool("showfps");
            onTop.Checked = guiInstance.SettingsManager.GetBool("ontop");
            codepageList.Text = guiInstance.SettingsManager.GetString("codepage");
            countryList.Text = guiInstance.SettingsManager.GetString("country");
            closeOnStart.Checked = guiInstance.SettingsManager.GetBool("closeonstart");
            logReports.Checked = guiInstance.SettingsManager.GetBool("logreports");
            logErrors.Checked = guiInstance.SettingsManager.GetBool("logerrors");
            clientDirectory.Text = guiInstance.SettingsManager.GetString("clientdirectory");
        }

        private void save_Click(object sender, EventArgs e)
        {
            guiInstance.SettingsManager.Update("showfps", fps.Checked);
            guiInstance.SettingsManager.Update("ontop", onTop.Checked);
            guiInstance.SettingsManager.Update("codepage", codepageList.Text);
            guiInstance.SettingsManager.Update("country", countryList.Text);
            guiInstance.SettingsManager.Update("closeonstart", closeOnStart.Checked);
            guiInstance.SettingsManager.Update("logreports", logReports.Checked);
            guiInstance.SettingsManager.Update("logerrors", logErrors.Checked);
            guiInstance.SettingsManager.Update("clientdirectory", clientDirectory.Text);
            guiInstance.SettingsManager.writeOPT();
            this.Close();
        }

        private void locateClientDirectory_btn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbDlg = new FolderBrowserDialog())
            {
                fbDlg.Description = "Select Rappelz Client Directory";
                fbDlg.ShowDialog();

                if (fbDlg.SelectedPath != null && Directory.Exists(fbDlg.SelectedPath)) { clientDirectory.Text = fbDlg.SelectedPath; }
            }
        }
    }
}
