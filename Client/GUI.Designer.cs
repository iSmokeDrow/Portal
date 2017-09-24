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
            this.gameSettings_lb = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.close_lb = new System.Windows.Forms.Label();
            this.start_btn = new System.Windows.Forms.PictureBox();
            this.totalProgress = new ProgressODoom.ProgressBarEx();
            this.bgPainter = new ProgressODoom.PlainBackgroundPainter();
            this.glossPainter = new ProgressODoom.MiddleGlossPainter();
            this.borderPainter = new ProgressODoom.PlainBorderPainter();
            this.pgPainter = new ProgressODoom.MetalProgressPainter();
            this.currentProgress = new ProgressODoom.ProgressBarEx();
            ((System.ComponentModel.ISupportInitialize)(this.start_btn)).BeginInit();
            this.SuspendLayout();
            // 
            // totalStatus
            // 
            this.totalStatus.AutoSize = true;
            this.totalStatus.BackColor = System.Drawing.Color.Transparent;
            this.totalStatus.ForeColor = System.Drawing.Color.White;
            this.totalStatus.Location = new System.Drawing.Point(88, 499);
            this.totalStatus.Name = "totalStatus";
            this.totalStatus.Size = new System.Drawing.Size(0, 13);
            this.totalStatus.TabIndex = 10;
            // 
            // currentStatus
            // 
            this.currentStatus.AutoSize = true;
            this.currentStatus.BackColor = System.Drawing.Color.Transparent;
            this.currentStatus.ForeColor = System.Drawing.Color.White;
            this.currentStatus.Location = new System.Drawing.Point(88, 546);
            this.currentStatus.Name = "currentStatus";
            this.currentStatus.Size = new System.Drawing.Size(0, 13);
            this.currentStatus.TabIndex = 12;
            // 
            // gameSettings_lb
            // 
            this.gameSettings_lb.AutoSize = true;
            this.gameSettings_lb.BackColor = System.Drawing.Color.Transparent;
            this.gameSettings_lb.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameSettings_lb.ForeColor = System.Drawing.Color.White;
            this.gameSettings_lb.Location = new System.Drawing.Point(150, 9);
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
            this.label1.Location = new System.Drawing.Point(12, 9);
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
            this.close_lb.Location = new System.Drawing.Point(974, 9);
            this.close_lb.Name = "close_lb";
            this.close_lb.Size = new System.Drawing.Size(13, 14);
            this.close_lb.TabIndex = 19;
            this.close_lb.Text = "x";
            this.close_lb.Click += new System.EventHandler(this.close_Click);
            // 
            // start_btn
            // 
            this.start_btn.BackColor = System.Drawing.Color.Transparent;
            this.start_btn.Image = global::Client.Properties.Resources.start_off;
            this.start_btn.InitialImage = global::Client.Properties.Resources.start_off;
            this.start_btn.Location = new System.Drawing.Point(646, 514);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(314, 70);
            this.start_btn.TabIndex = 25;
            this.start_btn.TabStop = false;
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // totalProgress
            // 
            this.totalProgress.BackgroundPainter = this.bgPainter;
            this.totalProgress.BorderPainter = this.borderPainter;
            this.totalProgress.Location = new System.Drawing.Point(83, 527);
            this.totalProgress.MarqueePercentage = 25;
            this.totalProgress.MarqueeSpeed = 30;
            this.totalProgress.MarqueeStep = 1;
            this.totalProgress.Maximum = 100;
            this.totalProgress.Minimum = 0;
            this.totalProgress.Name = "totalProgress";
            this.totalProgress.ProgressPadding = 0;
            this.totalProgress.ProgressPainter = this.pgPainter;
            this.totalProgress.ProgressType = ProgressODoom.ProgressType.Smooth;
            this.totalProgress.ShowPercentage = false;
            this.totalProgress.Size = new System.Drawing.Size(518, 17);
            this.totalProgress.TabIndex = 26;
            this.totalProgress.Value = 0;
            // 
            // bgPainter
            // 
            this.bgPainter.Color = System.Drawing.Color.Black;
            this.bgPainter.GlossPainter = this.glossPainter;
            // 
            // glossPainter
            // 
            this.glossPainter.AlphaHigh = 175;
            this.glossPainter.AlphaLow = 0;
            this.glossPainter.Color = System.Drawing.Color.Maroon;
            this.glossPainter.Style = ProgressODoom.GlossStyle.Both;
            this.glossPainter.Successor = null;
            this.glossPainter.TaperHeight = 8;
            // 
            // borderPainter
            // 
            this.borderPainter.Color = System.Drawing.SystemColors.ActiveCaptionText;
            this.borderPainter.RoundedCorners = true;
            this.borderPainter.Style = ProgressODoom.PlainBorderPainter.PlainBorderStyle.Flat;
            // 
            // pgPainter
            // 
            this.pgPainter.Color = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(94)))), ((int)(((byte)(1)))));
            this.pgPainter.GlossPainter = this.glossPainter;
            this.pgPainter.Highlight = System.Drawing.Color.DarkRed;
            this.pgPainter.ProgressBorderPainter = null;
            // 
            // currentProgress
            // 
            this.currentProgress.BackgroundPainter = this.bgPainter;
            this.currentProgress.BorderPainter = this.borderPainter;
            this.currentProgress.Location = new System.Drawing.Point(83, 560);
            this.currentProgress.MarqueePercentage = 25;
            this.currentProgress.MarqueeSpeed = 30;
            this.currentProgress.MarqueeStep = 1;
            this.currentProgress.Maximum = 100;
            this.currentProgress.Minimum = 0;
            this.currentProgress.Name = "currentProgress";
            this.currentProgress.ProgressPadding = 0;
            this.currentProgress.ProgressPainter = this.pgPainter;
            this.currentProgress.ProgressType = ProgressODoom.ProgressType.Smooth;
            this.currentProgress.ShowPercentage = false;
            this.currentProgress.Size = new System.Drawing.Size(518, 17);
            this.currentProgress.TabIndex = 27;
            this.currentProgress.Value = 0;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::Client.Properties.Resources.bg_new;
            this.ClientSize = new System.Drawing.Size(999, 600);
            this.Controls.Add(this.currentProgress);
            this.Controls.Add(this.totalProgress);
            this.Controls.Add(this.start_btn);
            this.Controls.Add(this.close_lb);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gameSettings_lb);
            this.Controls.Add(this.currentStatus);
            this.Controls.Add(this.totalStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1013, 600);
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
            ((System.ComponentModel.ISupportInitialize)(this.start_btn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Label totalStatus;
        public System.Windows.Forms.Label currentStatus;
        private System.Windows.Forms.Label gameSettings_lb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label close_lb;
        private System.Windows.Forms.PictureBox start_btn;
        private ProgressODoom.ProgressBarEx totalProgress;
        private ProgressODoom.ProgressBarEx currentProgress;
        private ProgressODoom.MetalProgressPainter pgPainter;
        private ProgressODoom.MiddleGlossPainter glossPainter;
        private ProgressODoom.PlainBackgroundPainter bgPainter;
        private ProgressODoom.PlainBorderPainter borderPainter;
    }
}

