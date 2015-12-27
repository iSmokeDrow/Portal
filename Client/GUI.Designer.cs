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
            this.totalProgress = new System.Windows.Forms.ProgressBar();
            this.currentProgress = new System.Windows.Forms.ProgressBar();
            this.start = new System.Windows.Forms.Button();
            this.totalStatus = new System.Windows.Forms.Label();
            this.currentStatus = new System.Windows.Forms.Label();
            this.browser = new Awesomium.Windows.Forms.WebControl(this.components);
            this.SuspendLayout();
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(703, 12);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(26, 23);
            this.close.TabIndex = 0;
            this.close.Text = "X";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // totalProgress
            // 
            this.totalProgress.Location = new System.Drawing.Point(12, 377);
            this.totalProgress.Name = "totalProgress";
            this.totalProgress.Size = new System.Drawing.Size(600, 13);
            this.totalProgress.TabIndex = 2;
            // 
            // currentProgress
            // 
            this.currentProgress.Location = new System.Drawing.Point(12, 410);
            this.currentProgress.Name = "currentProgress";
            this.currentProgress.Size = new System.Drawing.Size(600, 13);
            this.currentProgress.TabIndex = 3;
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(627, 377);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(102, 46);
            this.start.TabIndex = 4;
            this.start.Text = "START";
            this.start.UseVisualStyleBackColor = true;
            // 
            // totalStatus
            // 
            this.totalStatus.AutoSize = true;
            this.totalStatus.Location = new System.Drawing.Point(12, 362);
            this.totalStatus.Name = "totalStatus";
            this.totalStatus.Size = new System.Drawing.Size(0, 13);
            this.totalStatus.TabIndex = 5;
            // 
            // currentStatus
            // 
            this.currentStatus.AutoSize = true;
            this.currentStatus.Location = new System.Drawing.Point(12, 394);
            this.currentStatus.Name = "currentStatus";
            this.currentStatus.Size = new System.Drawing.Size(0, 13);
            this.currentStatus.TabIndex = 6;
            // 
            // browser
            // 
            this.browser.Location = new System.Drawing.Point(15, 38);
            this.browser.Size = new System.Drawing.Size(714, 317);
            this.browser.TabIndex = 7;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(741, 435);
            this.Controls.Add(this.browser);
            this.Controls.Add(this.currentStatus);
            this.Controls.Add(this.totalStatus);
            this.Controls.Add(this.start);
            this.Controls.Add(this.currentProgress);
            this.Controls.Add(this.totalProgress);
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
        private System.Windows.Forms.ProgressBar totalProgress;
        private System.Windows.Forms.ProgressBar currentProgress;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Label totalStatus;
        private System.Windows.Forms.Label currentStatus;
        private Awesomium.Windows.Forms.WebControl browser;
    }
}

