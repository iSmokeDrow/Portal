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

            ip.Text = OPT.Instance.GetString("ip");
            port.Text = OPT.Instance.GetString("port");
            fps.Checked = OPT.Instance.GetBool("showfps");
            onTop.Checked = OPT.Instance.GetBool("ontop");
            codepageList.Text = OPT.Instance.GetString("codepage");
            countryList.Text = OPT.Instance.GetString("country");
            closeOnStart.Checked = OPT.Instance.GetBool("closeonstart");
            logReports.Checked = OPT.Instance.GetBool("logreports");
            logErrors.Checked = OPT.Instance.GetBool("logerrors");
            clientDirectory.Text = OPT.Instance.GetString("clientdirectory");
        }

        private void save_Click(object sender, EventArgs e)
        {
            OPT.Instance.Update("ip", ip.Text);
            OPT.Instance.Update("port", port.Text);
            OPT.Instance.Update("showfps", fps.Checked);
            OPT.Instance.Update("ontop", onTop.Checked);
            OPT.Instance.Update("codepage", codepageList.Text);
            OPT.Instance.Update("country", countryList.Text);
            OPT.Instance.Update("closeonstart", closeOnStart.Checked);
            OPT.Instance.Update("logreports", logReports.Checked);
            OPT.Instance.Update("logerrors", logErrors.Checked);
            OPT.Instance.Update("clientdirectory", clientDirectory.Text);
            OPT.Instance.writeOPT();
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
