using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class LoginGUI : Form
    {
        public GUI guiInstance = GUI.Instance;
        public bool Cancelled = false;

        public LoginGUI()
        {
            InitializeComponent();
            this.FormClosing += (o, x) =>
            {
                guiInstance.SettingsManager.UpdateValue("username", username.Text);
                guiInstance.SettingsManager.UpdateValue("password", password.Text);
                guiInstance.SettingsManager.UpdateValue("pin", pin.Text);
                guiInstance.SettingsManager.UpdateValue("remember", true);

                if (rememberCredentials.Checked) { guiInstance.SettingsManager.writeOPT(); }
            };

            if (guiInstance.SettingsManager.GetBoolValue("remember"))
            {
                username.Text = guiInstance.SettingsManager.GetStringValue("username");
                password.Text = guiInstance.SettingsManager.GetStringValue("password");
                pin.Text = guiInstance.SettingsManager.GetStringValue("pin");
                rememberCredentials.Checked = guiInstance.SettingsManager.GetBoolValue("remember");
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Cancelled = true;
            this.Close();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (username.Text.Length > 0 && password.Text.Length > 0 && pin.Text.Length > 0)
            {
                this.Hide();
                GUI.Instance.Login(username.Text, password.Text, pin.Text);
                this.Close();
            }
            else { MessageBox.Show("You have not entered valid login information!\nPlease fill in all text boxes with proper infromation to continue!", "Login Exception", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void LoginGUI_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}
