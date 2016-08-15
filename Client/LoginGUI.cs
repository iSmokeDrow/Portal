using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Functions;

namespace Client
{
    public partial class LoginGUI : Form
    {
        public GUI guiInstance = GUI.Instance;
        public bool Cancelled = false;
        protected OPT settings = OPT.Instance;

        public LoginGUI()
        {
            InitializeComponent();
            this.FormClosing += (o, x) =>
            {
                if (username.Text.Length > 0 && password.Text.Length > 0)
                {
                    settings.Update("username", username.Text);
                    settings.Update("password", password.Text);
                    settings.Update("remember", rememberCredentials.Checked);
                }

                if (rememberCredentials.Checked) { settings.writeOPT(); }
            };

            if (settings.GetBool("remember"))
            {
                username.Text = settings.GetString("username");
                password.Text = settings.GetString("password");
                rememberCredentials.Checked = settings.GetBool("remember");
            }
        }

        public string Username
        {
            get { return (username.Text.Length > 0) ? username.Text : null; }
        }

        public string Password
        {
            get { return (password.Text.Length > 0) ? password.Text : null; }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Cancelled = true;
            this.Close();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (username.Text.Length > 0 && password.Text.Length > 0)
            {
                this.Hide();
            }
            else { MessageBox.Show("You have not entered valid login information!\nPlease fill in all text boxes with proper infromation to continue!", "Login Exception", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void LoginGUI_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}