using Client.Network;
using System.Diagnostics;
using DataCore;
using DataCore.Structures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Client.Structures;

namespace Client.Functions
{
    public class UpdateHandler
    {
        private string indexPath;
        private string resourceFolder;
        private string disabledFolder;
        private string tempPath;

        private const string patcherUrl = "http://176.31.181.127:13546/";

        private readonly Core core;

        private List<IndexEntry> index;

        internal OPT settings = OPT.Instance;

        internal static UpdateHandler instance;

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

        private string GetFileExtension(string fileName) { return (core.IsEncoded(fileName)) ? Path.GetExtension(core.DecodeName(fileName)).Remove(0, 1).ToLower() : Path.GetExtension(fileName).Remove(0, 1).ToLower(); }

        public bool NetworkError = false;

        public List<UpdateIndex> FileList { get; set; }
        
        private int currentIndex { get; set; }

        internal bool isLegacy = false;

        internal List<UpdateIndex> filteredUpdates;

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
            core.CurrentMaxDetermined += (o, x) => { guiInstance.Invoke(new MethodInvoker(delegate { guiInstance.currentProgress.Maximum = Convert.ToInt32(x.Maximum); })); };
            core.CurrentProgressChanged += (o, x) =>
            {
                guiInstance.Invoke(new MethodInvoker(delegate
                {
                    guiInstance.currentProgress.Value = Convert.ToInt32(x.Value);
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
            resourceFolder = string.Concat(guiInstance.SettingsManager.GetString("clientdirectory"), @"/Resource/");
            disabledFolder = string.Concat(guiInstance.SettingsManager.GetString("clientdirectory"), @"/Disabled/");
            tempPath = string.Concat(Directory.GetCurrentDirectory(), @"/tmp/");
            this.FileList = new List<UpdateIndex>();
        }

        public void Start()
        {
            guiInstance.UpdateStatus(0, "Loading Client index...");
            indexPath = Path.Combine(guiInstance.SettingsManager.GetString("clientdirectory"), "data.000");
            index = core.Load(indexPath, false, 64000);

            ServerPackets.Instance.RequestUpdateDateTime();
        }

        // TODO: Send trigger saying to check for legacy or neo updates?
        // TODO: Set flags for the instance of UpdateHandler that determine if Neo/Legacy or Both updates should be processed for updateIndex
        public void OnUpdateDateTimeReceived(DateTime dateTime)
        {
            // TODO: Clarify this text
            guiInstance.UpdateStatus(0, "Checking for updates...");

            DateTime indexDateTime = File.GetLastWriteTimeUtc(indexPath);
            DateTime resourceDateTime = Directory.GetLastWriteTime(resourceFolder);
            bool updateRequired = false;
            int updateType = 0;

            // TODO: Request indexes seperately
            if (indexDateTime < dateTime && resourceDateTime >= dateTime) { updateRequired = true; updateType = 1; } // Neo Only
            else if (indexDateTime >= dateTime && resourceDateTime < dateTime) { updateRequired = true; updateType = 2; } // Legacy Only
            else if (indexDateTime < dateTime && resourceDateTime < dateTime) { updateRequired = true; updateType = 3; }

            if (updateRequired && updateType > 0)
                ServerPackets.Instance.RequestUpdateIndex(updateType);

            else { guiInstance.OnUpdateComplete(); }
        }

        public void ExecuteSelfUpdate(string fileName)
        {
            DownloadSelfUpdate(fileName);
        }

        internal void ExecuteUpdaterUpdate(string fileName)
        {
            guiInstance.UpdateStatus(0, "Downloading Updater...");

            string zipPath = Path.Combine(tempPath, string.Concat(fileName, ".zip"));
            WebClient client = new WebClient();
            client.DownloadFileCompleted += (o, x) => 
            {
                if (File.Exists(zipPath))
                {
                    File.Delete("Updater.exe");
                    ZIP.Unpack(zipPath, Directory.GetCurrentDirectory());
                    File.Delete(zipPath);
                }
            };
            string url = String.Concat(patcherUrl, fileName, ".zip");
            client.DownloadFileAsync(new Uri(url), Path.Combine(tempPath, string.Concat(fileName, ".zip")), client);
        }

        // TODO: Remove isLegacy from this function chain
        public void OnUpdateIndexReceived(string fileName, string hash, bool isLegacy)
        {
            this.FileList.Add(new UpdateIndex() { FileName = fileName, FileHash = hash, IsLegacy = isLegacy });
        }

        public void OnUpdateIndexEnd(int indexType)
        {
            this.currentIndex = 0;
            guiInstance.UpdateProgressMaximum(0, this.FileList.Count);
            guiInstance.UpdateStatus(0, "Updating Client files...");

            switch (indexType)
            {
                case 1: // New
                    checkFiles();
                    break;

                case 2: // Legacy
                    checkLegacyFiles();
                    break;
                case 3: //
                    checkFiles();
                    checkLegacyFiles();
                    break;
            }
        }

        internal void checkFiles()
        {
            // Split only the currently relevant files
            filteredUpdates = new List<UpdateIndex>();
            filteredUpdates = FileList.FindAll(i => i.IsLegacy == false);

            guiInstance.UpdateStatus(0, "Checking indexed data files...");

            for (; this.currentIndex < this.filteredUpdates.Count; ++this.currentIndex)
            {
                guiInstance.UpdateProgressValue(0, this.currentIndex);

                UpdateIndex file = this.filteredUpdates[this.currentIndex];
                bool download = false;

                guiInstance.UpdateStatus(1, string.Format("Checking file: {0}", file.FileName));

                IndexEntry fileEntry = core.GetEntry(ref index, file.FileName);
                if (fileEntry != null)
                {
                    string fileHash = core.GetFileSHA512(settings.GetString("clientdirectory"), core.GetID(fileEntry.Name), fileEntry.Offset, fileEntry.Length, GetFileExtension(fileEntry.Name));

                    if (file.FileHash != fileHash)
                    {
                        guiInstance.UpdateStatus(1, string.Format("File: {0} is depreciated!", file.FileName));
                        download = true;
                    }
                }

                if (download) { DoUpdate(); break; }
            }

            if (this.currentIndex == this.FileList.Count)
            {
                guiInstance.UpdateProgressMaximum(0, 100);
                guiInstance.UpdateProgressValue(0, 0);
                guiInstance.UpdateStatus(0, "Saving Client index...");
                core.Save(ref index, settings.GetString("clientDirectory"), false, false);
                guiInstance.OnUpdateComplete();
            }

            filteredUpdates.Clear();
        }

        internal void checkLegacyFiles()
        {
            guiInstance.UpdateStatus(0, "Checking Resource Files...");

            for (; this.currentIndex < this.FileList.Count; ++this.currentIndex)
            {
                guiInstance.UpdateProgressValue(0, this.currentIndex);

                UpdateIndex file = this.FileList[this.currentIndex];
                bool download = false;

                guiInstance.UpdateStatus(1, string.Format("Checking resource: {0}", file.FileName));

                if (!File.Exists(resourceFolder + file.FileName) || (Hash.GetSHA512Hash(resourceFolder + file.FileName) != file.FileHash))
                {
                    download = true;
                    isLegacy = true;
                }

                if (download) { DoUpdate(); break; }
            }

            if (this.currentIndex == this.FileList.Count)
            {
                guiInstance.UpdateProgressMaximum(0, 100);
                guiInstance.UpdateProgressValue(0, 0);
                guiInstance.OnUpdateComplete();
            }
        }

        private void DownloadSelfUpdate(string fileName)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"Updater.exe";
            startInfo.Arguments = String.Concat(patcherUrl, fileName, ".zip") + " " + fileName;
            Process.Start(startInfo);
        }

        private void DoUpdate()
        {
            string name = this.FileList[this.currentIndex].FileName;
            ServerPackets.Instance.RequestFile(name, 0, "");
        }

        internal void OnUpdateFileNameReceived(string path)
        {
            string name = String.Concat(this.FileList[this.currentIndex].FileName, ".zip");
            string filePath = Path.Combine(tempPath, name);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            guiInstance.UpdateStatus(1, string.Format("Downloading file: {0}", name.Replace(".zip", string.Empty)));

            WebClient client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            string url = String.Concat(patcherUrl, path, ".zip");
            client.DownloadFileAsync(new Uri(url), filePath, client);
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            // TODO : Update progress
            if (guiInstance.currentProgress.Maximum == 100) { guiInstance.UpdateProgressMaximum(1, (int)e.TotalBytesToReceive); }
            if (guiInstance.currentProgress.Maximum > 100) { guiInstance.UpdateProgressValue(1, e.ProgressPercentage); }
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            WebClient client = (WebClient)e.UserState;
            client.Dispose();
            UpdateIndex file = this.FileList[this.currentIndex];
            
            string zipPath = Path.Combine(tempPath, string.Concat(file.FileName, ".zip"));
            string resourcePath = Path.Combine(tempPath, file.FileName);

            // Check to make sure tmp exists
            if (!Directory.Exists(tempPath)) { Directory.CreateDirectory(tempPath); }

            // Unzip the file
            ZIP.Unpack(zipPath, tempPath);

            if (isLegacy)
            {
                if (File.Exists(resourceFolder + file.FileName)) { File.Delete(resourceFolder + file.FileName); }
                File.Move(resourcePath, resourceFolder + file.FileName);
                isLegacy = false;
            }
            else { core.UpdateFileEntry(ref index, guiInstance.SettingsManager.GetString("clientdirectory"), resourcePath, 0); }
            
            guiInstance.UpdateStatus(1, string.Format("Verifying update: {0}", file.FileName));

            // Check that the file update actually took
            if (Hash.GetSHA512Hash(resourcePath) == file.FileHash)
            {
                currentIndex++;
                File.Delete(resourcePath);
                File.Delete(zipPath);
                disableMatchingResource();
            }

            guiInstance.UpdateStatus(1, "");
            guiInstance.UpdateProgressMaximum(1, 100);
            guiInstance.UpdateProgressValue(1, 0);

            checkFiles();
        }


        internal void disableMatchingResource()
        {
            // Make sure /disabled/ exists, or create it
            if (!Directory.Exists(disabledFolder)) { Directory.CreateDirectory(disabledFolder); }

            string fileName = this.FileList[this.currentIndex].FileName;
            string cfPath = Path.Combine(resourceFolder, fileName);
            string nfPath = Path.Combine(disabledFolder, fileName);

            if (File.Exists(cfPath))
            {
                File.Move(cfPath, nfPath);
                MessageBox.Show(string.Format("A conflicting resource with the name: {0} was detected!\nThis file has been disabled and can be found in the /disabled/ folder of your client.", fileName), "Update Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
