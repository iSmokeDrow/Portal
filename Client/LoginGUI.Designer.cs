namespace Client
{
    partial class LoginGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.rememberCredentials = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.cancel_lb = new System.Windows.Forms.Label();
            this.login_lb = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(15, 25);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(187, 20);
            this.username.TabIndex = 1;
            // 
            // rememberCredentials
            // 
            this.rememberCredentials.AutoSize = true;
            this.rememberCredentials.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rememberCredentials.ForeColor = System.Drawing.Color.White;
            this.rememberCredentials.Location = new System.Drawing.Point(107, 103);
            this.rememberCredentials.Name = "rememberCredentials";
            this.rememberCredentials.Size = new System.Drawing.Size(94, 18);
            this.rememberCredentials.TabIndex = 4;
            this.rememberCredentials.Text = "Remember Me";
            this.rememberCredentials.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(15, 64);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(187, 20);
            this.password.TabIndex = 2;
            // 
            // cancel_lb
            // 
            this.cancel_lb.AutoSize = true;
            this.cancel_lb.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancel_lb.ForeColor = System.Drawing.Color.White;
            this.cancel_lb.Location = new System.Drawing.Point(12, 147);
            this.cancel_lb.Name = "cancel_lb";
            this.cancel_lb.Size = new System.Drawing.Size(48, 16);
            this.cancel_lb.TabIndex = 6;
            this.cancel_lb.Text = "Cancel";
            this.cancel_lb.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // login_lb
            // 
            this.login_lb.AutoSize = true;
            this.login_lb.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_lb.ForeColor = System.Drawing.Color.White;
            this.login_lb.Location = new System.Drawing.Point(162, 147);
            this.login_lb.Name = "login_lb";
            this.login_lb.Size = new System.Drawing.Size(39, 16);
            this.login_lb.TabIndex = 5;
            this.login_lb.Text = "Login";
            this.login_lb.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // LoginGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(219, 172);
            this.ControlBox = false;
            this.Controls.Add(this.login_lb);
            this.Controls.Add(this.cancel_lb);
            this.Controls.Add(this.password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rememberCredentials);
            this.Controls.Add(this.username);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enter Credentials";
            this.DoubleClick += new System.EventHandler(this.LoginGUI_DoubleClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox rememberCredentials;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label cancel_lb;
        private System.Windows.Forms.Label login_lb;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox password;
    }
}