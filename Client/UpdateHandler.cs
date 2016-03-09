using Client.Functions;
using Client.Network;
using Client.Structures;
using DataCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public class UpdateHandler
    {
        private string indexPath;
        private string resourceFolder;
        private string tempPath;

        private readonly Core core;

        private List<IndexEntry> index;

        public static readonly UpdateHandler Instance = new UpdateHandler();

        private readonly Properties.Settings settings;

        private GUI guiInstance = GUI.Instance;

        public bool NetworkError = false;

        public class UpdateIndex
        {
            public string FileName { get; set; }
            public string FileHash { get; set; }
            public bool IsLegacy { get; set; }
        }

        public List<UpdateIndex> FileList { get; set; }
        
        private int CurrentIndex { get; set; }

        public UpdateHandler()
        {
            core = new Core();
            core.TotalMaxDetermined += (o, x) => { guiInstance.Invoke(new MethodInvoker(delegate { guiInstance.totalProgress.Maximum = x.Maximum; })); };
            core.TotalProgressChanged += (o, x) => 
            {
                guiInstance.Invoke(new MethodInvoker(delegate
                {
                    guiInstance.totalProgress.Value = x.Value;
                    guiInstance.totalStatus.Text = x.Status;
                }));
            };
            core.TotalProgressReset += (o, x) => 
            {
                guiInstance.Invoke(new MethodInvoker(delegate
                {
                    guiInstance.totalProgress.Value = 0;
                    guiInstance.totalProgress.Maximum = 100;
                    guiInstance.totalStatus.ResetText();
                }));
            };
            core.CurrentMaxDetermined += (o, x) => { guiInstance.Invoke(new MethodInvoker(delegate { guiInstance.currentProgress.Maximum = x.Maximum; })); };
            core.CurrentProgressChanged += (o, x) => 
            {
                guiInstance.Invoke(new MethodInvoker(delegate
                {
                    guiInstance.currentProgress.Value = x.Value;
                    guiInstance.currentStatus.Text = x.Status;
                }));
            };
            core.CurrentProgressReset += (o, x) => 
            {
                guiInstance.Invoke(new MethodInvoker(delegate
                {
                    guiInstance.currentProgress.Value = 0;
                    guiInstance.currentProgress.Maximum = 100;
                    guiInstance.currentStatus.ResetText();
                }));
            };
            core.WarningOccured += (o, x) => { MessageBox.Show(x.Warning, "DataCore Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); };
            core.ErrorOccured += (o, x) => { MessageBox.Show(x.Error, "DataCore Exception", MessageBoxButtons.OK, MessageBoxIcon.Error); };
            settings = Properties.Settings.Default;
            indexPath = Path.Combine(settings.clientDirectory, "data.000");
            resourceFolder = string.Concat(settings.clientDirectory, @"/Resource/");
            tempPath = string.Concat(Directory.GetCurrentDirectory(), @"/tmp/");
            this.FileList = new List<UpdateIndex>();
        }

        public void Start()
        {
            index = core.Load(indexPath, false);
            // Start a connection to the server, if failed exit
            if (!ServerManager.Instance.Start(guiInstance.ip, guiInstance.port))
            {
                MessageBox.Show(ServerManager.Instance.ErrorMessage, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                NetworkError = true;
                return;
            }

            ServerPackets.Instance.RequestUpdateIndex();
        }

        public void OnUpdateIndexReceived(string fileName, string hash, bool isLegacy)
        {
            this.FileList.Add(new UpdateIndex() { FileName = fileName, FileHash = hash, IsLegacy = isLegacy });
        }

        public void OnUpdateIndexEnd()
        {
            this.CurrentIndex = 0;
            guiInstance.UpdateProgressMaximum(0, this.FileList.Count);
            CheckFiles();
        }

        private void CheckFiles()
        {
            for (; this.CurrentIndex < this.FileList.Count; ++this.CurrentIndex)
            {
                guiInstance.UpdateProgressValue(0, this.CurrentIndex);

                UpdateIndex file = this.FileList[this.CurrentIndex];
                bool download = false;

                guiInstance.UpdateStatus(1, string.Format("Checking file: {0}", file.FileName));

                if (file.IsLegacy)
                {
                    if (!File.Exists(resourceFolder + file.FileName) || (Hash.GetSHA512Hash(resourceFolder + file.FileName) != file.FileHash))
                    {
                        download = true;
                    }
                }
                else
                {
                    IndexEntry fileEntry = core.GetEntry(ref index, file.FileName);
                    if (fileEntry != null)
                    {
                        string fileHash = Hash.GetSHA512Hash(core.GetFileBytes(settings.clientDirectory, Path.GetExtension(core.DecodeName(fileEntry.Name)).Remove(0, 1), fileEntry.DataID, fileEntry.Offset, fileEntry.Length), fileEntry.Length);

                        if (file.FileHash != fileHash)
                        {
                            guiInstance.UpdateStatus(1, string.Format("File: {0} is out of date!", file.FileName));
                            download = true;
                        }
                    }
                }

                if (download) { DoUpdate(); break; }
            }

            if (this.CurrentIndex == this.FileList.Count) { GUI.OnUpdateComplete(); }
        }

        private void DoUpdate()
        {
            string name = this.FileList[this.CurrentIndex].FileName;
            int offset = 0;
            string partialHash = string.Empty;

            if (File.Exists(name))
            {
                byte[] currentData = File.ReadAllBytes(name);
                partialHash = Hash.GetSHA512Hash(currentData, currentData.Length);
                offset = currentData.Length;
            }

            guiInstance.UpdateStatus(1, string.Format("Downloading file: {0}", name));
            ServerPackets.Instance.RequestFile(name, offset, partialHash);
        }

        // TODO: Add timer to do timeout if file quits downloading
        public void OnFileDataReceived(int offset, bool isEOF, byte[] data)
        {
            string zipName = String.Concat(this.FileList[this.CurrentIndex].FileName, ".zip");
            string filePath = Path.Combine(tempPath, zipName);

            Directory.CreateDirectory("tmp");

            if (offset == 0 && File.Exists(filePath)) { File.Delete(filePath); }
            using (FileStream fs = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
            {
                fs.Seek(offset, SeekOrigin.Begin);
                fs.Write(data, 0, data.Length);
                fs.Dispose();
            }

            if (isEOF) { OnUpdateDownloadCompleted(); }
        }

        private void OnUpdateDownloadCompleted()
        {
            UpdateIndex file = this.FileList[this.CurrentIndex];

            guiInstance.UpdateStatus(1, string.Format("Applying update file: {0}", file.FileName));

            string zipPath = Path.Combine(tempPath, string.Concat(file.FileName, ".zip"));
            string resourcePath = Path.Combine(tempPath, file.FileName);

            // Unzip the file
            ZIP.Unpack(zipPath, tempPath);
            
            if (file.IsLegacy)
            {
                if (File.Exists(resourceFolder + file.FileName)) { File.Delete(resourceFolder + file.FileName); }
                File.Move(resourcePath, resourceFolder + file.FileName);
            }
            else
            {
                core.UpdateFileEntry(ref index, settings.clientDirectory, resourcePath, 0);
                core.Save(ref index, indexPath, false);
                File.Delete(resourcePath);
                File.Delete(zipPath);
            }

            guiInstance.UpdateStatus(1, string.Format("Verifying update: {0}", file.FileName));

            // Check that the file update actually took
            if (Hash.GetSHA512Hash(resourcePath) == file.FileHash) { CurrentIndex++; }

            guiInstance.UpdateStatus(1, "");
            
            CheckFiles();
        }
    }
}
