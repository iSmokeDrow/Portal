using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Updater.Structures;

namespace Updater.Functions
{
    public static class OPT
    {
        internal static string optName = "config.opt";
        internal static string optPath = Path.Combine(Directory.GetCurrentDirectory(), optName);
        internal static XDes des = new XDes("dXDd92j34");
        internal static List<LauncherSetting> settingsList = new List<LauncherSetting>();

        internal static bool optExists
        {
            get { return File.Exists(optPath); }
        }

        public static int GetInt(string name)
        {
            LauncherSetting setting = settingsList.Find(s => s.Name == name);
            return (setting != null) ? Convert.ToInt32(setting.Value) : 0;
        }

        public static string GetString(string name)
        {
            LauncherSetting setting = settingsList.Find(s => s.Name == name);
            return (setting != null) ? setting.Value.ToString() : null;
        }

        public static bool GetBool(string name)
        {
            LauncherSetting setting = settingsList.Find(s => s.Name == name);
            return (setting != null) ? Convert.ToBoolean(setting.Value) : false;
        }

        internal static void Read()
        {
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
                            break;
                        }
                    }
                }
            }
            catch
            {
                // TODO: Print error
            }
        }
    }
}
