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
            this.totalStatus = new System.Windows.Forms.Label();
            this.currentStatus = new System.Windows.Forms.Label();
            this.currentProgress = new System.Windows.Forms.ProgressBar();
            this.start_lb = new System.Windows.Forms.Label();
            this.gameSettings_lb = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.close_lb = new System.Windows.Forms.Label();
            this.browser = new Awesomium.Windows.Forms.WebControl(this.components);
            this.totalProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // totalStatus
            // 
            this.totalStatus.AutoSize = true;
            this.totalStatus.ForeColor = System.Drawing.Color.White;
            this.totalStatus.Location = new System.Drawing.Point(12, 484);
            this.totalStatus.Name = "totalStatus";
            this.totalStatus.Size = new System.Drawing.Size(0, 13);
            this.totalStatus.TabIndex = 10;
            // 
            // currentStatus
            // 
            this.currentStatus.AutoSize = true;
            this.currentStatus.ForeColor = System.Drawing.Color.White;
            this.currentStatus.Location = new System.Drawing.Point(11, 514);
            this.currentStatus.Name = "currentStatus";
            this.currentStatus.Size = new System.Drawing.Size(0, 13);
            this.currentStatus.TabIndex = 12;
            // 
            // currentProgress
            // 
            this.currentProgress.ForeColor = System.Drawing.Color.White;
            this.currentProgress.Location = new System.Drawing.Point(10, 532);
            this.currentProgress.Name = "currentProgress";
            this.currentProgress.Size = new System.Drawing.Size(992, 13);
            this.currentProgress.TabIndex = 11;
            // 
            // start_lb
            // 
            this.start_lb.AutoSize = true;
            this.start_lb.Font = new System.Drawing.Font("Arial", 14F);
            this.start_lb.ForeColor = System.Drawing.Color.White;
            this.start_lb.Location = new System.Drawing.Point(929, 554);
            this.start_lb.Name = "start_lb";
            this.start_lb.Size = new System.Drawing.Size(73, 22);
            this.start_lb.TabIndex = 16;
            this.start_lb.Text = "START";
            this.start_lb.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // gameSettings_lb
            // 
            this.gameSettings_lb.AutoSize = true;
            this.gameSettings_lb.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameSettings_lb.ForeColor = System.Drawing.Color.White;
            this.gameSettings_lb.Location = new System.Drawing.Point(151, 561);
            this.gameSettings_lb.Name = "gameSettings_lb";
            this.gameSettings_lb.Size = new System.Drawing.Size(89, 14);
            this.gameSettings_lb.TabIndex = 17;
            this.gameSettings_lb.Text = "GAME SETTINGS";
            this.gameSettings_lb.Click += new System.EventHandler(this.gameSettings_lb_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 561);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 14);
            this.label1.TabIndex = 18;
            this.label1.Text = "LAUNCHER SETTINGS";
            this.label1.Click += new System.EventHandler(this.launcherSettings_btn_Click);
            // 
            // close_lb
            // 
            this.close_lb.AutoSize = true;
            this.close_lb.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close_lb.ForeColor = System.Drawing.Color.White;
            this.close_lb.Location = new System.Drawing.Point(997, 0);
            this.close_lb.Name = "close_lb";
            this.close_lb.Size = new System.Drawing.Size(13, 14);
            this.close_lb.TabIndex = 19;
            this.close_lb.Text = "x";
            this.close_lb.Click += new System.EventHandler(this.close_Click);
            // 
            // browser
            // 
            this.browser.Location = new System.Drawing.Point(2, 21);
            this.browser.Size = new System.Drawing.Size(1008, 460);
            this.browser.TabIndex = 20;
            // 
            // totalProgress
            // 
            this.totalProgress.ForeColor = System.Drawing.Color.White;
            this.totalProgress.Location = new System.Drawing.Point(10, 500);
            this.totalProgress.Name = "totalProgress";
            this.totalProgress.Size = new System.Drawing.Size(992, 13);
            this.totalProgress.TabIndex = 21;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(1013, 584);
            this.Controls.Add(this.totalProgress);
            this.Controls.Add(this.browser);
            this.Controls.Add(this.close_lb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gameSettings_lb);
            this.Controls.Add(this.start_lb);
            this.Controls.Add(this.currentStatus);
            this.Controls.Add(this.currentProgress);
            this.Controls.Add(this.totalStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1013, 584);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1013, 584);
            this.Name = "GUI";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Horizons Launcher";
            this.Load += new System.EventHandler(this.GUI_Load);
            this.Shown += new System.EventHandler(this.GUI_Shown);
            this.DoubleClick += new System.EventHandler(this.GUI_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GUI_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GUI_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GUI_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label totalStatus;
        public System.Windows.Forms.Label currentStatus;
        public System.Windows.Forms.ProgressBar currentProgress;
        private System.Windows.Forms.Label start_lb;
        private System.Windows.Forms.Label gameSettings_lb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label close_lb;
        private Awesomium.Windows.Forms.WebControl browser;
        public System.Windows.Forms.ProgressBar totalProgress;
    }
}

