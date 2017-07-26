using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Config_Reader
{
    public class LauncherSetting
    {
        string _type;
        string _name;
        object _value;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    public class OPT
    {
        internal string optName = "config.opt";
        internal string optPath;
        internal XDes des;
        internal string encKey = "dXDd92j34";
        internal List<LauncherSetting> settingsList = new List<LauncherSetting>();


        public OPT()
        {
            des = new XDes(encKey);
            optPath = Path.Combine(Directory.GetCurrentDirectory(), optName);
        }

        public void Start()
        {
            // Check that the config.opt exists
            if (!optExists) { Console.WriteLine("config.opt doesn't exist"); }
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
            writeOPT();
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

                                case "n":
                                    value = Convert.ToInt32(optBlocks[2]);
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
