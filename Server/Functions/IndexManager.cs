using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using Server.Structures;

namespace Server.Functions
{
    public static class IndexManager
    {
        internal static List<IndexEntry> Index = new List<IndexEntry>();

        public static int Count { get { return Index.Count; } }

        public static int DataCount { get { return Filter(FilterType.Data).Count; } }

        public static int ResourceCount { get { return Filter(FilterType.Resource).Count; } }

        public static string UpdatesDirectory
        {
            get
            {
                if (!string.IsNullOrEmpty(OPT.GetString("update.dir"))) { return OPT.GetString("update.dir"); }
                else { return string.Format(@"{0}/{1}", System.IO.Directory.GetCurrentDirectory(), "/updates/"); }
            }
        }

        public static void Build(bool rebuild)
        {
            Output.WriteAndLock(new Message() { Text = string.Format("{0} the Update Index...", (rebuild && OPT.GetBool("debug")) ? "Rebuilding" : "Building") });

            if (rebuild) { Program.Wait = true; }

            if (Index.Count > 0) { Index.Clear(); }

            switch (OPT.GetInt("send.type"))
            {
                case 0: // Google drive 
                    using (StreamReader sr = new StreamReader(File.Open(string.Format(@"{0}\{1}", System.IO.Directory.GetCurrentDirectory(), "gIndex.opt"), FileMode.Open, FileAccess.Read)))
                    {
                        //string line;
                        //while ((line = sr.ReadLine()) != null)
                        //{
                        //    string[] optBlocks = line.Split('|');
                        //    if (optBlocks.Length == 4)
                        //    {
                        //        Index.Add(new IndexEntry { FileName = optBlocks[0], SHA512 = optBlocks[1], Legacy = Convert.ToBoolean(Convert.ToInt32(optBlocks[2])), Delete = Convert.ToBoolean(Convert.ToInt32(optBlocks[3])) });
                        //    }
                        //}
                    }
                    break;

                case 1: // HTTP
                    break;

                case 2: // FTP
                    break;

                case 3: // TCP
                    foreach (string filePath in System.IO.Directory.GetFiles(UpdatesDirectory))
                    {
                        string fileName = Path.GetFileName(filePath);

                        Index.Add(new IndexEntry
                        {
                            FileName = fileName,
                            SHA512 = Hash.GetSHA512Hash(filePath),
                            Legacy = OPT.IsLegacy(fileName),
                            Delete = OPT.IsDelete(fileName)
                        });
                    }
                    break;
            }

            if (OPT.GetBool("debug")) { Output.WriteAndUnlock(new Message() { Text = string.Format("[OK]\n\t- {0} files indexed", Count), AddBreak = true }); }

            if (rebuild) { Program.Wait = false; }

            GUI.Instance.updatesViewBtn.Enabled = true;
            GUI.Instance.updatesView.Enabled = true;
        }

        public static List<IndexEntry> Filter(FilterType type)
        {
            switch (type)
            {
                case FilterType.Data:
                    return Index.FindAll(i => !i.Legacy);

                case FilterType.Resource:
                    return Index.FindAll(i => i.Legacy);
            }

            return null;
        }

        public static bool EntryExists(string name, FilterType type)
        {
            switch (type)
            {
                case FilterType.Data:
                    return Index.Find(i => !i.Legacy && i.FileName == name) != null;

                case FilterType.Resource:
                    return Index.Find(i => i.Legacy && i.FileName == name) != null;
            }

            return false;
        }
    }
}
