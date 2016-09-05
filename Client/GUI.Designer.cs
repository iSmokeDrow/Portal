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
            this.totalStatus = new System.Windows.Forms.Label();
            this.currentStatus = new System.Windows.Forms.Label();
            this.currentProgress = new System.Windows.Forms.ProgressBar();
            this.gameSettings_lb = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.close_lb = new System.Windows.Forms.Label();
            this.totalProgress = new System.Windows.Forms.ProgressBar();
            this.start_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // totalStatus
            // 
            this.totalStatus.AutoSize = true;
            this.totalStatus.BackColor = System.Drawing.Color.Transparent;
            this.totalStatus.ForeColor = System.Drawing.Color.White;
            this.totalStatus.Location = new System.Drawing.Point(13, 204);
            this.totalStatus.Name = "totalStatus";
            this.totalStatus.Size = new System.Drawing.Size(0, 13);
            this.totalStatus.TabIndex = 10;
            // 
            // currentStatus
            // 
            this.currentStatus.AutoSize = true;
            this.currentStatus.BackColor = System.Drawing.Color.Transparent;
            this.currentStatus.ForeColor = System.Drawing.Color.White;
            this.currentStatus.Location = new System.Drawing.Point(13, 236);
            this.currentStatus.Name = "currentStatus";
            this.currentStatus.Size = new System.Drawing.Size(0, 13);
            this.currentStatus.TabIndex = 12;
            // 
            // currentProgress
            // 
            this.currentProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.currentProgress.ForeColor = System.Drawing.Color.White;
            this.currentProgress.Location = new System.Drawing.Point(12, 252);
            this.currentProgress.Name = "currentProgress";
            this.currentProgress.Size = new System.Drawing.Size(566, 13);
            this.currentProgress.TabIndex = 11;
            // 
            // gameSettings_lb
            // 
            this.gameSettings_lb.AutoSize = true;
            this.gameSettings_lb.BackColor = System.Drawing.Color.Transparent;
            this.gameSettings_lb.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameSettings_lb.ForeColor = System.Drawing.Color.White;
            this.gameSettings_lb.Location = new System.Drawing.Point(153, 281);
            this.gameSettings_lb.Name = "gameSettings_lb";
            this.gameSettings_lb.Size = new System.Drawing.Size(89, 14);
            this.gameSettings_lb.TabIndex = 17;
            this.gameSettings_lb.Text = "GAME SETTINGS";
            this.gameSettings_lb.Click += new System.EventHandler(this.gameSettings_lb_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 281);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 14);
            this.label1.TabIndex = 18;
            this.label1.Text = "LAUNCHER SETTINGS";
            this.label1.Click += new System.EventHandler(this.launcherSettings_btn_Click);
            // 
            // close_lb
            // 
            this.close_lb.AutoSize = true;
            this.close_lb.BackColor = System.Drawing.Color.Transparent;
            this.close_lb.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close_lb.ForeColor = System.Drawing.Color.White;
            this.close_lb.Location = new System.Drawing.Point(567, 9);
            this.close_lb.Name = "close_lb";
            this.close_lb.Size = new System.Drawing.Size(13, 14);
            this.close_lb.TabIndex = 19;
            this.close_lb.Text = "x";
            this.close_lb.Click += new System.EventHandler(this.close_Click);
            // 
            // totalProgress
            // 
            this.totalProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.totalProgress.ForeColor = System.Drawing.Color.White;
            this.totalProgress.Location = new System.Drawing.Point(12, 220);
            this.totalProgress.Name = "totalProgress";
            this.totalProgress.Size = new System.Drawing.Size(566, 13);
            this.totalProgress.TabIndex = 21;
            // 
            // start_btn
            // 
            this.start_btn.BackColor = System.Drawing.Color.Transparent;
            this.start_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.start_btn.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.start_btn.ForeColor = System.Drawing.Color.White;
            this.start_btn.Location = new System.Drawing.Point(503, 273);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(75, 34);
            this.start_btn.TabIndex = 22;
            this.start_btn.Text = "START";
            this.start_btn.UseVisualStyleBackColor = false;
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::Client.Properties.Resources.rappelz_logo;
            this.ClientSize = new System.Drawing.Size(590, 319);
            this.Controls.Add(this.start_btn);
            this.Controls.Add(this.totalProgress);
            this.Controls.Add(this.close_lb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gameSettings_lb);
            this.Controls.Add(this.currentStatus);
            this.Controls.Add(this.currentProgress);
            this.Controls.Add(this.totalStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1013, 584);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(590, 317);
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
        private System.Windows.Forms.Label gameSettings_lb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label close_lb;
        public System.Windows.Forms.ProgressBar totalProgress;
        private System.Windows.Forms.Button start_btn;
    }
}

