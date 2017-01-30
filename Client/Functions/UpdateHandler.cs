using Client.Network;
using System.Diagnostics;
using DataCore;
using DataCore.Structures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Threading.Tasks;
using Client.Structures;

namespace Client.Functions
{
    public class UpdateHandler
    {
        private string indexPath;
        private string resourceFolder;
        private string disabledFolder;
        private string tmpDirectory;

        public readonly Core Core;

        private List<DataCore.Structures.IndexEntry> index;

        internal OPT settings = OPT.Instance;
        protected static UpdateHandler instance;
        public static UpdateHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UpdateHandler();
                }

                return instance;
            }
        }
        private GUI guiInstance = GUI.Instance;

        protected int receiveType = 0;

        private string GetFileExtension(string fileName) { return (Core.IsEncoded(fileName)) ? Path.GetExtension(Core.DecodeName(fileName)).Remove(0, 1).ToLower() : Path.GetExtension(fileName).Remove(0, 1).ToLower(); }

        public List<Structures.IndexEntry> FileList { get; set; }
        
        private int currentIndex { get; set; }

        internal bool isLegacy = false;

        internal Drive gDrive;

        public UpdateHandler()
        {
            Core = new Core();
            Core.TotalMaxDetermined += (o, x) => { guiInstance.Invoke(new MethodInvoker(delegate { guiInstance.totalProgress.Maximum = x.Maximum; })); };
            Core.TotalProgressChanged += (o, x) =>
            {
                guiInstance.Invoke(new MethodInvoker(delegate
                {
                    guiInstance.totalProgress.Value = x.Value;
                    guiInstance.totalStatus.Text = x.Status;
                }));
            };
            Core.TotalProgressReset += (o, x) =>
            {
                guiInstance.Invoke(new MethodInvoker(delegate
                {
                    guiInstance.totalProgress.Value = 0;
                    guiInstance.totalProgress.Maximum = 100;
                    guiInstance.totalStatus.ResetText();
                }));
            };
            Core.CurrentMaxDetermined += (o, x) => { guiInstance.Invoke(new MethodInvoker(delegate { guiInstance.currentProgress.Maximum = Convert.ToInt32(x.Maximum); })); };
            Core.CurrentProgressChanged += (o, x) =>
            {
                guiInstance.Invoke(new MethodInvoker(delegate
                {
                    guiInstance.currentProgress.Value = Convert.ToInt32(x.Value);
                    guiInstance.currentStatus.Text = x.Status;
                }));
            };
            Core.CurrentProgressReset += (o, x) =>
            {
                guiInstance.Invoke(new MethodInvoker(delegate
                {
                    guiInstance.currentProgress.Value = 0;
                    guiInstance.currentProgress.Maximum = 100;
                    guiInstance.currentStatus.ResetText();
                }));
            };
            Core.WarningOccured += (o, x) => { MessageBox.Show(x.Warning, "DataCore Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); };
            Core.ErrorOccured += (o, x) => { MessageBox.Show(x.Error, "DataCore Exception", MessageBoxButtons.OK, MessageBoxIcon.Error); };
            resourceFolder = string.Concat(OPT.Instance.GetString("clientdirectory"), @"/Resource/");
            disabledFolder = string.Concat(OPT.Instance.GetString("clientdirectory"), @"/Disabled/");
            tmpDirectory = string.Concat(Directory.GetCurrentDirectory(), @"/tmp/");
            FileList = new List<Structures.IndexEntry>();
            gDrive = new Drive();
        }

        protected bool downloading = false;

        public void Start()
        {
            indexPath = Path.Combine(OPT.Instance.GetString("clientdirectory"), "data.000");
            index = Core.Load(indexPath, false, 64000);

            if (!Directory.Exists(disabledFolder)) { Directory.CreateDirectory(disabledFolder); }
            if (!Directory.Exists(tmpDirectory)) { Directory.CreateDirectory(tmpDirectory); }

            ServerPackets.Instance.CS_RequestTransferType();
            ServerPackets.Instance.CS_GetUpdateDateTime();
        }

        public void OnUpdateDateTimeReceived(DateTime dateTime)
        {
            guiInstance.UpdateStatus(0, "Checking times...");

            DateTime indexDateTime = File.GetLastWriteTimeUtc(indexPath);
            DateTime resourceDateTime = Directory.GetLastWriteTime(resourceFolder);
            bool updateRequired = indexDateTime < dateTime || resourceDateTime < dateTime;

            if (updateRequired) { ServerPackets.Instance.CS_RequestUpdateIndex(); }
            else { guiInstance.OnUpdateComplete(); }
        }

        // TODO: Update me to use TCP
        public void ExecuteSelfUpdate(string fileName) { DownloadSelfUpdate(fileName); }

        // TODO: Upgrade to use TCP
        // Review me!
        internal void ExecuteUpdaterUpdate(string fileName)
        {
            //guiInstance.UpdateStatus(0, "Downloading Updater...");

            //string zipPath = Path.Combine(tmpDirectory, string.Concat(fileName, ".zip"));
            //WebClient client = new WebClient();
            //client.DownloadFileCompleted += (o, x) => 
            //{
            //    if (File.Exists(zipPath))
            //    {
            //        File.Delete("Updater.exe");
            //        ZIP.Unpack(zipPath, Directory.GetCurrentDirectory());
            //        File.Delete(zipPath);
            //    }
            //};
            //string url = String.Concat(patcherUrl, fileName, ".zip");
            //client.DownloadFileAsync(new Uri(url), Path.Combine(tmpDirectory, string.Concat(fileName, ".zip")), client);
        }

        public void OnUpdateIndexReceived(string fileName, string hash, bool isLegacy)
        {
            this.FileList.Add(new Structures.IndexEntry() { FileName = fileName, FileHash = hash, IsLegacy = isLegacy });
        }

        public void OnUpdateIndexEnd()
        {
            this.currentIndex = 0;
            guiInstance.UpdateProgressMaximum(0, this.FileList.Count);

            compareFiles();
        }

        protected void compareFiles()
        {
            guiInstance.UpdateStatus(0, "Checking client files...");
            GUI.Instance.UpdateProgressMaximum(0, FileList.Count);
            guiInstance.UpdateProgressValue(0, currentIndex);

            Structures.IndexEntry file = FileList[currentIndex];
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
                DataCore.Structures.IndexEntry fileEntry = Core.GetEntry(ref index, file.FileName);

                if (fileEntry != null)
                {
                    string fileHash = Core.GetFileSHA512(settings.GetString("clientdirectory"), Core.GetID(fileEntry.Name), fileEntry.Offset, fileEntry.Length, GetFileExtension(fileEntry.Name));

                    if (file.FileHash != fileHash)
                    {
                        guiInstance.UpdateStatus(1, string.Format("File: {0} is depreciated!", file.FileName));
                        download = true;
                    }
                }
                else { /* TODO: Throw exception about file not found in data.000 */ }
            }

            if (download)
            {
                GUI.Instance.UpdateStatus(1, string.Format("Downloading {0}...", FileList[currentIndex].FileName));
                doUpdate();
            }
            else { iterateCurrentIndex(); }
        }

        private void DownloadSelfUpdate(string fileName)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"Updater.exe";
            //startInfo.Arguments = String.Concat(patcherUrl, fileName, ".zip") + " " + fileName;
            Process.Start(startInfo);
        }

        internal void OnSendTypeReceived(int transferType) { receiveType = transferType;  }

        protected void doUpdate()
        {
            switch (receiveType)
            {
                case 0: // Google Drive
                    // TODO: Adjust me for new downloading system
                    //gDrive.Start();
                    //string filePath = gDrive.GetFile(name);
                    break;

                case 1: // HTTP
                    break;

                case 2: // FTP
                    break;

                case 3: // TCP
                    ServerPackets.Instance.CS_RequestFileSize(FileList[currentIndex].FileName);
                    break;

                case 99:
                    MessageBox.Show("Invalid receiveType!", "UpdateHandler Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        internal void OnFileInfoReceived(string archiveName) { ServerPackets.Instance.CS_RequestFileTransfer(archiveName); }

        internal void OnFileTransfered(string zipName)
        {
            GUI.Instance.ResetProgressStatus(1);

            GUI.Instance.UpdateStatus(0, "Unpacking update...");

            bool isLegacy = this.FileList[this.currentIndex].IsLegacy;

            string zipPath = string.Format(@"{0}\Downloads\{1}", Directory.GetCurrentDirectory(), zipName);

            string fileName = FileList[currentIndex].FileName;

            if (isLegacy)
            {
                GUI.Instance.UpdateStatus(1, string.Format("Moving {0} to /Resource/...", fileName));

                // Extract the zip to the /resource/ folder of client
                ZIP.Unpack(zipPath, resourceFolder);
            }
            else
            {
                string filePath = string.Format(@"{0}\{1}", tmpDirectory, fileName);

                // Extract the zip to the /tmp/ folder for processing
                ZIP.Unpack(zipPath, tmpDirectory);

                DataCore.Structures.IndexEntry indexEntry = Core.GetEntry(ref index, fileName);

                if (indexEntry != null)
                {
                    GUI.Instance.UpdateStatus(0, string.Format("Updating indexed file: {0}...", fileName));
                    Core.UpdateFileEntry(ref index, settings.GetString("clientdirectory"), filePath, 0);
                    Core.Save(ref index, settings.GetString("clientdirectory"), false);
                }
            }

            GUI.Instance.UpdateStatus(1, "Cleaning up...");

            // Delete the zip
            File.Delete(zipPath);

            // Increase the currentIndex
            iterateCurrentIndex();
        }

        private void iterateCurrentIndex()
        {
            this.currentIndex++;
            if (this.currentIndex == this.FileList.Count) { GUI.Instance.OnUpdateComplete(); }
            else { compareFiles(); }
        }
    }
}
