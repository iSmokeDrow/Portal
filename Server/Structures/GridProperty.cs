using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;
using Server.Functions;

namespace Server.Structures
{
    public class GridProperties
    {
        [Description("Changes some Portal behaviours like logging"), Category("Debug"), DisplayName("Debug Mode")]
        public bool Debug { get { return OPT.GetBool("debug"); } set { OPT.UpdateSetting("debug", Convert.ToInt32(value).ToString()); } }

        [Description("Determines if connecting users will Authenticate by logging into the Launcher (true) or the Game Client (false)")
        Category("Authentication")]
        public bool IMBC { get { return OPT.GetBool("imbc.login"); } set { OPT.UpdateSetting("imbc.login", Convert.ToInt32(value).ToString()); } }

        [Description("The IP which Launchers will connect to. (e.g. 127.0.0.1 / 92.12.33.11"), Category("Connections"), DisplayName("I/O IP")]
        public string IP { get { return OPT.GetString("io.ip"); } set { OPT.UpdateSetting("io.ip", value.ToString()); } }

        [Description("The Port which Launchers will connect to."), Category("Connections"), DisplayName("I/O Port")]
        public int Port { get { return OPT.GetInt("io.port"); } set { OPT.UpdateSetting("io.port", value.ToString()); } }

        [Description("IP where Game Clients can reach the Auth Server (e.g. PrincessAurora or Glandu Auth Emu)"), Category("Connections"), DisplayName("Auth I/O IP")]
        public string Auth_IP { get { return OPT.GetString("auth.io.ip"); } set { OPT.UpdateSetting("auth.io.ip", value.ToString()); } }

        [Description("Port where Game Clients can reach the Auth Server (e.g. PrincessAurora or Glandu Auth Emu)"), Category("Connections"), DisplayName("Auth I/O IP")]
        public int Auth_Port { get { return OPT.GetInt("auth.io.port"); } set { OPT.UpdateSetting("auth.io.port", value.ToString()); } }

        [Description("Determines if connecting Launchers will update their Game Client."), Category("Updates")]
        public bool Disable { get { return OPT.GetBool("disable.updating"); } set { OPT.UpdateSetting("disable.updating", Convert.ToInt32(value).ToString()); } }

        [Description("The path to the folder where the HASHED update files are\nLEAVE THIS BLANK IF USING /updates/ folder!"), Category("Updates")]
        public string Directory { get { return OPT.GetString("update.dir"); } set { OPT.UpdateSetting("update.dir", value.ToString()); } }

        [Description("The amount of time in seconds between Update Index rebuilds (e.g. Refreshing the Portal Updates List)"), Category("Updates"), DisplayName("Interval")]
        public int Rebuild_Interval { get { return OPT.GetInt("rebuild.interval"); } set { OPT.UpdateSetting("rebuild.interval", value.ToString()); } }

        [Description("A special key used in the encryption of sensitive information send between the Portal and connecting Launchers."), Category("Security"), DisplayName("DES Key")]
        public string Des_Key { get { return OPT.GetString("des.key"); } set { OPT.UpdateSetting("des.key", value.ToString()); } }

        [Description("A special key used in the encryption of sensitive information like user passwords when Launchers authenticate."), Category("Security"), DisplayName("MD5 Key")]
        public string MD5_Key { get { return OPT.GetString("md5.key"); } set { OPT.UpdateSetting("md5.key", value.ToString()); } }

        [Description("IP where the Portal can connect to Database holding your Auth table"), Category("Database"), DisplayName("IP"), DefaultValue("127.0.0.1")]
        public string DB_Auth_IP { get { return OPT.GetString("db.auth.ip"); } set { OPT.UpdateSetting("db.auth.ip", value.ToString()); } }

        [Description("Determines if the Portal should connect to your Database using Trusted Credentials (this will not require username/password)"), Category("Database"),
        DisplayName("Trusted Connection")]
        public bool DB_Auth_Trusted { get { return OPT.GetBool("db.auth.trusted"); } set { OPT.UpdateSetting("db.auth.trusted", Convert.ToInt32(value).ToString()); } }

        [Description("The name of you Auth database (generally: Auth)"), Category("Database"), DisplayName("Database Name")]
        public string DB_Auth_Name { get { return OPT.GetString("db.auth.name"); } set { OPT.UpdateSetting("db.auth.name", value.ToString()); } }

        [Description("The username with permission to connect to your Database (not needed if DB Auth Trusted Connection is true"), Category("Database"), DisplayName("Username")]
        public string DB_Auth_User { get { return OPT.GetString("db.auth.user"); } set { OPT.UpdateSetting("db.auth.user", value.ToString()); } }

