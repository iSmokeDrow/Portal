namespace Server
{
    partial class updatesManagerGUI
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
            this.updatesGrid = new System.Windows.Forms.DataGridView();
            this.updatesContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.updatesContextAddBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.updatesContextRemoveBtn = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.updatesGrid)).BeginInit();
            this.updatesContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // updatesGrid
            // 
            this.updatesGrid.AllowUserToAddRows = false;
            this.updatesGrid.AllowUserToDeleteRows = false;
            this.updatesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.updatesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.updatesGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.updatesGrid.Location = new System.Drawing.Point(0, 0);
            this.updatesGrid.Name = "updatesGrid";
            this.updatesGrid.RowHeadersVisible = false;
            this.updatesGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.updatesGrid.Size = new System.Drawing.Size(580, 253);
            this.updatesGrid.TabIndex = 0;
            this.updatesGrid.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.updatesGrid_CellMouseUp);
            this.updatesGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.updatesGrid_CellValueChanged);
            this.updatesGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.updatesGrid_MouseDown);
            // 
            // updatesContextMenu
            // 
            this.updatesContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updatesContextAddBtn,
            this.updatesContextRemoveBtn});
            this.updatesContextMenu.Name = "updatesContextMenu";
            this.updatesContextMenu.Size = new System.Drawing.Size(108, 48);
            // 
            // updatesContextAddBtn
            // 
            this.updatesContextAddBtn.Name = "updatesContextAddBtn";
            this.updatesContextAddBtn.Size = new System.Drawing.Size(107, 22);
            this.updatesContextAddBtn.Text = "Add";
            this.updatesContextAddBtn.Click += new System.EventHandler(this.updatesContextAddBtn_Click);
            // 
            // updatesContextRemoveBtn
            // 
            this.updatesContextRemoveBtn.Name = "updatesContextRemoveBtn";
            this.updatesContextRemoveBtn.Size = new System.Drawing.Size(107, 22);
            this.updatesContextRemoveBtn.Text = "Delete";
            this.updatesContextRemoveBtn.Click += new System.EventHandler(this.updatesContextRemoveBtn_Click);
            // 
            // updatesManagerGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 253);
            this.Controls.Add(this.updatesGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "updatesManagerGUI";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Updates Manager";
            this.Load += new System.EventHandler(this.updatesViewerGUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.updatesGrid)).EndInit();
            this.updatesContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView updatesGrid;
        private System.Windows.Forms.ContextMenuStrip updatesContextMenu;
        private System.Windows.Forms.ToolStripMenuItem updatesContextAddBtn;
        private System.Windows.Forms.ToolStripMenuItem updatesContextRemoveBtn;
    }
}