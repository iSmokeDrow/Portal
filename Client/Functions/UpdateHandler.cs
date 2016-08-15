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

        private string GetFileExtension(string fileName) { return (Core.IsEncoded(fileName)) ? Path.GetExtension(Core.DecodeName(fileName)).Remove(0, 1).ToLower() : Path.GetExtension(fileName).Remove(0, 1).ToLower(); }

        public bool NetworkError = false;

        public List<Structures.IndexEntry> FileList { get; set; }
        
        private int currentIndex { get; set; }

        internal bool isLegacy = false;

        internal List<Structures.IndexEntry> filteredUpdates;

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

        public void Start()
        {
            indexPath = Path.Combine(OPT.Instance.GetString("clientdirectory"), "data.000");
            index = Core.Load(indexPath, false, 64000);

            if (!Directory.Exists(disabledFolder)) { Directory.CreateDirectory(disabledFolder); }
            if (!Directory.Exists(tmpDirectory)) { Directory.CreateDirectory(tmpDirectory); }

            ServerPackets.Instance.RequestUpdateDateTime();
        }

        public void OnUpdateDateTimeReceived(DateTime dateTime)
        {
            guiInstance.UpdateStatus(0, "Checking times...");

            DateTime indexDateTime = File.GetLastWriteTimeUtc(indexPath);
            DateTime resourceDateTime = Directory.GetLastWriteTime(resourceFolder);
            bool updateRequired = false;
            int updateType = 0;

            if (indexDateTime < dateTime && resourceDateTime == dateTime) { updateRequired = true; updateType = 1; } // Neo Only
            else if (indexDateTime == dateTime && resourceDateTime < dateTime) { updateRequired = true; updateType = 2; } // Legacy Only
            else if (indexDateTime < dateTime && resourceDateTime < dateTime) { updateRequired = true; updateType = 3; } // Both

            if (updateRequired && updateType > 0)
                ServerPackets.Instance.RequestUpdateIndex(updateType);

            else { guiInstance.OnUpdateComplete(); }
        }

        // TODO: Update me to use google drive for file fetching
        public void ExecuteSelfUpdate(string fileName)
        {
            DownloadSelfUpdate(fileName);
        }

        // TODO: Upgrade to use Drive.cs
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
            // Filter  only the currently relevant files
            filteredUpdates = new List<Structures.IndexEntry>();
            filteredUpdates = FileList.FindAll(i => i.IsLegacy == false);

            guiInstance.UpdateStatus(0, "Checking indexed data files...");

            // Loop through the filteredUpdates
            for (; this.currentIndex < this.filteredUpdates.Count; ++this.currentIndex)
            {
                guiInstance.UpdateProgressValue(0, this.currentIndex);
                
                // Collect the current index entries information
                Structures.IndexEntry file = this.filteredUpdates[this.currentIndex];
                bool download = false;

                guiInstance.UpdateStatus(1, string.Format("Checking file: {0}", file.FileName));

                // Collect relevant data from the loaded data.000 index (if existing)
                DataCore.Structures.IndexEntry fileEntry = Core.GetEntry(ref index, file.FileName);
                if (fileEntry != null)
                {
                    // Compute file SHA512 hash (of file stored in data.xxx file system)
                    string fileHash = Core.GetFileSHA512(settings.GetString("clientdirectory"), Core.GetID(fileEntry.Name), fileEntry.Offset, fileEntry.Length, GetFileExtension(fileEntry.Name));
                    
                    // If the local/remote file hashes don't match, flag this file for remote download
                    if (file.FileHash != fileHash)
                    {
                        guiInstance.UpdateStatus(1, string.Format("File: {0} is depreciated!", file.FileName));
                        download = true;
                    }
                }

                // If the file has been flagged, ex
                if (download) { DoUpdate(false); break; }
            }

            if (this.currentIndex == this.FileList.Count)
            {
                currentIndex = 0;
                guiInstance.UpdateProgressMaximum(0, 100);
                guiInstance.UpdateProgressValue(0, 0);
                guiInstance.UpdateStatus(0, "Saving Client index...");
                Core.Save(ref index, settings.GetString("clientDirectory"), false, false);
                guiInstance.OnUpdateComplete();
            }
        }

        internal void checkLegacyFiles()
        {
            filteredUpdates = new List<Structures.IndexEntry>();
            filteredUpdates = FileList.FindAll(i => i.IsLegacy == true);

            guiInstance.UpdateStatus(0, "Checking Resource Files...");

            for (; this.currentIndex < this.filteredUpdates.Count; ++this.currentIndex)
            {
                guiInstance.UpdateProgressValue(0, this.currentIndex);

                Structures.IndexEntry file = this.FileList[this.currentIndex];
                bool download = false;

                guiInstance.UpdateStatus(1, string.Format("Checking resource: {0}", file.FileName));

                if (!File.Exists(resourceFolder + file.FileName) || (Hash.GetSHA512Hash(resourceFolder + file.FileName) != file.FileHash))
                {
                    download = true;
                }

                if (download) { DoUpdate(true); break; }
            }

            if (this.currentIndex == this.FileList.Count)
            {
                currentIndex = 0;
                guiInstance.UpdateProgressMaximum(0, 100);
                guiInstance.UpdateProgressValue(0, 0);
                guiInstance.OnUpdateComplete();
            }
        }

        private void DownloadSelfUpdate(string fileName)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"Updater.exe";
            //startInfo.Arguments = String.Concat(patcherUrl, fileName, ".zip") + " " + fileName;
            Process.Start(startInfo);
        }

        private void DoUpdate(bool isLegacy)
        {
            string name = this.filteredUpdates[this.currentIndex].FileName;

            guiInstance.UpdateStatus(1, string.Format("Downloading file: {0}", name));

            gDrive.Start();
            string filePath = gDrive.GetFile(name);

            guiInstance.UpdateStatus(1, string.Format("Unpacking {0}...", name));

            if (isLegacy)
            {
                string legacyPath = string.Format(@"{0}\{1}", resourceFolder, name);
                string disabledPath = string.Format(@"{0}\{1}", disabledFolder, name);
                if (File.Exists(legacyPath)) { File.Move(legacyPath, disabledPath); }
                ZIP.Unpack(filePath, resourceFolder);
                File.Move(legacyPath, string.Format(@"{0}\{1}", resourceFolder, Core.EncodeName(name)));
                File.Delete(filePath);
            }
            else
            {
                string decodedName = Core.DecodeName(name);
                string encodedFilePath = string.Format(@"{0}\{1}", tmpDirectory, name);
                string decodedFilePath = string.Format(@"{0}\{1}", tmpDirectory, decodedName);
                if (File.Exists(decodedFilePath)) { File.Delete(decodedFilePath); }
                ZIP.Unpack(filePath, tmpDirectory);
                File.Move(decodedFilePath, encodedFilePath);
                Core.UpdateFileEntry(ref index, settings.GetString("clientdirectory"), string.Format(@"{0}\{1}", tmpDirectory, name), 0);
                File.Delete(filePath);
                Core.Save(ref index, settings.GetString("clientdirectory"), false, false);
            }
        }
    }
}
