using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Server.Functions;

namespace Server
{
    public partial class updatesManagerGUI : Form
    {
        public updatesManagerGUI()
        {
            InitializeComponent();
        }

        private async void updatesViewerGUI_Load(object sender, EventArgs e)
        {
            await Task.Run(() => 
            {
                updatesGrid.DataSource = IndexManager.Index;
                GUI.Instance.Invoke(new MethodInvoker(delegate 
                {
                    updatesGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    updatesGrid.Columns[0].ReadOnly = true;
                    updatesGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    updatesGrid.Columns[1].ReadOnly = true;
                    updatesGrid.Columns[2].Width = 50;
                    updatesGrid.Columns[3].Width = 50;
                }));
                
            });
        }

        private void updatesGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == updatesGrid.Columns[2].Index && e.RowIndex != -1 || e.ColumnIndex == updatesGrid.Columns[3].Index && e.RowIndex != -1) { updatesGrid.EndEdit(); }
        }

        private void updatesGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int saveType = 99;
            string fileName = updatesGrid.Rows[e.RowIndex].Cells[0].Value.ToString();
            bool state = (bool)updatesGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                     
            if (e.ColumnIndex == updatesGrid.Columns[2].Index && e.RowIndex != -1) // Legacy
            {
                OPT.AlterLegacyIndex(fileName, state);
                saveType = 0;
            }
            else if (e.ColumnIndex == updatesGrid.Columns[3].Index && e.RowIndex != -1) // Delete
            {
                OPT.AlterDeleteIndex(fileName, state);
                saveType = 1;
            }

            OPT.SaveIndex(saveType);
        }

        private void updatesGrid_MouseDown(object sender, MouseEventArgs e) { if (e.Button == MouseButtons.Right) { updatesContextMenu.Show(updatesGrid, e.Location); } }

        private void updatesContextAddBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofDlg = new OpenFileDialog() { Title = "Select File'(s) to add", Multiselect = true })
            {
                ofDlg.ShowDialog(this);

                bool filesChanged = ofDlg.FileNames.Length > 0;

                foreach (string filePath in ofDlg.FileNames)
                {
                    string fileName = Path.GetFileName(filePath);
                    string updatePath = string.Format(@"{0}\{1}", IndexManager.UpdatesDirectory, fileName);

                    if (File.Exists(updatePath))
                    {
                        FileInfo oldInfo = new FileInfo(updatePath);
                        FileInfo newInfo = new FileInfo(filePath);

                        string outputMsg = string.Format("An error importing {0} has occured.\n\nA file with the same name already exists in the updates directory!\n", fileName);
                        outputMsg += string.Format("\nExisting File:\n\t-Size: {0}\n\t-Date Modified: {1}", oldInfo.Length, oldInfo.LastWriteTimeUtc);
                        outputMsg += string.Format("\nNew File:\n\t-Size: {0}\n\t-Date Modified: {1}", newInfo.Length, newInfo.LastWriteTimeUtc);

                        if (MessageBox.Show(outputMsg, "Input Required", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) { File.Delete(updatePath); }
                    }

                    File.Move(fileName, updatePath);
                }

                if (filesChanged) { IndexManager.Build(true); }
            }
        }

        private void updatesContextRemoveBtn_Click(object sender, EventArgs e)
        {
            bool filesChanged = false;

            string fileName = updatesGrid.SelectedRows[0].Cells[0].Value.ToString();
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = string.Format(@"{0}\{1}", IndexManager.UpdatesDirectory, fileName);
                if (File.Exists(filePath)) { File.Delete(filePath); filesChanged = true; }
                else { MessageBox.Show("The file at path: {0} cannot be found, perhaps it has been deleted or renamed!", "File Exception", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else { MessageBox.Show("An error has occured fetching the file name of the selected update entry!", "Update Grid Exception", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            if (filesChanged) { IndexManager.Build(true); }
        }
    }
}
