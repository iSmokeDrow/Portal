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

            username.Text = guiInstance.SettingsManager.GetStringValue("username");
            password.Text = guiInstance.SettingsManager.GetStringValue("password");
            pin.Text = guiInstance.SettingsManager.GetStringValue("pin");
            remember.Checked = guiInstance.SettingsManager.GetBoolValue("remember");
            fps.Checked = guiInstance.SettingsManager.GetBoolValue("showfps");
            onTop.Checked = guiInstance.SettingsManager.GetBoolValue("ontop");
            codepageList.Text = guiInstance.SettingsManager.GetStringValue("codepage");
            countryList.Text = guiInstance.SettingsManager.GetStringValue("country");
            closeOnStart.Checked = guiInstance.SettingsManager.GetBoolValue("closeonstart");
            logReports.Checked = guiInstance.SettingsManager.GetBoolValue("logreports");
            logErrors.Checked = guiInstance.SettingsManager.GetBoolValue("logerrors");
            clientDirectory.Text = guiInstance.SettingsManager.GetStringValue("clientdirectory");
        }

        private void save_Click(object sender, EventArgs e)
        {
            guiInstance.SettingsManager.UpdateValue("username", username.Text);
            guiInstance.SettingsManager.UpdateValue("password", password.Text);
            guiInstance.SettingsManager.UpdateValue("pin", pin.Text);
            guiInstance.SettingsManager.UpdateValue("remember", remember.Checked);
            guiInstance.SettingsManager.UpdateValue("showfps", fps.Checked);
            guiInstance.SettingsManager.UpdateValue("ontop", onTop.Checked);
            guiInstance.SettingsManager.UpdateValue("codepage", codepageList.Text);
            guiInstance.SettingsManager.UpdateValue("country", countryList.Text);
            guiInstance.SettingsManager.UpdateValue("closeonstart", closeOnStart.Checked);
            guiInstance.SettingsManager.UpdateValue("logreports", logReports.Checked);
            guiInstance.SettingsManager.UpdateValue("logerrors", logErrors.Checked);
            guiInstance.SettingsManager.UpdateValue("clientdirectory", clientDirectory.Text);
            guiInstance.SettingsManager.writeOPT();
            this.Close();
        }

        private void showPassword_Click(object sender, EventArgs e)
        {
            if (password.PasswordChar == '*' ) { password.PasswordChar = '\0'; }
            else { password.PasswordChar = '*'; }
        }

        private void remember_CheckedChanged(object sender, EventArgs e)
        {
            if (!remember.Checked)
            {
                guiInstance.SettingsManager.UpdateValue("username", "");
                guiInstance.SettingsManager.UpdateValue("password", "");
                guiInstance.SettingsManager.UpdateValue("pin", "");
                guiInstance.SettingsManager.UpdateValue("remember", remember.Checked);
            }
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

        private void showPin_Click(object sender, EventArgs e)
        {
            if (pin.PasswordChar == '*') { pin.PasswordChar = '\0'; }
            else { pin.PasswordChar = '*'; }
        }
    }
}
