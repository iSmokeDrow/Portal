using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Client.Structures;

namespace Client.Functions
{
    public class OPT
    {
        internal string optName = "config.opt";
        internal string optPath;
        internal XDes des;
        internal string encKey = "dXDd92j34";
        internal List<LauncherSetting> settingsList = new List<LauncherSetting>();
        internal List<string> defaultSettings = new List<string>
        {
            "s|clientdirectory|",
            "s|username|",
            "s|password|",
            "s|pin|",
            "b|remember|false",
            "s|codepage|ASCII",
            "s|country|US",
            "b|showfps|false",
            "b|ontop|false",
            "b|closeonstart|true",
            "b|logreports|false",
            "b|logerrors|false"
        };

        public OPT()
        {
            des = new XDes(encKey);
            optPath = Path.Combine(Directory.GetCurrentDirectory(), optName);
        }

        public void Start()
        {
            // Check that the config.opt exists
            if (!optExists) { preloadDefaults(); writeDefaultOPT(); }
            else { readOPT(); }
        }

        internal bool optExists
        {
            get { return File.Exists(optPath); }
        }

        public string GetStringValue(string name)
        {
            LauncherSetting setting = settingsList.Find(s => s.Name == name);
            return setting.Value.ToString();
        }

        public bool GetBoolValue(string name)
        {
            LauncherSetting setting = settingsList.Find(s => s.Name == name);
            return Convert.ToBoolean(setting.Value);
        }

        internal void UpdateValue(string name, object value)
        {
            settingsList.Find(s => s.Name == name).Value = value;
        }

        private void preloadDefaults()
        {
            if (settingsList.Count > 0) { settingsList.Clear(); }

            foreach (string setting in defaultSettings)
            {
                string[] optBlocks = setting.Split('|');
                if (optBlocks.Length == 3)
                {
                    string type = optBlocks[0];
                    string name = optBlocks[1];
                    object value = null;

                    switch (type)
                    {
                        case "s": // string
                            value = optBlocks[2].TrimEnd('\0');
                            break;

                        case "b": // bool
                            value = Convert.ToBoolean(optBlocks[2]);
                            break;
                    }

                    if (!string.IsNullOrEmpty(name) && value != null) { settingsList.Add(new LauncherSetting() { Type = type, Name = name, Value = value }); }
                }
            }
        }

        internal void writeDefaultOPT()
        {
            using (FileStream fs = new FileStream(optPath, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    foreach (string setting in defaultSettings)
                    {
                        byte[] encBuffer = des.Encrypt(setting);
                        bw.Write(Convert.ToInt32(encBuffer.Length));
                        bw.Write(encBuffer);
                    }
                }
            }           
        }

        internal void readOPT()
        {
            bool deleteOpt = false;

            try
            {
                using (BinaryReader br = new BinaryReader(File.Open(optPath, FileMode.Open, FileAccess.Read), Encoding.ASCII))
                {
                    while (br.PeekChar() != -1)
                    {
                        int len = 0;
                        len = br.ReadInt32();
                        string[] optBlocks = des.Decrypt(br.ReadBytes(len)).Split('|');

                        if (optBlocks.Length == 3)
                        {
                            string type = optBlocks[0];
                            string name = optBlocks[1];
                            object value = null;

                            switch (type)
                            {
                                case "s": // string
                                    value = optBlocks[2].TrimEnd('\0');
                                    break;

                                case "b": // bool
                                    value = Convert.ToBoolean(optBlocks[2]);
                                    break;
                            }

                            if (!string.IsNullOrEmpty(name) && value != null) { settingsList.Add(new LauncherSetting() { Type = type, Name = name, Value = value }); }
                        }
                        else
                        {
                            deleteOpt = true;
                            break;
                        }
                    }
                }
            }
            catch
            {
                deleteOpt = true;
            }

            if (deleteOpt)
            {
                GUI.Instance.UpdateStatus(0, "Malformed OPT File, Resetting...");
                File.Delete("config.opt");
                preloadDefaults();
                writeDefaultOPT();
            }
        }

        internal void writeOPT()
        {
            Task.Run(() => 
            {
                using (BinaryWriter bw = new BinaryWriter(File.Open(optPath, FileMode.OpenOrCreate, FileAccess.Write), Encoding.ASCII))
                {
                    foreach (LauncherSetting setting in settingsList)
                    {
                        string encString = encString = string.Format("{0}|{1}|{2}", setting.Type, setting.Name, setting.Value.ToString());
                        byte[] encBuffer = des.Encrypt(encString);
                        bw.Write(Convert.ToInt32(encBuffer.Length));
                        bw.Write(encBuffer);

                    }
                }
            });
        }
    }
}