        [Description("The password for the username with permission to connect to your Database."), Category("Database"), DisplayName("Password")]
        public string DB_Auth_Pass { get { return OPT.GetString("db.auth.password"); } set { OPT.UpdateSetting("db.auth.password", value.ToString()); } }

        [Description("The name of your Accounts table in the Auth Database. (e.g. Account / Accounts)\nDO NOT LEAVE BLANK!"), Category("Database"), DisplayName("Account Table Alias"),
        DefaultValue("Accounts")]
        public string DB_Auth_Table_Alias { get { return OPT.GetString("db.auth.table.alias"); } set { OPT.UpdateSetting("db.auth.table.alias", value.ToString()); } }

        [Description("Defines the way the Launcher will attempt to download update files.\n 0 - Google Drive / 1 - HTTP / 2 - FTP / 3 - TCP"), Category("File Transmission"),
        DisplayName("Send Type")]
        public int Send_Type { get { return OPT.GetInt("send.type"); } set { OPT.UpdateSetting("send.type", value.ToString()); } }

        [Description("The Google Drive Service Account used to connect to Google Drive.\nONLY USE WITH Send Type 3!"), Category("Google Drive Credentials"), DisplayName("Service Account")]
        public string Gdrive_Account { get { return OPT.GetString("gdrive.account"); } set { OPT.UpdateSetting("gdrive.account", value.ToString()); } }

        [Description("The Google Drive Service API used to connect to Google Drive.\nONLY USE WITH Send Type 3!"), Category("Google Drive Credentials"), DisplayName("Service API Key")]
        public string Gdrive_Api { get { return OPT.GetString("gdrive.api.key"); } set { OPT.UpdateSetting("gdrive.api.key", value.ToString()); } }

        // TODO: Implement url base
        [Description("The base URL from which Launcher will attempt to download update files from.\n{e.g. http://yourwebsite/updates/)"), Category("HTTP Downloads"), 
        DisplayName("Base URL")]
        public string URL_Base { get; set; }

        // TODO: Impelement me
        [Description("IP that Launchers will use to connect to FTP Server for downloading update files"), Category("FTP Credentials"), DisplayName("IP")]
        public string FTP_IP { get { return OPT.GetString("ftp.ip"); } set { OPT.UpdateSetting("ftp.ip", value.ToString()); } }

        [Description("Port that Launchers will use to connect to FTP Server for downloading update files"), Category("FTP Credentials"), DisplayName("Port")]
        public int FTP_Port { get { return OPT.GetInt("ftp.port"); } set { OPT.UpdateSetting("ftp.port", value.ToString()); } }

        [Description("Username that Launchers will use to connect to FTP Server for downloading update files"), Category("FTP Credentials"), DisplayName("Username")]
        public string FTP_Username { get { return OPT.GetString("ftp.username"); } set { OPT.UpdateSetting("ftp.username", value.ToString()); } }

        [Description("Password that Launchers will use to connect to FTP Server for downloading update files"), Category("FTP Credentials"), DisplayName("Password")]
        public string FTP_Password { get { return OPT.GetString("ftp.password"); } set { OPT.UpdateSetting("ftp.password", value.ToString()); } }

        [Description("Amount of time (in seconds) a connecting Launcher will be told to wait if the Launcher is processing priority resources"), Category("Waiting"), DisplayName("Wait Period")]
        public int Wait_Period { get { return OPT.GetInt("wait.period"); } set { OPT.UpdateSetting("wait.period", value.ToString()); } }

        [Description("Determines if connecting Launchers will be allowed to Launch their Game Client because server is in Maintenance."), Category("Maintenance"),
        DisplayName("Maintenance On"), ReadOnly(true)]
        public bool Maintenance { get { return OPT.GetBool("maintenance"); }  set { OPT.UpdateSetting("maintenance", value.ToString()); } }

        [Description("Determines if connecting Launchers should use 8.1+ Game Client signed startup when launching their Game Client"), Category("SFrame"), DisplayName("SFrame Bypass")]
        public bool SFrame_Bypass { get { return OPT.GetBool("sframe.bypass"); } set { OPT.UpdateSetting("sframe.bypass", Convert.ToInt32(value).ToString()); } }

        [Description("Determines if connecting Launchers should be able to start multiple Game Clients"), Category("SFrame"), DisplayName("Double Execute")]
        public bool Double_Exec { get { return OPT.GetBool("double.execute"); } set { OPT.UpdateSetting("double.execute", Convert.ToInt32(value).ToString()); } }
    }
}
