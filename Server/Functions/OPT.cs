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
        /// <summary>
        /// Dictionary holding all the settings loaded from portal.opt
        /// </summary>
        protected static Dictionary<string, string> SettingsList = new Dictionary<string, string>();
        protected static List<string> legacyIndex = new List<string>();
        protected static List<string> deleteIndex = new List<string>();

        public static bool SettingExists(string key) { return (SettingsList[key] != null) ? true : false; }

        public static string GetString(string key) { return (SettingExists(key)) ? SettingsList[key] : null; }

        public static bool GetBool(string key) { return (SettingExists(key)) ? Convert.ToBoolean(Convert.ToInt32(SettingsList[key])) : false; }

        public static int GetInt(string key) { return (SettingExists(key)) ? Convert.ToInt32(SettingsList[key]) : 0; }

        public static double GetDouble(string key) { return (SettingExists(key)) ? Convert.ToDouble(SettingsList[key]) : 0.0; }

        public static bool UpdateSetting(string key, string value)
        {
            if (SettingsList[key] != null) { SettingsList[key] = value; return true; }

            return false;
        }

        public static bool IsLegacy(string key) { return legacyIndex.Contains(key); }

        public static int LegacyCount { get { return legacyIndex.Count; } }

        public static bool IsDelete(string key) { return deleteIndex.Contains(key); }

        public static int DeleteCount { get { return deleteIndex.Count; } }

        /// <summary>
        /// Parses: portal.opt, legacy.opt, legacy_delete.opt into their appropriate Dictionary and List variables
        /// </summary>
        public static void LoadSettings()
        {
            // TODO: Implement trigger to read gIndex.opt
            if (File.Exists("portal.opt"))
            {
                Console.Write("Loading settings in: portal.opt...");

                using (StreamReader sR = new StreamReader("portal.opt"))
                {
                    string currentLineValue = null;
                    while ((currentLineValue = sR.ReadLine()) != null)
                    {
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
                Console.WriteLine("[Fail]\n\t-The portal.opt does not exist, create it!");
            }
        }

        public static void LoadLegacyIndex()
        {
            if (File.Exists("legacy.opt"))
            {
                using (StreamReader sr = new StreamReader("legacy.opt"))
                {
                    string currentLineValue = null;
                    while ((currentLineValue = sr.ReadLine()) != null) { legacyIndex.Add(currentLineValue); }
                }
            }
            else { Console.WriteLine("The legacy.opt does not exist, create it!"); }
        }

        public static void AlterLegacyIndex(string fileName, bool add) { if (add) legacyIndex.Add(fileName); else legacyIndex.Remove(fileName); }

        public static void LoadDeleteFiles()
        {
            if (File.Exists("delete.opt"))
            {
                using (StreamReader sr = new StreamReader("delete.opt"))
                {
                    string currentLineValue = null;
                    while ((currentLineValue = sr.ReadLine()) != null) { deleteIndex.Add(currentLineValue); }
                }
            }
            else { Console.WriteLine("The delete.opt does not exist, create it!"); }
        }

        public static void AlterDeleteIndex(string fileName, bool add) { if (add) deleteIndex.Add(fileName); else deleteIndex.Remove(fileName); }

        public static void SaveIndex(int type)
        {
            string name = (type < 1) ? "legacy" : "delete";
            string fileName = string.Concat(name, ".opt");

            Output.WriteAndLock(new Structures.Message() { Text = string.Format("Attempting to save: {0}.opt...", name) });

            if (File.Exists(fileName)) { File.Delete(fileName); }

            try { using (StreamWriter sW = new StreamWriter(File.Create(fileName))) { foreach (string entryName in legacyIndex) { sW.Write(string.Concat(entryName, "\n")); } } }
            catch (Exception ex) { Output.WriteAndUnlock(new Structures.Message() { Text = string.Format("[Fail]\nError:{0}", ex.Message), AddBreak = true }); }
            finally { Output.WriteAndUnlock(new Structures.Message() { Text = "[OK]", AddBreak = true }); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public static void SaveSettings()
        {
            Output.WriteAndLock(new Structures.Message() { Text = "Attempting to save: Portal.opt..." });

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
            catch (Exception ex) { Output.WriteAndUnlock(new Structures.Message() { Text = string.Format("[Fail]\nError: {0}", ex.Message), AddBreak = true }); }
            finally { Output.WriteAndUnlock(new Structures.Message() { Text = "[OK]", AddBreak = true }); }
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
