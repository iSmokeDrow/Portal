using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Client.Structures
{
    public class UserSettings
    {
        string _username;
        string _password;
        string _pin;
        bool _remember;
        string _codepage;
        string _country;
        bool _showFPS;
        bool _onTop;
        bool _closeOnStart;
        bool _detailedReports;
        bool _logReports;
        bool _detailedErrors;
        bool _logErrors;
        string _clientDirectory;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Pin
        {
            get { return _pin; }
            set { _pin = value; }
        }

        public bool Remember
        {
            get { return _remember; }
            set { _remember = value; }
        }

        public string Codepage
        {
            get { return _codepage; }
            set { _codepage = value; }
        }

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        public bool ShowFPS
        {
            get { return _showFPS; }
            set { _showFPS = value; }
        }

        public bool AlwaysOnTop
        {
            get { return _onTop; }
            set { _onTop = value; }
        }

        public bool CloseOnStart
        {
            get { return _closeOnStart; }
            set { _closeOnStart = value; }
        }

        public bool DetailedReports
        {
            get { return _detailedReports; }
            set { _detailedReports = value; }
        }

        public bool LogReports
        {
            get { return _logReports; }
            set { _logReports = value; }
        }

        public bool DetailedErrors
        {
            get { return _detailedErrors; }
            set { _detailedErrors = value; }
        }

        public bool LogErrors
        {
            get { return _logErrors; }
            set { _logErrors = value; }
        }

        public string ClientDirectory
        {
            get { return _clientDirectory; }
            set { _clientDirectory = value; }
        }

        public UserSettings()
        {
            Username = Properties.Settings.Default.username;
            Password = Properties.Settings.Default.password;
            Pin = Properties.Settings.Default.pin;
            Remember = Properties.Settings.Default.remember;
            Codepage = Properties.Settings.Default.codepage;
            Country = Properties.Settings.Default.country;
            ShowFPS = Properties.Settings.Default.showFPS;
            AlwaysOnTop = Properties.Settings.Default.onTop;
            CloseOnStart = Properties.Settings.Default.onTop;
            LogReports = Properties.Settings.Default.logReports;
            LogErrors = Properties.Settings.Default.logErrors;
            ClientDirectory = Properties.Settings.Default.clientDirectory;
        }

        public void Save()
        {
            Properties.Settings.Default.username = _username;
            Properties.Settings.Default.password = _password;
            Properties.Settings.Default.pin = _pin;
            Properties.Settings.Default.remember = _remember;
            Properties.Settings.Default.codepage = _codepage;
            Properties.Settings.Default.country = _country;
            Properties.Settings.Default.showFPS = _showFPS;
            Properties.Settings.Default.onTop = _onTop;
            Properties.Settings.Default.onTop = _closeOnStart;
            Properties.Settings.Default.logReports = _logReports;
            Properties.Settings.Default.logErrors = _logErrors;
            Properties.Settings.Default.clientDirectory = _clientDirectory;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }
    }

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

    public class ClientSettings
    {
        string _name;
        object _value;

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

    public class SettingsManager
    {
        internal static Properties.Settings settings = Properties.Settings.Default;

        public static ClientSettings[] RappelzSettings;

        internal static string optPath = Path.Combine(settings.clientDirectory, @"rappelz_v1.opt");

        public static void InitRappelzSettings()
        {
            RappelzSettings = new ClientSettings[File.ReadLines(optPath).Count() - 1];
            ReadOPT_v1();
        }

        public static void ReadOPT_v1()
        {
            try
            {
                using (StreamReader sr = new StreamReader(File.Open(optPath, FileMode.Open, FileAccess.Read)))
                {
                    if (sr.ReadLine() == "[RAPPELZ]")
                    {
                        for (int i = 0; i < RappelzSettings.Length; i++)
                        {
                            string currentLine = sr.ReadLine();

                            if (currentLine.Contains('='))
                            {
                                string[] lineBlocks = currentLine.Split('=');

                                ClientSettings currentSetting = new ClientSettings { Name = lineBlocks[0], Value = lineBlocks[1] };
                                RappelzSettings[i] = currentSetting;
                            }
                            else
                            {
                                ClientSettings currentSetting = new ClientSettings { Name = currentLine, Value = null };
                                RappelzSettings[i] = currentSetting;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bad Header", "OPT Exception #0", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception Ex) { MessageBox.Show(Ex.ToString(), "OPT Exception #1", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public static bool SaveSettings(ClientSettings[] settings)
        {
            using (StreamWriter sw = new StreamWriter(File.Open(optPath, FileMode.Open, FileAccess.Write), Encoding.Default))
            {
                sw.Write("[RAPPELZ]\n");

                for (int i = 0; i < settings.Length; i++)
                {
                    ClientSettings currentSetting = settings[i];
                    string output = String.Empty;
                    if (currentSetting.Name.Contains('[')) { output = string.Concat(currentSetting.Name, "\n"); }
                    else { output = string.Format("{0}={1}\n", currentSetting.Name, currentSetting.Value); }
                    sw.Write(output);
                }
            }

            return false;
        }

    }

    public class ClientSettingsOld
    {
        int _resolutionWidth;
        int _resolutionHeight;
        int _refresh;
        bool _windowed;
        bool _fullWindowed;
        int _brightness;
        int _gfxPreset;
        int _environmentDistance;
        int _terrainDistance;
        int _propDistance;
        int _speedDistance;
        int _grassDistance;
        int _characterDistance;
        int _shadowDistance;
        int _textureQuality;
        bool _bloom;
        bool _waterReflections;
        bool _treeDetailed;
        bool _enchantmentFX;
        bool _treeOn;
        bool _grassOn;
        bool _lightMap;
        bool _desktopBrightness;
        bool _lastingFX;
        bool _lastingFX_cs;
        bool _lastingFX_p;
        bool _lastingFX_o;
        bool _effect;
        bool _effect_cs;
        bool _effect_p;
        bool _effect_o;
        bool _minStructures;
        bool _showOthers;
        bool _shadows;
        bool _shadowLowQuality;
        int _selfShadowsQuality;
        int _speedQuality;
        bool _masterMute;
        int _masterVolume;
        bool _effectMute;
        int _effectVolume;
        bool _musicMute;
        int _musicVolume;
        bool _ambientMute;
        int _ambientVolume;    
        bool _chatBalloon;
        bool _chatHide;
        bool _mantleHide;
        bool _showHPGauge;
        bool _showPlayerHP;
        bool _showMobHP;
        bool _showTargetHP;
        bool _showMobAvatar;
        bool _showName;
        bool _showPlayerName;
        bool _showCreatureName;
        bool _showMobName;
        bool _showNPCName;
        bool _showDamage;
        bool _showPlayerDamage;
        bool _showCreatureDamage;
        bool _showPartyDamage;
        bool _winLock;
        bool _winAlpha;
        bool _showHelm;
        bool _showDeco;
        bool _showTitle;
        bool _showTargetOutline;
        bool _criticalShake;
        int _weatherQuality;
        bool _cameraCollision;

        public int ResolutionWidth
        {
            get { return _resolutionWidth; }
            set { _resolutionWidth = value; }
        }

        public int ResolutionHeight
        {
            get { return _resolutionHeight; }
            set { _resolutionHeight = value; }
        }

        public int Refresh
        {
            get { return _refresh; }
            set { _refresh = value; }
        }

        public bool Windowed
        {
            get { return _windowed; }
            set { _windowed = value; }
        }

        public bool FullWindowed
        {
            get { return _fullWindowed; }
            set { _fullWindowed = value; }
        }

        public int Brightness
        {
            get { return _brightness; }
            set { _brightness = value; }
        }

        public int GFXPreset
        {
            get { return _gfxPreset; }
            set { _gfxPreset = value; }
        }

        public int EnvironmentDistance
        {
            get { return _environmentDistance; }
            set { _environmentDistance = value; }
        }

        public int TerrainDistance
        {
            get { return _terrainDistance; }
            set { _terrainDistance = value; }
        }

        public int PropDistance
        {
            get { return _propDistance; }
            set { _propDistance = value; }
        }

        public int SpeedDistance
        {
            get { return _speedDistance; }
            set { _speedDistance = value; }
        }

        public int GrassDistance
        {
            get { return _grassDistance; }
            set { _grassDistance = value; }
        }

        public int CharacterDistance
        {
            get { return _characterDistance; }
            set { _characterDistance = value; }
        }

        public int ShadowDistance
        {
            get { return _shadowDistance; }
            set { _shadowDistance = value; }
        }

        public int TextureQuality
        {
            get { return _textureQuality; }
            set { _textureQuality = value; }
        }

        public bool Bloom
        {
            get { return _bloom; }
            set { _bloom = value; }
        }

        public bool WaterReflections
        {
            get { return _waterReflections; }
            set { _waterReflections = value; }
        }

        public bool TreeDetailed
        {
            get { return _treeDetailed; }
            set { _treeDetailed = value; }
        }

        public bool EnchantmentFX
        {
            get { return _enchantmentFX; }
            set { _enchantmentFX = value; }
        }

        public bool TreeOn
        {
            get { return _treeOn; }
            set { _treeOn = value; }
        }

        public bool GrassOn
        {
            get { return _grassOn; }
            set { _grassOn = value; }
        }

        public bool LightMap
        {
            get { return _lightMap; }
            set { _lightMap = value; }
        }

        public bool UseDesktopBrightness
        {
            get { return _desktopBrightness; }
            set { _desktopBrightness = value; }
        }

        public bool LastingFX
        {
            get { return _lastingFX; }
            set { _lastingFX = value; }
        }

        public bool LastingFX_CreatureAndSelf
        {
            get { return _lastingFX_cs; }
            set { _lastingFX_cs = value; }
        }

        public bool LastingFX_Party
        {
            get { return _lastingFX_p; }
            set { _lastingFX_p = value; }
        }

        public bool LastingFX_Other
        {
            get { return _lastingFX_o; }
            set { _lastingFX_o = value; }
        }

        public bool Effect
        {
            get { return _effect; }
            set { _effect = value; }
        }

        public bool Effect_CreatureAndSelf
        {
            get { return _effect_cs; }
            set { _effect_cs = value; }
        }

        public bool Effect_Party
        {
            get { return _effect_p; }
            set { _effect_p = value; }
        }

        public bool Effect_Other
        {
            get { return _effect_o; }
            set { _effect_o = value; }
        }

        public bool MinimumStructures
        {
            get { return _minStructures; }
            set { _minStructures = value; }
        }

        public bool Shadows
        {
            get { return _shadows; }
            set { _shadows = value; }
        }

        public bool ShadowsLowQuality
        {
            get { return _shadowLowQuality; }
            set { _shadowLowQuality = value; }
        }

        public int SelfShadowsQuality
        {
            get { return _selfShadowsQuality; }
            set { _selfShadowsQuality = value; }
        }

        public int SpeedQuality
        {
            get { return _speedQuality; }
            set { _speedQuality = value; }
        }

        public bool ShowOthersInTown
        {
            get { return _showOthers; }
            set { _showOthers = value; }
        }

        public bool MasterMuted
        {
            get { return _masterMute; }
            set { _masterMute = value; }
        }

        public int MasterVolume
        {
            get { return _masterVolume; }
            set { _masterVolume = value; }
        }

        public bool EffectMuted
        {
            get { return _effectMute; }
            set { _effectMute = value; }
        }

        public int EffectVolume
        {
            get { return _effectVolume; }
            set { _effectVolume = value; }
        }

        public bool MusicMuted
        {
            get { return _musicMute; }
            set { _musicMute = value; }
        }

        public int MusicVolume
        {
            get { return _musicVolume; }
            set { _musicVolume = value; }
        }

        public bool AmbientMuted
        {
            get { return _ambientMute; }
            set { _ambientMute = value; }
        }

        public int AmbientVolumne
        {
            get { return _ambientVolume; }
            set { _ambientVolume = value; }
        }

        public bool ChatBalloons
        {
            get { return _chatBalloon; }
            set { _chatBalloon = value; }
        }

        public bool ChatHidden
        {
            get { return _chatHide; }
            set { _chatHide = value; }
        }

        public bool ShowMantle
        {
            get { return _mantleHide; }
            set { _mantleHide = value; }
        }

        public bool ShowHPGauge
        {
            get { return _showHPGauge; }
            set { _showHPGauge = value; }
        }

        public bool ShowPlayerHP
        {
            get { return _showPlayerHP; }
            set { _showPlayerHP = value; }
        }

        public bool ShowMobHP
        {
            get { return _showMobHP; }
            set { _showMobHP = value; }
        }

        public bool ShowTargetHP
        {
            get { return _showTargetHP; }
            set { _showTargetHP = value; }
        }

        public bool ShowName
        {
            get { return _showName; }
            set { _showName = value; }
        }

        public bool ShowPlayerName
        {
            get { return _showPlayerName; }
            set { _showPlayerName = value; }
        }

        public bool ShowCreatureName
        {
            get { return _showCreatureName; }
            set { _showCreatureName = value; }
        }

        public bool ShowMobNames
        {
            get { return _showMobName; }
            set { _showMobName = value; }
        }

        public bool ShowNPCName
        {
            get { return _showNPCName; }
            set { _showNPCName = value; }
        }

        public bool ShowMobAvatar
        {
            get { return _showMobAvatar; }
            set { _showMobAvatar = value; }
        }

        public bool ShowDamage
        {
            get { return _showDamage; }
            set { _showDamage = value; }
        }

        public bool ShowPlayerDamage
        {
            get { return _showPlayerDamage; }
            set { _showPlayerDamage = value; }
        }

        public bool ShowCreatureDamage
        {
            get { return _showCreatureDamage; }
            set { _showCreatureDamage = value; }
        }

        public bool ShowPartyDamage
        {
            get { return _showPlayerDamage; }
            set { _showPlayerDamage = value; }
        }

        public bool WinLock
        {
            get { return _winLock; }
            set { _winLock = value; }
        }

        public bool WinAlpha
        {
            get { return _winAlpha; }
            set { _winAlpha = value; }
        }

        public bool ShowHelm
        {
            get { return _showHelm; }
            set { _showHelm = value; }
        }

        public bool ShowDeco
        {
            get { return _showDeco; }
            set { _showDeco = value; }
        }

        public bool ShowTitles
        {
            get { return _showTitle; }
            set { _showTitle = value; }
        }

        public bool ShowTargetOutline
        {
            get { return _showTargetOutline; }
            set { _showTargetOutline = value; }
        }

        public bool CriticalCameraShake
        {
            get { return _criticalShake; }
            set { _criticalShake = value; }
        }

        public int WeatherQuality
        {
            get { return _weatherQuality; }
            set { _weatherQuality = value; }
        }

        public bool CameraCollisions
        {
            get { return _cameraCollision; }
            set { _cameraCollision = value; }
        }
    }
}
