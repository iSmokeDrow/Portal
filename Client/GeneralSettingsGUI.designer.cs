namespace Client
{
    partial class GeneralSettingsGUI
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.showPin = new System.Windows.Forms.PictureBox();
            this.pin = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.showPassword = new System.Windows.Forms.PictureBox();
            this.remember = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fps = new System.Windows.Forms.CheckBox();
            this.onTop = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.langBox = new System.Windows.Forms.GroupBox();
            this.countryList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.codepageList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.locateClientDirectory_btn = new System.Windows.Forms.Button();
            this.clientDirectory = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.logErrors = new System.Windows.Forms.CheckBox();
            this.logReports = new System.Windows.Forms.CheckBox();
            this.closeOnStart = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.showPin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.showPassword)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.langBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.showPin);
            this.groupBox1.Controls.Add(this.pin);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.showPassword);
            this.groupBox1.Controls.Add(this.remember);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.password);
            this.groupBox1.Controls.Add(this.username);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 154);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Account";
            // 
            // showPin
            // 
            this.showPin.Image = global::Client.Properties.Resources.eye;
            this.showPin.Location = new System.Drawing.Point(31, 98);
            this.showPin.Name = "showPin";
            this.showPin.Size = new System.Drawing.Size(16, 17);
            this.showPin.TabIndex = 8;
            this.showPin.TabStop = false;
            this.showPin.Click += new System.EventHandler(this.showPin_Click);
            // 
            // pin
            // 
            this.pin.Location = new System.Drawing.Point(26, 115);
            this.pin.Name = "pin";
            this.pin.PasswordChar = '*';
            this.pin.Size = new System.Drawing.Size(75, 20);
            this.pin.TabIndex = 7;
            this.pin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 14);
            this.label6.TabIndex = 6;
            this.label6.Text = "Pin:";
            // 
            // showPassword
            // 
            this.showPassword.Image = global::Client.Properties.Resources.eye;
            this.showPassword.Location = new System.Drawing.Point(62, 59);
            this.showPassword.Name = "showPassword";
            this.showPassword.Size = new System.Drawing.Size(16, 17);
            this.showPassword.TabIndex = 5;
            this.showPassword.TabStop = false;
            this.showPassword.Click += new System.EventHandler(this.showPassword_Click);
            // 
            // remember
            // 
            this.remember.AutoSize = true;
            this.remember.Location = new System.Drawing.Point(129, 116);
            this.remember.Name = "remember";
            this.remember.Size = new System.Drawing.Size(94, 18);
            this.remember.TabIndex = 4;
            this.remember.Text = "Remember Me";
            this.remember.UseVisualStyleBackColor = true;
            this.remember.CheckedChanged += new System.EventHandler(this.remember_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(26, 78);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(198, 20);
            this.password.TabIndex = 2;
            this.password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(26, 34);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(198, 20);
            this.username.TabIndex = 1;
            this.username.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // fps
            // 
            this.fps.AutoSize = true;
            this.fps.Location = new System.Drawing.Point(40, 25);
            this.fps.Name = "fps";
            this.fps.Size = new System.Drawing.Size(77, 18);
            this.fps.TabIndex = 0;
            this.fps.Text = "Show FPS";
            this.fps.UseVisualStyleBackColor = true;
            // 
            // onTop
            // 
            this.onTop.AutoSize = true;
            this.onTop.Location = new System.Drawing.Point(122, 25);
            this.onTop.Name = "onTop";
            this.onTop.Size = new System.Drawing.Size(99, 18);
            this.onTop.TabIndex = 1;
            this.onTop.Text = "Always on Top";
            this.onTop.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.onTop);
            this.groupBox2.Controls.Add(this.fps);
            this.groupBox2.Location = new System.Drawing.Point(271, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(253, 53);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Client";
            // 
            // langBox
            // 
            this.langBox.Controls.Add(this.countryList);
            this.langBox.Controls.Add(this.label4);
            this.langBox.Controls.Add(this.codepageList);
            this.langBox.Controls.Add(this.label3);
            this.langBox.Location = new System.Drawing.Point(271, 73);
            this.langBox.Name = "langBox";
            this.langBox.Size = new System.Drawing.Size(253, 94);
            this.langBox.TabIndex = 3;
            this.langBox.TabStop = false;
            this.langBox.Text = "Language";
            // 
            // countryList
            // 
            this.countryList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.countryList.FormattingEnabled = true;
            this.countryList.Items.AddRange(new object[] {
            "US",
            "DE",
            "EU",
            "FR",
            "ME"});
            this.countryList.Location = new System.Drawing.Point(102, 54);
            this.countryList.Name = "countryList";
            this.countryList.Size = new System.Drawing.Size(121, 22);
            this.countryList.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "Country:";
            // 
            // codepageList
            // 
            this.codepageList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.codepageList.FormattingEnabled = true;
            this.codepageList.Items.AddRange(new object[] {
            "ASCII",
            "Windows-1252",
            "Windows-1256"});
            this.codepageList.Location = new System.Drawing.Point(102, 24);
            this.codepageList.Name = "codepageList";
            this.codepageList.Size = new System.Drawing.Size(121, 22);
            this.codepageList.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "Codepage:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.locateClientDirectory_btn);
            this.groupBox3.Controls.Add(this.clientDirectory);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.logErrors);
            this.groupBox3.Controls.Add(this.logReports);
            this.groupBox3.Controls.Add(this.closeOnStart);
            this.groupBox3.Location = new System.Drawing.Point(12, 173);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(408, 98);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Launcher";
            // 
            // locateClientDirectory_btn
            // 
            this.locateClientDirectory_btn.Location = new System.Drawing.Point(356, 24);
            this.locateClientDirectory_btn.Name = "locateClientDirectory_btn";
            this.locateClientDirectory_btn.Size = new System.Drawing.Size(35, 25);
            this.locateClientDirectory_btn.TabIndex = 7;
            this.locateClientDirectory_btn.Text = "...";
            this.locateClientDirectory_btn.UseVisualStyleBackColor = true;
            this.locateClientDirectory_btn.Click += new System.EventHandler(this.locateClientDirectory_btn_Click);
            // 
            // clientDirectory
            // 
            this.clientDirectory.Location = new System.Drawing.Point(93, 26);
            this.clientDirectory.Name = "clientDirectory";
            this.clientDirectory.Size = new System.Drawing.Size(257, 20);
            this.clientDirectory.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 14);
            this.label5.TabIndex = 5;
            this.label5.Text = "Client Directory:";
            // 
            // logErrors
            // 
            this.logErrors.AutoSize = true;
            this.logErrors.Location = new System.Drawing.Point(197, 66);
            this.logErrors.Name = "logErrors";
            this.logErrors.Size = new System.Drawing.Size(77, 18);
            this.logErrors.TabIndex = 4;
            this.logErrors.Text = "Log Errors";
            this.logErrors.UseVisualStyleBackColor = true;
            // 
            // logReports
            // 
            this.logReports.AutoSize = true;
            this.logReports.Location = new System.Drawing.Point(107, 66);
            this.logReports.Name = "logReports";
            this.logReports.Size = new System.Drawing.Size(85, 18);
            this.logReports.TabIndex = 3;
            this.logReports.Text = "Log Reports";
            this.logReports.UseVisualStyleBackColor = true;
            // 
            // closeOnStart
            // 
            this.closeOnStart.AutoSize = true;
            this.closeOnStart.Location = new System.Drawing.Point(9, 66);
            this.closeOnStart.Name = "closeOnStart";
            this.closeOnStart.Size = new System.Drawing.Size(94, 18);
            this.closeOnStart.TabIndex = 0;
            this.closeOnStart.Text = "Close on Start";
            this.closeOnStart.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 15.25F);
            this.label7.Location = new System.Drawing.Point(448, 247);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 24);
            this.label7.TabIndex = 5;
            this.label7.Text = "SAVE";
            this.label7.Click += new System.EventHandler(this.save_Click);
            // 
            // GeneralSettingsGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(536, 285);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.langBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GeneralSettingsGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "General Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.showPin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.showPassword)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.langBox.ResumeLayout(false);
            this.langBox.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox fps;
        private System.Windows.Forms.CheckBox onTop;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox remember;
        private System.Windows.Forms.GroupBox langBox;
        private System.Windows.Forms.ComboBox countryList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox codepageList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox showPassword;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox logErrors;
        private System.Windows.Forms.CheckBox logReports;
        private System.Windows.Forms.CheckBox closeOnStart;
        private System.Windows.Forms.Button locateClientDirectory_btn;
        private System.Windows.Forms.TextBox clientDirectory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox pin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox showPin;
        private System.Windows.Forms.Label label7;
    }
}