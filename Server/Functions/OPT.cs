using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Functions
{
    /// <summary>
    /// Provides access to the various settings files used by Portal
    /// </summary>
    public class OPT
    {
        // TODO: Replace legacy_delete.opt with Delete bool in UpdateIndex

        /// <summary>
        /// Dictionary holding all the settings loaded from portal.opt
        /// </summary>
        protected static Dictionary<string, string> SettingsList = new Dictionary<string, string>();

        public static bool SettingExists(string key)
        {
            return (SettingsList[key] != null) ? true : false;
        }

        public static string GetString(string key)
        {
            return (SettingExists(key)) ? SettingsList[key] : null;
        }

        public static bool GetBool(string key)
        {
            return (SettingExists(key)) ? Convert.ToBoolean(Convert.ToInt32(SettingsList[key])) : false;
        }

        public static int GetInt(string key)
        {
            return (SettingExists(key)) ? Convert.ToInt32(SettingsList[key]) : 0;
        }

        public static bool UpdateSetting(string key, string value)
        {
            if (SettingsList[key] != null) { SettingsList[key] = value; return true; }

            return false;
        }

        /// <summary>
        /// Parses: portal.opt, legacy.opt, legacy_delete.opt into their appropriate Dictionary and List variables
        /// </summary>
        public static void LoadSettings()
        {
            Console.Write("Loading settings in: portal.opt...");

            if (File.Exists("portal.opt"))
            {
                using (StreamReader sR = new StreamReader("portal.opt"))
                {
                    int currentLineIdx = 0;
                    string currentLineValue = null;
                    while ((currentLineValue = sR.ReadLine()) != null)
                    {
                        currentLineIdx++;

                        if (!currentLineValue.StartsWith("#"))
                        {
                            //Break the line 
                            string[] lineBlocks = currentLineValue.Split(':');
                            string settingName = lineBlocks[0];
                            string settingValue = lineBlocks[1];
                            SettingsList.Add(settingName, settingValue);
                        }
                    }

                    Console.WriteLine("[OK]");
                }
            }
            else
            {
                Console.WriteLine("[Fail]\n\t** The portal.opt does not exist, create it.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public static void SaveSettings(int type)
        {
            //MainWindow.Instance.MessageConsole.AppendText("Attempting to save: ");

            if (type == 0)
            {
                //MainWindow.Instance.MessageConsole.AppendText("portal.opt...");

                if (File.Exists("portal.opt")) { File.Delete("portal.opt"); }

                try
                {
                    using (StreamWriter sW = new StreamWriter(File.Create("portal.opt")))
                    {
                        foreach (KeyValuePair<string, string> pair in SettingsList)
                        {
                            sW.Write(string.Format("{0}:{1}\n", pair.Key, pair.Value));
                        }
                    }
                }
                catch
                { 
                    //MainWindow.Instance.MessageConsole.AppendText("[Fail]\n");
                }
                finally
                {
                    //MainWindow.Instance.MessageConsole.AppendText("[Success]\n");
                }
            }
        }

        /// <summary>
        /// Reparses: portal.opt, legacy.opt, legacy_delete.opt into their appriopriate Dictionary and List variables
        /// </summary>
        /// <param name="type"></param>
        /// <param name="all"></param>
        public static void ReloadSettings(int type, bool all)
        {
            if (type == 0 || all)
            {
                SettingsList.Clear();

                //MainWindow.Instance.MessageConsole.AppendText("Reloading settings in: portal.opt...");

                if (File.Exists("portal.opt"))
                {
                    using (StreamReader sR = new StreamReader("portal.opt"))
                    {
                        int currentLineIdx = 0;
                        string currentLineValue = null;
                        while ((currentLineValue = sR.ReadLine()) != null)
                        {
                            currentLineIdx++;

                            //Break the line 
                            string[] lineBlocks = currentLineValue.Split(':');
                            string settingName = lineBlocks[0];
                            string settingValue = lineBlocks[1];
                            SettingsList.Add(settingName, settingValue);
                        }

                        //MainWindow.Instance.MessageConsole.AppendText("[Success]\n");
                    }
                }
                else
                {
                    //MainWindow.Instance.MessageConsole.AppendText("[Fail]\n\t** The portal.opt does not exist, create it.\n");
                }
            }
        }
    }
}
