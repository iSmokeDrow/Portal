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
            foreach(UpdateIndex ind in this.FileList)
            {
                File.AppendAllText("dump.txt", "\r\n" + ind.FileName + " | " + ind.FileHash + " | " + ind.IsLegacy);
            }
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
                    if (!File.Exists("Resource/" + file.FileName) || (Hash.GetSHA512Hash("Resource/" + file.FileName) != file.FileHash))
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
            // TODO : Download File
        }

        private void OnUpdateDownloadCompleted()
        {
            // TODO : Apply update

            CheckFiles();
        }
    }
}
