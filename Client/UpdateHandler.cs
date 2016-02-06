using Client.Functions;
using Client.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class UpdateHandler
    {
        private string ResourceFolder = "Resource/";

        public static readonly UpdateHandler Instance = new UpdateHandler();

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
            this.FileList = new List<UpdateIndex>();
            Directory.CreateDirectory(ResourceFolder);
        }

        public void Start()
        {
            ServerPackets.Instance.RequestUpdateIndex();
        }

        public void OnUpdateIndexReceived(string fileName, string hash, bool isLegacy)
        {
            this.FileList.Add(new UpdateIndex() { FileName = fileName, FileHash = hash, IsLegacy = isLegacy });
        }

        public void OnUpdateIndexEnd()
        {
            this.CurrentIndex = 0;
            CheckFiles();
        }

        private void CheckFiles()
        {
            for (; this.CurrentIndex < this.FileList.Count; ++this.CurrentIndex)
            {
                UpdateIndex file = this.FileList[this.CurrentIndex];
                bool download = false;

                if (file.IsLegacy)
                {
                    if (!File.Exists(ResourceFolder + file.FileName) || (Hash.GetSHA512Hash(ResourceFolder + file.FileName) != file.FileHash))
                    {
                        download = true;
                    }
                }
                else
                {
                    // TODO : Check file inside data.xxx
                }

                if (download)
                {
                    DoUpdate();
                    break;
                }
            }

            if (this.CurrentIndex == this.FileList.Count)
            {
                GUI.OnUpdateComplete();
            }
        }

        private void DoUpdate()
        {
            string name = this.FileList[this.CurrentIndex].FileName;
            int offset = 0;
            string partialHash = "";

            if (File.Exists(name))
            {
                byte[] currentData = File.ReadAllBytes(name);
                partialHash = Hash.GetSHA512Hash(currentData, currentData.Length);
                offset = currentData.Length;
            }
            ServerPackets.Instance.RequestFile(name, offset, partialHash);
        }

        public void OnFileDataReceived(int offset, bool isEOF, byte[] data)
        {
            string name = this.FileList[this.CurrentIndex].FileName;

            if (offset == 0 && File.Exists(name))
            {
                File.Delete(name);
            }

            using (FileStream fs = File.OpenWrite(name))
            {
                fs.Seek(offset, SeekOrigin.Begin);
                fs.Write(data, 0, data.Length);
                fs.Close();
            }

            if (isEOF)
            {
                OnUpdateDownloadCompleted();
            }
        }

        private void OnUpdateDownloadCompleted()
        {
            UpdateIndex file = this.FileList[this.CurrentIndex];
            
            if (file.IsLegacy)
            {
                if (File.Exists(ResourceFolder + file.FileName))
                {
                    File.Delete(ResourceFolder + file.FileName);
                }
                File.Move(file.FileName, ResourceFolder + file.FileName);
            }
            else
            {
                // TODO : Apply file inside data.xxx
            }
            
            CheckFiles();
        }
    }
}
