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

namespace Client
{
    public partial class GeneralSettingsGUI : Form
    {
        public UserSettings userSettings;

        public GeneralSettingsGUI(UserSettings settings)
        {
            InitializeComponent();

            userSettings = settings;
            username.Text = userSettings.Username;
            password.Text = userSettings.Password;
            pin.Text = userSettings.Pin;
            remember.Checked = userSettings.Remember;
            fps.Checked = userSettings.ShowFPS;
            onTop.Checked = userSettings.AlwaysOnTop;
            codepageList.Text = userSettings.Codepage;
            countryList.Text = userSettings.Country;
            closeOnStart.Checked = userSettings.CloseOnStart;
            logReports.Checked = userSettings.LogReports;
            logErrors.Checked = userSettings.LogErrors;
            clientDirectory.Text = userSettings.ClientDirectory;
        }

        private void save_Click(object sender, EventArgs e)
        {
            userSettings.Username = username.Text;
            userSettings.Password = password.Text;
            userSettings.Pin = pin.Text;
            userSettings.Remember = remember.Checked;
            userSettings.ShowFPS = fps.Checked;
            userSettings.AlwaysOnTop = onTop.Checked;
            userSettings.Codepage = codepageList.Text;
            userSettings.Country = countryList.Text;
            userSettings.CloseOnStart = closeOnStart.Checked;
            userSettings.LogReports = logReports.Checked;
            userSettings.LogErrors = logErrors.Checked;
            userSettings.ClientDirectory = clientDirectory.Text;
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
                Properties.Settings.Default.username = string.Empty;
                Properties.Settings.Default.password = string.Empty;
                Properties.Settings.Default.pin = string.Empty;
                Properties.Settings.Default.remember = false;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
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
