namespace Client
{
    partial class GUI
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
            this.components = new System.ComponentModel.Container();
            this.close = new System.Windows.Forms.Button();
            this.browser = new Awesomium.Windows.Forms.WebControl(this.components);
            this.totalProgress = new System.Windows.Forms.ProgressBar();
            this.start_btn = new System.Windows.Forms.Button();
            this.totalStatus = new System.Windows.Forms.Label();
            this.currentStatus = new System.Windows.Forms.Label();
            this.currentProgress = new System.Windows.Forms.ProgressBar();
            this.sessionProvider = new Awesomium.Windows.Forms.WebSessionProvider(this.components);
            this.launcherSettings_btn = new System.Windows.Forms.Button();
            this.gameSettings_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(962, 12);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(26, 23);
            this.close.TabIndex = 0;
            this.close.Text = "X";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // browser
            // 
            this.browser.Location = new System.Drawing.Point(12, 45);
            this.browser.Size = new System.Drawing.Size(973, 559);
            this.browser.TabIndex = 7;
            // 
            // totalProgress
            // 
            this.totalProgress.Location = new System.Drawing.Point(153, 632);
            this.totalProgress.Name = "totalProgress";
            this.totalProgress.Size = new System.Drawing.Size(696, 13);
            this.totalProgress.TabIndex = 8;
            // 
            // start_btn
            // 
            this.start_btn.Location = new System.Drawing.Point(867, 626);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(121, 56);
            this.start_btn.TabIndex = 9;
            this.start_btn.Text = "START";
            this.start_btn.UseVisualStyleBackColor = true;
            // 
            // totalStatus
            // 
            this.totalStatus.AutoSize = true;
            this.totalStatus.Location = new System.Drawing.Point(155, 616);
            this.totalStatus.Name = "totalStatus";
            this.totalStatus.Size = new System.Drawing.Size(0, 13);
            this.totalStatus.TabIndex = 10;
            // 
            // currentStatus
            // 
            this.currentStatus.AutoSize = true;
            this.currentStatus.Location = new System.Drawing.Point(154, 648);
            this.currentStatus.Name = "currentStatus";
            this.currentStatus.Size = new System.Drawing.Size(0, 13);
            this.currentStatus.TabIndex = 12;
            // 
            // currentProgress
            // 
            this.currentProgress.Location = new System.Drawing.Point(153, 664);
            this.currentProgress.Name = "currentProgress";
            this.currentProgress.Size = new System.Drawing.Size(696, 13);
            this.currentProgress.TabIndex = 11;
            // 
            // sessionProvider
            // 
            this.sessionProvider.DataPath = "F:\\Users\\Switch\\Documents\\Visual Studio 2015\\Projects\\Portal\\Client\\bin\\Debug\\Res" +
    "ource";
            this.sessionProvider.Views.Add(this.browser);
            // 
            // launcherSettings_btn
            // 
            this.launcherSettings_btn.Location = new System.Drawing.Point(12, 626);
            this.launcherSettings_btn.Name = "launcherSettings_btn";
            this.launcherSettings_btn.Size = new System.Drawing.Size(121, 27);
            this.launcherSettings_btn.TabIndex = 13;
            this.launcherSettings_btn.Text = "Launcher Settings";
            this.launcherSettings_btn.UseVisualStyleBackColor = true;
            this.launcherSettings_btn.Click += new System.EventHandler(this.launcherSettings_btn_Click);
            // 
            // gameSettings_btn
            // 
            this.gameSettings_btn.Location = new System.Drawing.Point(12, 655);
            this.gameSettings_btn.Name = "gameSettings_btn";
            this.gameSettings_btn.Size = new System.Drawing.Size(121, 27);
            this.gameSettings_btn.TabIndex = 14;
            this.gameSettings_btn.Text = "Game Settings";
            this.gameSettings_btn.UseVisualStyleBackColor = true;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.gameSettings_btn);
            this.Controls.Add(this.launcherSettings_btn);
            this.Controls.Add(this.currentStatus);
            this.Controls.Add(this.currentProgress);
            this.Controls.Add(this.totalStatus);
            this.Controls.Add(this.start_btn);
            this.Controls.Add(this.totalProgress);
            this.Controls.Add(this.browser);
            this.Controls.Add(this.close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GUI";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.GUI_Load);
            this.Shown += new System.EventHandler(this.GUI_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button close;
        private Awesomium.Windows.Forms.WebControl browser;
        private System.Windows.Forms.Button start_btn;
        private Awesomium.Windows.Forms.WebSessionProvider sessionProvider;
        private System.Windows.Forms.Button launcherSettings_btn;
        private System.Windows.Forms.Button gameSettings_btn;
        public System.Windows.Forms.ProgressBar totalProgress;
        public System.Windows.Forms.Label totalStatus;
        public System.Windows.Forms.Label currentStatus;
        public System.Windows.Forms.ProgressBar currentProgress;
    }
}

