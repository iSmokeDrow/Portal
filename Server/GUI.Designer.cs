namespace Server
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
            this.output = new System.Windows.Forms.TabPage();
            this.OutputText = new System.Windows.Forms.RichTextBox();
            this.statistics = new System.Windows.Forms.TabPage();
            this.logins = new System.Windows.Forms.GroupBox();
            this.bannedCount = new System.Windows.Forms.Label();
            this.rejectedCount = new System.Windows.Forms.Label();
            this.authenticatedCount = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.bannedCountLbl = new System.Windows.Forms.Label();
            this.wrongPassCountLbl = new System.Windows.Forms.Label();
            this.authenticatedCountLbl = new System.Windows.Forms.Label();
            this.updates = new System.Windows.Forms.GroupBox();
            this.DeleteCount = new System.Windows.Forms.Label();
            this.ResourceCount = new System.Windows.Forms.Label();
            this.DataCount = new System.Windows.Forms.Label();
            this.updatesViewBtn = new System.Windows.Forms.Button();
            this.deleteUpdateLbl = new System.Windows.Forms.Label();
            this.resourceUpdateLbl = new System.Windows.Forms.Label();
            this.dataUpdateLbl = new System.Windows.Forms.Label();
            this.connections = new System.Windows.Forms.GroupBox();
            this.DisconnectCount = new System.Windows.Forms.Label();
            this.UpdatersCount = new System.Windows.Forms.Label();
            this.LaunchersCount = new System.Windows.Forms.Label();
            this.disconnectCountLbl = new System.Windows.Forms.Label();
            this.viewConBtn = new System.Windows.Forms.Button();
            this.updaterCountLbl = new System.Windows.Forms.Label();
            this.launcherCountLbl = new System.Windows.Forms.Label();
            this.netGrpBox = new System.Windows.Forms.GroupBox();
            this.Maintenance = new System.Windows.Forms.Label();
            this.Uptime = new System.Windows.Forms.Label();
            this.Port = new System.Windows.Forms.Label();
            this.IP = new System.Windows.Forms.Label();
            this.maintLbl = new System.Windows.Forms.Label();
            this.uptimeLbl = new System.Windows.Forms.Label();
            this.portLbl = new System.Windows.Forms.Label();
            this.ipLbl = new System.Windows.Forms.Label();
            this.tabs = new System.Windows.Forms.TabControl();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.updatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatesView = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updatesClearTmp = new System.Windows.Forms.ToolStripMenuItem();
            this.networkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listenerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.networkListenerMaintenance = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.characterNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.updateWriteTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setWTBtn = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.output.SuspendLayout();
            this.statistics.SuspendLayout();
            this.logins.SuspendLayout();
            this.updates.SuspendLayout();
            this.connections.SuspendLayout();
            this.netGrpBox.SuspendLayout();
            this.tabs.SuspendLayout();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // output
            // 
            this.output.Controls.Add(this.OutputText);
            this.output.Location = new System.Drawing.Point(4, 22);
            this.output.Name = "output";
            this.output.Padding = new System.Windows.Forms.Padding(3);
            this.output.Size = new System.Drawing.Size(395, 296);
            this.output.TabIndex = 2;
            this.output.Text = "Output";
            this.output.UseVisualStyleBackColor = true;
            // 
            // OutputText
            // 
            this.OutputText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputText.Location = new System.Drawing.Point(3, 3);
            this.OutputText.Name = "OutputText";
            this.OutputText.ReadOnly = true;
            this.OutputText.Size = new System.Drawing.Size(389, 290);
            this.OutputText.TabIndex = 0;
            this.OutputText.Text = "";
            // 
            // statistics
            // 
            this.statistics.Controls.Add(this.logins);
            this.statistics.Controls.Add(this.updates);
            this.statistics.Controls.Add(this.connections);
            this.statistics.Controls.Add(this.netGrpBox);
            this.statistics.Location = new System.Drawing.Point(4, 22);
            this.statistics.Name = "statistics";
            this.statistics.Padding = new System.Windows.Forms.Padding(3);
            this.statistics.Size = new System.Drawing.Size(395, 296);
            this.statistics.TabIndex = 0;
            this.statistics.Text = "Statistics";
            this.statistics.UseVisualStyleBackColor = true;
            // 
            // logins
            // 
            this.logins.Controls.Add(this.bannedCount);
            this.logins.Controls.Add(this.rejectedCount);
            this.logins.Controls.Add(this.authenticatedCount);
            this.logins.Controls.Add(this.button2);
            this.logins.Controls.Add(this.bannedCountLbl);
            this.logins.Controls.Add(this.wrongPassCountLbl);
            this.logins.Controls.Add(this.authenticatedCountLbl);
            this.logins.Location = new System.Drawing.Point(201, 149);
            this.logins.Name = "logins";
            this.logins.Size = new System.Drawing.Size(189, 137);
            this.logins.TabIndex = 3;
            this.logins.TabStop = false;
            this.logins.Text = "Logins";
            // 
            // bannedCount
            // 
            this.bannedCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.bannedCount.Location = new System.Drawing.Point(83, 65);
            this.bannedCount.Name = "bannedCount";
            this.bannedCount.Size = new System.Drawing.Size(100, 20);
            this.bannedCount.TabIndex = 21;
            this.bannedCount.Text = "0";
            this.bannedCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rejectedCount
            // 
            this.rejectedCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.rejectedCount.Location = new System.Drawing.Point(83, 39);
            this.rejectedCount.Name = "rejectedCount";
            this.rejectedCount.Size = new System.Drawing.Size(100, 20);
            this.rejectedCount.TabIndex = 20;
            this.rejectedCount.Text = "0";
            this.rejectedCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // authenticatedCount
            // 
            this.authenticatedCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.authenticatedCount.Location = new System.Drawing.Point(83, 16);
            this.authenticatedCount.Name = "authenticatedCount";
            this.authenticatedCount.Size = new System.Drawing.Size(100, 20);
            this.authenticatedCount.TabIndex = 19;
            this.authenticatedCount.Text = "0";
            this.authenticatedCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(108, 99);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "View All";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // bannedCountLbl
            // 
            this.bannedCountLbl.AutoSize = true;
            this.bannedCountLbl.Location = new System.Drawing.Point(6, 72);
            this.bannedCountLbl.Name = "bannedCountLbl";
            this.bannedCountLbl.Size = new System.Drawing.Size(44, 13);
            this.bannedCountLbl.TabIndex = 2;
            this.bannedCountLbl.Text = "Banned";
            // 
            // wrongPassCountLbl
            // 
            this.wrongPassCountLbl.AutoSize = true;
            this.wrongPassCountLbl.Location = new System.Drawing.Point(6, 46);
            this.wrongPassCountLbl.Name = "wrongPassCountLbl";
            this.wrongPassCountLbl.Size = new System.Drawing.Size(53, 13);
            this.wrongPassCountLbl.TabIndex = 1;
            this.wrongPassCountLbl.Text = "Rejected:";
            // 
            // authenticatedCountLbl
            // 
            this.authenticatedCountLbl.AutoSize = true;
            this.authenticatedCountLbl.Location = new System.Drawing.Point(6, 20);
            this.authenticatedCountLbl.Name = "authenticatedCountLbl";
            this.authenticatedCountLbl.Size = new System.Drawing.Size(76, 13);
            this.authenticatedCountLbl.TabIndex = 0;
            this.authenticatedCountLbl.Text = "Authenticated:";
            // 
            // updates
            // 
            this.updates.Controls.Add(this.setWTBtn);
            this.updates.Controls.Add(this.DeleteCount);
            this.updates.Controls.Add(this.ResourceCount);
            this.updates.Controls.Add(this.DataCount);
            this.updates.Controls.Add(this.updatesViewBtn);
            this.updates.Controls.Add(this.deleteUpdateLbl);
            this.updates.Controls.Add(this.resourceUpdateLbl);
            this.updates.Controls.Add(this.dataUpdateLbl);
            this.updates.Location = new System.Drawing.Point(6, 149);
            this.updates.Name = "updates";
            this.updates.Size = new System.Drawing.Size(189, 137);
            this.updates.TabIndex = 2;
            this.updates.TabStop = false;
            this.updates.Text = "Updates";
            // 
            // DeleteCount
            // 
            this.DeleteCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DeleteCount.Location = new System.Drawing.Point(83, 69);
            this.DeleteCount.Name = "DeleteCount";
            this.DeleteCount.Size = new System.Drawing.Size(100, 20);
            this.DeleteCount.TabIndex = 15;
            this.DeleteCount.Text = "0";
            this.DeleteCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ResourceCount
            // 
            this.ResourceCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ResourceCount.Location = new System.Drawing.Point(83, 43);
            this.ResourceCount.Name = "ResourceCount";
            this.ResourceCount.Size = new System.Drawing.Size(100, 20);
            this.ResourceCount.TabIndex = 14;
            this.ResourceCount.Text = "0";
            this.ResourceCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DataCount
            // 
            this.DataCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DataCount.Location = new System.Drawing.Point(83, 16);
            this.DataCount.Name = "DataCount";
            this.DataCount.Size = new System.Drawing.Size(100, 20);
            this.DataCount.TabIndex = 13;
            this.DataCount.Text = "0";
            this.DataCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // updatesViewBtn
            // 
            this.updatesViewBtn.Enabled = false;
            this.updatesViewBtn.Location = new System.Drawing.Point(108, 99);
            this.updatesViewBtn.Name = "updatesViewBtn";
            this.updatesViewBtn.Size = new System.Drawing.Size(75, 23);
            this.updatesViewBtn.TabIndex = 7;
            this.updatesViewBtn.Text = "View All";
            this.updatesViewBtn.UseVisualStyleBackColor = true;
            this.updatesViewBtn.Click += new System.EventHandler(this.updatesView_Click);
            // 
            // deleteUpdateLbl
            // 
            this.deleteUpdateLbl.AutoSize = true;
            this.deleteUpdateLbl.Location = new System.Drawing.Point(6, 72);
            this.deleteUpdateLbl.Name = "deleteUpdateLbl";
            this.deleteUpdateLbl.Size = new System.Drawing.Size(41, 13);
            this.deleteUpdateLbl.TabIndex = 2;
            this.deleteUpdateLbl.Text = "Delete:";
            // 
            // resourceUpdateLbl
            // 
            this.resourceUpdateLbl.AutoSize = true;
            this.resourceUpdateLbl.Location = new System.Drawing.Point(6, 46);
            this.resourceUpdateLbl.Name = "resourceUpdateLbl";
            this.resourceUpdateLbl.Size = new System.Drawing.Size(56, 13);
            this.resourceUpdateLbl.TabIndex = 1;
            this.resourceUpdateLbl.Text = "Resource:";
            // 
            // dataUpdateLbl
            // 
            this.dataUpdateLbl.AutoSize = true;
            this.dataUpdateLbl.Location = new System.Drawing.Point(6, 20);
            this.dataUpdateLbl.Name = "dataUpdateLbl";
            this.dataUpdateLbl.Size = new System.Drawing.Size(33, 13);
            this.dataUpdateLbl.TabIndex = 0;
            this.dataUpdateLbl.Text = "Data:";
            // 
            // connections
            // 
            this.connections.Controls.Add(this.DisconnectCount);
            this.connections.Controls.Add(this.UpdatersCount);
            this.connections.Controls.Add(this.LaunchersCount);
            this.connections.Controls.Add(this.disconnectCountLbl);
            this.connections.Controls.Add(this.viewConBtn);
            this.connections.Controls.Add(this.updaterCountLbl);
            this.connections.Controls.Add(this.launcherCountLbl);
            this.connections.Location = new System.Drawing.Point(201, 6);
            this.connections.Name = "connections";
            this.connections.Size = new System.Drawing.Size(189, 137);
            this.connections.TabIndex = 1;
            this.connections.TabStop = false;
            this.connections.Text = "Connections";
            // 
            // DisconnectCount
            // 
            this.DisconnectCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DisconnectCount.Location = new System.Drawing.Point(83, 65);
            this.DisconnectCount.Name = "DisconnectCount";
            this.DisconnectCount.Size = new System.Drawing.Size(100, 20);
            this.DisconnectCount.TabIndex = 18;
            this.DisconnectCount.Text = "0";
            this.DisconnectCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UpdatersCount
            // 
            this.UpdatersCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.UpdatersCount.Location = new System.Drawing.Point(83, 39);
            this.UpdatersCount.Name = "UpdatersCount";
            this.UpdatersCount.Size = new System.Drawing.Size(100, 20);
            this.UpdatersCount.TabIndex = 17;
            this.UpdatersCount.Text = "0";
            this.UpdatersCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LaunchersCount
            // 
            this.LaunchersCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LaunchersCount.Location = new System.Drawing.Point(83, 13);
            this.LaunchersCount.Name = "LaunchersCount";
            this.LaunchersCount.Size = new System.Drawing.Size(100, 20);
            this.LaunchersCount.TabIndex = 16;
            this.LaunchersCount.Text = "0";
            this.LaunchersCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // disconnectCountLbl
            // 
            this.disconnectCountLbl.AutoSize = true;
            this.disconnectCountLbl.Location = new System.Drawing.Point(6, 72);
            this.disconnectCountLbl.Name = "disconnectCountLbl";
            this.disconnectCountLbl.Size = new System.Drawing.Size(69, 13);
            this.disconnectCountLbl.TabIndex = 5;
            this.disconnectCountLbl.Text = "Disconnects:";
            // 
            // viewConBtn
            // 
            this.viewConBtn.Enabled = false;
            this.viewConBtn.Location = new System.Drawing.Point(108, 99);
            this.viewConBtn.Name = "viewConBtn";
            this.viewConBtn.Size = new System.Drawing.Size(75, 23);
            this.viewConBtn.TabIndex = 2;
            this.viewConBtn.Text = "View All";
            this.viewConBtn.UseVisualStyleBackColor = true;
            // 
            // updaterCountLbl
            // 
            this.updaterCountLbl.AutoSize = true;
            this.updaterCountLbl.Location = new System.Drawing.Point(6, 46);
            this.updaterCountLbl.Name = "updaterCountLbl";
            this.updaterCountLbl.Size = new System.Drawing.Size(53, 13);
            this.updaterCountLbl.TabIndex = 1;
            this.updaterCountLbl.Text = "Updaters:";
            // 
            // launcherCountLbl
            // 
            this.launcherCountLbl.AutoSize = true;
            this.launcherCountLbl.Location = new System.Drawing.Point(6, 20);
            this.launcherCountLbl.Name = "launcherCountLbl";
            this.launcherCountLbl.Size = new System.Drawing.Size(60, 13);
            this.launcherCountLbl.TabIndex = 0;
            this.launcherCountLbl.Text = "Launchers:";
            // 
            // netGrpBox
            // 
            this.netGrpBox.Controls.Add(this.Maintenance);
            this.netGrpBox.Controls.Add(this.Uptime);
            this.netGrpBox.Controls.Add(this.Port);
            this.netGrpBox.Controls.Add(this.IP);
            this.netGrpBox.Controls.Add(this.maintLbl);
            this.netGrpBox.Controls.Add(this.uptimeLbl);
            this.netGrpBox.Controls.Add(this.portLbl);
            this.netGrpBox.Controls.Add(this.ipLbl);
            this.netGrpBox.Location = new System.Drawing.Point(6, 6);
            this.netGrpBox.Name = "netGrpBox";
            this.netGrpBox.Size = new System.Drawing.Size(189, 137);
            this.netGrpBox.TabIndex = 0;
            this.netGrpBox.TabStop = false;
            this.netGrpBox.Text = "Network";
            // 
            // Maintenance
            // 
            this.Maintenance.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Maintenance.Location = new System.Drawing.Point(83, 95);
            this.Maintenance.Name = "Maintenance";
            this.Maintenance.Size = new System.Drawing.Size(100, 20);
            this.Maintenance.TabIndex = 12;
            this.Maintenance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Uptime
            // 
            this.Uptime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Uptime.Location = new System.Drawing.Point(83, 68);
            this.Uptime.Name = "Uptime";
            this.Uptime.Size = new System.Drawing.Size(100, 20);
            this.Uptime.TabIndex = 11;
            this.Uptime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Port
            // 
            this.Port.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Port.Location = new System.Drawing.Point(83, 42);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(100, 20);
            this.Port.TabIndex = 10;
            this.Port.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IP
            // 
            this.IP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.IP.Location = new System.Drawing.Point(83, 17);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(100, 20);
            this.IP.TabIndex = 9;
            this.IP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // maintLbl
            // 
            this.maintLbl.AutoSize = true;
            this.maintLbl.Location = new System.Drawing.Point(6, 99);
            this.maintLbl.Name = "maintLbl";
            this.maintLbl.Size = new System.Drawing.Size(72, 13);
            this.maintLbl.TabIndex = 7;
            this.maintLbl.Text = "Maintenance:";
            // 
            // uptimeLbl
            // 
            this.uptimeLbl.AutoSize = true;
            this.uptimeLbl.Location = new System.Drawing.Point(6, 72);
            this.uptimeLbl.Name = "uptimeLbl";
            this.uptimeLbl.Size = new System.Drawing.Size(50, 13);
            this.uptimeLbl.TabIndex = 5;
            this.uptimeLbl.Text = "Up-Time:";
            // 
            // portLbl
            // 
            this.portLbl.AutoSize = true;
            this.portLbl.Location = new System.Drawing.Point(6, 46);
            this.portLbl.Name = "portLbl";
            this.portLbl.Size = new System.Drawing.Size(29, 13);
            this.portLbl.TabIndex = 2;
            this.portLbl.Text = "Port:";
            // 
            // ipLbl
            // 
            this.ipLbl.AutoSize = true;
            this.ipLbl.Location = new System.Drawing.Point(6, 20);
            this.ipLbl.Name = "ipLbl";
            this.ipLbl.Size = new System.Drawing.Size(20, 13);
            this.ipLbl.TabIndex = 0;
            this.ipLbl.Text = "IP:";
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.statistics);
            this.tabs.Controls.Add(this.output);
            this.tabs.Location = new System.Drawing.Point(10, 27);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(403, 322);
            this.tabs.TabIndex = 0;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updatesToolStripMenuItem,
            this.networkToolStripMenuItem,
            this.usersToolStripMenuItem,
            this.menuSettings});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(422, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menuStrip1";
            // 
            // updatesToolStripMenuItem
            // 
            this.updatesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updatesView,
            this.reloadToolStripMenuItem,
            this.updatesClearTmp,
            this.updateWriteTimeToolStripMenuItem});
            this.updatesToolStripMenuItem.Name = "updatesToolStripMenuItem";
            this.updatesToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.updatesToolStripMenuItem.Text = "Updates";
            // 
            // updatesView
            // 
            this.updatesView.Enabled = false;
            this.updatesView.Name = "updatesView";
            this.updatesView.Size = new System.Drawing.Size(152, 22);
            this.updatesView.Text = "View";
            this.updatesView.Click += new System.EventHandler(this.updatesView_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.deletesToolStripMenuItem});
            this.reloadToolStripMenuItem.Enabled = false;
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.reloadToolStripMenuItem.Text = "Reload";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.allToolStripMenuItem.Text = "All";
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.dataToolStripMenuItem.Text = "Updates";
            // 
            // deletesToolStripMenuItem
            // 
            this.deletesToolStripMenuItem.Name = "deletesToolStripMenuItem";
            this.deletesToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.deletesToolStripMenuItem.Text = "Deletes";
            // 
            // updatesClearTmp
            // 
            this.updatesClearTmp.Name = "updatesClearTmp";
            this.updatesClearTmp.Size = new System.Drawing.Size(152, 22);
            this.updatesClearTmp.Text = "Clear Tmp";
            this.updatesClearTmp.Click += new System.EventHandler(this.updatesClearTmp_Click);
            // 
            // networkToolStripMenuItem
            // 
            this.networkToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listenerToolStripMenuItem});
            this.networkToolStripMenuItem.Name = "networkToolStripMenuItem";
            this.networkToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.networkToolStripMenuItem.Text = "Network";
            // 
            // listenerToolStripMenuItem
            // 
            this.listenerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.networkListenerMaintenance});
            this.listenerToolStripMenuItem.Name = "listenerToolStripMenuItem";
            this.listenerToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.listenerToolStripMenuItem.Text = "Listener";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Enabled = false;
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.startToolStripMenuItem.Text = "Start";
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            // 
            // networkListenerMaintenance
            // 
            this.networkListenerMaintenance.Name = "networkListenerMaintenance";
            this.networkListenerMaintenance.Size = new System.Drawing.Size(143, 22);
            this.networkListenerMaintenance.Text = "Maintenance";
            this.networkListenerMaintenance.Click += new System.EventHandler(this.networkListenerMaintenance_Click);
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem});
            this.usersToolStripMenuItem.Enabled = false;
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.usersToolStripMenuItem.Text = "Users";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.byNameToolStripMenuItem,
            this.byIPToolStripMenuItem});
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.searchToolStripMenuItem.Text = "Search";
            // 
            // byNameToolStripMenuItem
            // 
            this.byNameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginNameToolStripMenuItem,
            this.characterNameToolStripMenuItem});
            this.byNameToolStripMenuItem.Name = "byNameToolStripMenuItem";
            this.byNameToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.byNameToolStripMenuItem.Text = "By Name";
            // 
            // loginNameToolStripMenuItem
            // 
            this.loginNameToolStripMenuItem.Name = "loginNameToolStripMenuItem";
            this.loginNameToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.loginNameToolStripMenuItem.Text = "Login Name";
            // 
            // characterNameToolStripMenuItem
            // 
            this.characterNameToolStripMenuItem.Name = "characterNameToolStripMenuItem";
            this.characterNameToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.characterNameToolStripMenuItem.Text = "Character Name";
            // 
            // byIPToolStripMenuItem
            // 
            this.byIPToolStripMenuItem.Name = "byIPToolStripMenuItem";
            this.byIPToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.byIPToolStripMenuItem.Text = "By IP";
            // 
            // menuSettings
            // 
            this.menuSettings.Name = "menuSettings";
            this.menuSettings.Size = new System.Drawing.Size(61, 20);
            this.menuSettings.Text = "Settings";
            this.menuSettings.Click += new System.EventHandler(this.settingsMenu_Click);
            // 
            // updateWriteTimeToolStripMenuItem
            // 
            this.updateWriteTimeToolStripMenuItem.Name = "updateWriteTimeToolStripMenuItem";
            this.updateWriteTimeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.updateWriteTimeToolStripMenuItem.Text = "Set Write Time";
            this.updateWriteTimeToolStripMenuItem.ToolTipText = "Sets the Updates folder last write time to the current time";
            this.updateWriteTimeToolStripMenuItem.Click += new System.EventHandler(this.setWTBtn_Click);
            // 
            // setWTBtn
            // 
            this.setWTBtn.Location = new System.Drawing.Point(9, 99);
            this.setWTBtn.Name = "setWTBtn";
            this.setWTBtn.Size = new System.Drawing.Size(93, 23);
            this.setWTBtn.TabIndex = 16;
            this.setWTBtn.Text = "Set Write Time";
            this.setWTBtn.UseVisualStyleBackColor = true;
            this.setWTBtn.Click += new System.EventHandler(this.setWTBtn_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 352);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.menu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GUI";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Portal Server";
            this.Shown += new System.EventHandler(this.GUI_Shown);
            this.output.ResumeLayout(false);
            this.statistics.ResumeLayout(false);
            this.logins.ResumeLayout(false);
            this.logins.PerformLayout();
            this.updates.ResumeLayout(false);
            this.updates.PerformLayout();
            this.connections.ResumeLayout(false);
            this.connections.PerformLayout();
            this.netGrpBox.ResumeLayout(false);
            this.netGrpBox.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage output;
        private System.Windows.Forms.TabPage statistics;
        private System.Windows.Forms.GroupBox logins;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label bannedCountLbl;
        private System.Windows.Forms.Label wrongPassCountLbl;
        private System.Windows.Forms.Label authenticatedCountLbl;
        private System.Windows.Forms.GroupBox updates;
        private System.Windows.Forms.Label deleteUpdateLbl;
        private System.Windows.Forms.Label resourceUpdateLbl;
        private System.Windows.Forms.Label dataUpdateLbl;
        private System.Windows.Forms.GroupBox connections;
        private System.Windows.Forms.Label disconnectCountLbl;
        private System.Windows.Forms.Button viewConBtn;
        private System.Windows.Forms.Label updaterCountLbl;
        private System.Windows.Forms.Label launcherCountLbl;
        private System.Windows.Forms.GroupBox netGrpBox;
        private System.Windows.Forms.Label portLbl;
        private System.Windows.Forms.Label ipLbl;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem updatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem networkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuSettings;
        private System.Windows.Forms.ToolStripMenuItem listenerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem networkListenerMaintenance;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem characterNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byIPToolStripMenuItem;
        private System.Windows.Forms.Label uptimeLbl;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletesToolStripMenuItem;
        public System.Windows.Forms.RichTextBox OutputText;
        private System.Windows.Forms.Label maintLbl;
        public System.Windows.Forms.Label Uptime;
        public System.Windows.Forms.Label Port;
        public System.Windows.Forms.Label IP;
        private System.Windows.Forms.ToolStripMenuItem updatesClearTmp;
        public System.Windows.Forms.Label DataCount;
        public System.Windows.Forms.Label DeleteCount;
        public System.Windows.Forms.Label ResourceCount;
        public System.Windows.Forms.Label DisconnectCount;
        public System.Windows.Forms.Label UpdatersCount;
        public System.Windows.Forms.Label LaunchersCount;
        public System.Windows.Forms.Label Maintenance;
        internal System.Windows.Forms.Label authenticatedCount;
        internal System.Windows.Forms.Label rejectedCount;
        internal System.Windows.Forms.Label bannedCount;
        internal System.Windows.Forms.Button updatesViewBtn;
        internal System.Windows.Forms.ToolStripMenuItem updatesView;
        internal System.Windows.Forms.Button setWTBtn;
        private System.Windows.Forms.ToolStripMenuItem updateWriteTimeToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
    }
}