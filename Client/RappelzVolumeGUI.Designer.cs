namespace Client
{
    partial class RappelzVolumeGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RappelzVolumeGUI));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.overallMute = new System.Windows.Forms.CheckBox();
            this.sfxMute = new System.Windows.Forms.CheckBox();
            this.musicMute = new System.Windows.Forms.CheckBox();
            this.environmentalMute = new System.Windows.Forms.CheckBox();
            this.disableLobbyTheme = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.musicRepeat = new System.Windows.Forms.CheckBox();
            this.environmentalVolume = new LBSoft.IndustrialCtrls.Knobs.LBKnob();
            this.musicVolume = new LBSoft.IndustrialCtrls.Knobs.LBKnob();
            this.sfxVolume = new LBSoft.IndustrialCtrls.Knobs.LBKnob();
            this.overallVolume = new LBSoft.IndustrialCtrls.Knobs.LBKnob();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(21, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Overall Volume";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(218, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "SFX Volume";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(24, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Music Volume";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(199, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Ambient Volume";
            // 
            // overallMute
            // 
            this.overallMute.AutoSize = true;
            this.overallMute.ForeColor = System.Drawing.Color.White;
            this.overallMute.Location = new System.Drawing.Point(121, 111);
            this.overallMute.Name = "overallMute";
            this.overallMute.Size = new System.Drawing.Size(50, 17);
            this.overallMute.TabIndex = 8;
            this.overallMute.Text = "Mute";
            this.overallMute.UseVisualStyleBackColor = true;
            this.overallMute.CheckedChanged += new System.EventHandler(this.overallMute_CheckedChanged);
            // 
            // sfxMute
            // 
            this.sfxMute.AutoSize = true;
            this.sfxMute.ForeColor = System.Drawing.Color.White;
            this.sfxMute.Location = new System.Drawing.Point(297, 111);
            this.sfxMute.Name = "sfxMute";
            this.sfxMute.Size = new System.Drawing.Size(50, 17);
            this.sfxMute.TabIndex = 9;
            this.sfxMute.Text = "Mute";
            this.sfxMute.UseVisualStyleBackColor = true;
            this.sfxMute.CheckedChanged += new System.EventHandler(this.sfxMute_CheckedChanged);
            // 
            // musicMute
            // 
            this.musicMute.AutoSize = true;
            this.musicMute.ForeColor = System.Drawing.Color.White;
            this.musicMute.Location = new System.Drawing.Point(121, 258);
            this.musicMute.Name = "musicMute";
            this.musicMute.Size = new System.Drawing.Size(50, 17);
            this.musicMute.TabIndex = 10;
            this.musicMute.Text = "Mute";
            this.musicMute.UseVisualStyleBackColor = true;
            this.musicMute.CheckedChanged += new System.EventHandler(this.musicMute_CheckedChanged);
            // 
            // environmentalMute
            // 
            this.environmentalMute.AutoSize = true;
            this.environmentalMute.ForeColor = System.Drawing.Color.White;
            this.environmentalMute.Location = new System.Drawing.Point(297, 258);
            this.environmentalMute.Name = "environmentalMute";
            this.environmentalMute.Size = new System.Drawing.Size(50, 17);
            this.environmentalMute.TabIndex = 11;
            this.environmentalMute.Text = "Mute";
            this.environmentalMute.UseVisualStyleBackColor = true;
            this.environmentalMute.CheckedChanged += new System.EventHandler(this.environmentalMute_CheckedChanged);
            // 
            // disableLobbyTheme
            // 
            this.disableLobbyTheme.AutoSize = true;
            this.disableLobbyTheme.ForeColor = System.Drawing.Color.White;
            this.disableLobbyTheme.Location = new System.Drawing.Point(21, 344);
            this.disableLobbyTheme.Name = "disableLobbyTheme";
            this.disableLobbyTheme.Size = new System.Drawing.Size(91, 17);
            this.disableLobbyTheme.TabIndex = 12;
            this.disableLobbyTheme.Text = "Lobby Theme";
            this.disableLobbyTheme.UseVisualStyleBackColor = true;
            this.disableLobbyTheme.CheckedChanged += new System.EventHandler(this.disableLobbyTheme_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(283, 338);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 22);
            this.label5.TabIndex = 13;
            this.label5.Text = "SAVE";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // musicRepeat
            // 
            this.musicRepeat.AutoSize = true;
            this.musicRepeat.ForeColor = System.Drawing.Color.White;
            this.musicRepeat.Location = new System.Drawing.Point(121, 235);
            this.musicRepeat.Name = "musicRepeat";
            this.musicRepeat.Size = new System.Drawing.Size(61, 17);
            this.musicRepeat.TabIndex = 14;
            this.musicRepeat.Text = "Repeat";
            this.musicRepeat.UseVisualStyleBackColor = true;
            this.musicRepeat.CheckedChanged += new System.EventHandler(this.musicRepeat_CheckedChanged);
            // 
            // environmentalVolume
            // 
            this.environmentalVolume.BackColor = System.Drawing.Color.Transparent;
            this.environmentalVolume.DrawRatio = 0.41F;
            this.environmentalVolume.IndicatorColor = System.Drawing.Color.White;
            this.environmentalVolume.IndicatorOffset = 10F;
            this.environmentalVolume.KnobCenter = ((System.Drawing.PointF)(resources.GetObject("environmentalVolume.KnobCenter")));
            this.environmentalVolume.KnobColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.environmentalVolume.KnobRect = ((System.Drawing.RectangleF)(resources.GetObject("environmentalVolume.KnobRect")));
            this.environmentalVolume.Location = new System.Drawing.Point(207, 193);
            this.environmentalVolume.MaxValue = 9F;
            this.environmentalVolume.MinValue = 0F;
            this.environmentalVolume.Name = "environmentalVolume";
            this.environmentalVolume.Renderer = null;
            this.environmentalVolume.ScaleColor = System.Drawing.Color.White;
            this.environmentalVolume.Size = new System.Drawing.Size(90, 82);
            this.environmentalVolume.StepValue = 0.1F;
            this.environmentalVolume.Style = LBSoft.IndustrialCtrls.Knobs.LBKnob.KnobStyle.Circular;
            this.environmentalVolume.TabIndex = 6;
            this.environmentalVolume.Value = 0F;
            this.environmentalVolume.KnobChangeValue += new LBSoft.IndustrialCtrls.Knobs.KnobChangeValue(this.environmentalVolume_KnobChangeValue);
            // 
            // musicVolume
            // 
            this.musicVolume.BackColor = System.Drawing.Color.Transparent;
            this.musicVolume.DrawRatio = 0.41F;
            this.musicVolume.IndicatorColor = System.Drawing.Color.White;
            this.musicVolume.IndicatorOffset = 10F;
            this.musicVolume.KnobCenter = ((System.Drawing.PointF)(resources.GetObject("musicVolume.KnobCenter")));
            this.musicVolume.KnobColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.musicVolume.KnobRect = ((System.Drawing.RectangleF)(resources.GetObject("musicVolume.KnobRect")));
            this.musicVolume.Location = new System.Drawing.Point(25, 193);
            this.musicVolume.MaxValue = 9F;
            this.musicVolume.MinValue = 0F;
            this.musicVolume.Name = "musicVolume";
            this.musicVolume.Renderer = null;
            this.musicVolume.ScaleColor = System.Drawing.Color.White;
            this.musicVolume.Size = new System.Drawing.Size(90, 82);
            this.musicVolume.StepValue = 0.1F;
            this.musicVolume.Style = LBSoft.IndustrialCtrls.Knobs.LBKnob.KnobStyle.Circular;
            this.musicVolume.TabIndex = 4;
            this.musicVolume.Value = 0F;
            this.musicVolume.KnobChangeValue += new LBSoft.IndustrialCtrls.Knobs.KnobChangeValue(this.musicVolume_KnobChangeValue);
            // 
            // sfxVolume
            // 
            this.sfxVolume.BackColor = System.Drawing.Color.Transparent;
            this.sfxVolume.DrawRatio = 0.41F;
            this.sfxVolume.IndicatorColor = System.Drawing.Color.White;
            this.sfxVolume.IndicatorOffset = 10F;
            this.sfxVolume.KnobCenter = ((System.Drawing.PointF)(resources.GetObject("sfxVolume.KnobCenter")));
            this.sfxVolume.KnobColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.sfxVolume.KnobRect = ((System.Drawing.RectangleF)(resources.GetObject("sfxVolume.KnobRect")));
            this.sfxVolume.Location = new System.Drawing.Point(211, 46);
            this.sfxVolume.MaxValue = 9F;
            this.sfxVolume.MinValue = 0F;
            this.sfxVolume.Name = "sfxVolume";
            this.sfxVolume.Renderer = null;
            this.sfxVolume.ScaleColor = System.Drawing.Color.White;
            this.sfxVolume.Size = new System.Drawing.Size(90, 82);
            this.sfxVolume.StepValue = 0.1F;
            this.sfxVolume.Style = LBSoft.IndustrialCtrls.Knobs.LBKnob.KnobStyle.Circular;
            this.sfxVolume.TabIndex = 2;
            this.sfxVolume.Value = 0F;
            this.sfxVolume.KnobChangeValue += new LBSoft.IndustrialCtrls.Knobs.KnobChangeValue(this.sfxVolume_KnobChangeValue);
            // 
            // overallVolume
            // 
            this.overallVolume.BackColor = System.Drawing.Color.Transparent;
            this.overallVolume.DrawRatio = 0.41F;
            this.overallVolume.IndicatorColor = System.Drawing.Color.White;
            this.overallVolume.IndicatorOffset = 10F;
            this.overallVolume.KnobCenter = ((System.Drawing.PointF)(resources.GetObject("overallVolume.KnobCenter")));
            this.overallVolume.KnobColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.overallVolume.KnobRect = ((System.Drawing.RectangleF)(resources.GetObject("overallVolume.KnobRect")));
            this.overallVolume.Location = new System.Drawing.Point(25, 46);
            this.overallVolume.MaxValue = 9F;
            this.overallVolume.MinValue = 0F;
            this.overallVolume.Name = "overallVolume";
            this.overallVolume.Renderer = null;
            this.overallVolume.ScaleColor = System.Drawing.Color.White;
            this.overallVolume.Size = new System.Drawing.Size(90, 82);
            this.overallVolume.StepValue = 0.1F;
            this.overallVolume.Style = LBSoft.IndustrialCtrls.Knobs.LBKnob.KnobStyle.Circular;
            this.overallVolume.TabIndex = 0;
            this.overallVolume.Value = 0F;
            this.overallVolume.KnobChangeValue += new LBSoft.IndustrialCtrls.Knobs.KnobChangeValue(this.overallVolume_KnobChangeValue);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.musicRepeat);
            this.groupBox1.Controls.Add(this.overallVolume);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.sfxVolume);
            this.groupBox1.Controls.Add(this.disableLobbyTheme);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.environmentalMute);
            this.groupBox1.Controls.Add(this.musicVolume);
            this.groupBox1.Controls.Add(this.musicMute);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.sfxMute);
            this.groupBox1.Controls.Add(this.environmentalVolume);
            this.groupBox1.Controls.Add(this.overallMute);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 381);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // RappelzVolumeGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(390, 407);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RappelzVolumeGUI";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RappelzVolumeGUI";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private LBSoft.IndustrialCtrls.Knobs.LBKnob overallVolume;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private LBSoft.IndustrialCtrls.Knobs.LBKnob sfxVolume;
        private System.Windows.Forms.Label label3;
        private LBSoft.IndustrialCtrls.Knobs.LBKnob musicVolume;
        private System.Windows.Forms.Label label4;
        private LBSoft.IndustrialCtrls.Knobs.LBKnob environmentalVolume;
        private System.Windows.Forms.CheckBox overallMute;
        private System.Windows.Forms.CheckBox sfxMute;
        private System.Windows.Forms.CheckBox musicMute;
        private System.Windows.Forms.CheckBox environmentalMute;
        private System.Windows.Forms.CheckBox disableLobbyTheme;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox musicRepeat;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}