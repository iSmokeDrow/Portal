using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Structures;

namespace Client
{
    //TODO: Check for missing settings (in rappelz_v1.opt) like: titles, outline
    public partial class RappelzSettingsGUI : Form
    {
        public bool Save = false;

        protected bool settingDefaults = false;

        public List<ClientSettings> Settings;

        public RappelzSettingsGUI(List<ClientSettings> inSettings)
        {
            InitializeComponent();

            Settings = inSettings;

            setValues();
        }

        void setValues()
        {
            settingDefaults = true;

            foreach (ClientSettings setting in Settings)
            {
                string currentName = setting.Name;
                int currentValue = Convert.ToInt32(setting.Value);

                if (currentName == "GRAPHIC_RESOLUTION_WIDTH") { resolutionWidth.Value = currentValue; }
                if (currentName == "GRAPHIC_RESOLUTION_HEIGHT") { resolutionHeight.Value = currentValue; }
                if (currentName == "GRAPHIC_REFRESHRATE") 
                {
                    if (currentValue == 0) { refreshRate.Text = "60 Hz"; } else { refreshRate.Text = "75 Hz"; }
                }
                if (currentName == "GRAPHIC_WINMODE") { windowed.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_BACKGROUNDDIS") { enviromentDistance.Value = currentValue; }
                if (currentName == "GRAPHIC_TERRAINDIS") { terrainDistance.Value = currentValue; }
                if (currentName == "GRAPHIC_PROPDIS") { propDistance.Value = currentValue; }
                if (currentName == "GRAPHIC_SPEEDDIS") { speedDistance.Value = currentValue; }
                if (currentName == "GRAPHIC_GRASSDIS") { grassDistance.Value = currentValue; }
                if (currentName == "GRAPHIC_AVATARDIS") { characterDistance.Value = currentValue; }
                if (currentName == "GRAPHIC_SHADOWDIS") { shadowDistance.Value = currentValue; }
                if (currentName == "GRAPHIC_LOWSHADOW") { lowQualityShadows.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_SPEEDQUAL") { speedQuality.Value = currentValue; }
                if (currentName == "GRAPHIC_MIPBIAS") { textureQuality.Value = currentValue; }
                if (currentName == "GRAPHIC_SELFSHADOWQUAL") { shadowSelfQuality.Value = currentValue; }
                if (currentName == "GRAPHIC_GLOWQUAL") { bloom.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_WATERQUAL") { waterReflections.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_BRIGHT") { brightness.Value = currentValue; }
                if (currentName == "GRAPHIC_WINBRIGHT") { useDesktopBrightness.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_PRESETOPTION") { gfxPreset.Value = currentValue; }
                if (currentName == "GRAPHIC_TOWNLIMIT") { minStructures.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_TREEALPHA") { betterTrees.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_ENHANCE") { enchantmentEffects.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_STATE_EFFECT") { lastingEffects.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_STATE_EFFECT_CREATURE") { lastingEffects_cs.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_STATE_EFFECT_PARTY") { lastingEffects_p.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_STATE_EFFECT_OTHER") { lastingEffects_o.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_SHADOW") { shadows.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_RENDERTREE") { trees.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_RENDERGRASS") { grass.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_LIGHT") { lightMap.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_EFFECT") { effect.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_EFFECT_CREATURE") { effect_cs.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_EFFECT_PARTY") { effect_p.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_EFFECT_ENEMY") { effect_o.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "GRAPHIC_EFFECT_ENEMY") { effect_o.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_MOBFACE") { monsterAvatar.Checked = Convert.ToBoolean(currentValue); }
                //if (currentName == "GRAPHIC_FACE_ANI", );
                if (currentName == "GRAPHIC_RENDER_OTHERPLAYER") { showOthers.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_CHATBALLOON") { chatBalloons.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_ENTERCHAT") { enterToChat.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_MANTLE") { cloaks.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_HPGAGE") { hpGauges.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_PLAYERGAGE") { playerHPGauge.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_MOBGAGE") { mobHPGauge.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_TARGETGAGE") { targetHPGauge.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_NAME") { names.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_PLAYERNAME") { playerNames.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_CREATURENAME") { creatureNames.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_MOBNAME") { monsterNames.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_NPCNAME") { npcNames.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_DAMAGE") { damage.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_PLAYERDM") { playerDamage.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_CREATUREDM") { creatureDamage.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_WEATHER_QUALITY") { weatherQuality.Value = currentValue; }
                if (currentName == "PLAY_CAMERA_COLLISION") { cameraCollisions.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_CRITICAL_CAMERA") { cameraCriticalShake.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_HELM") { helmets.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_AVATAR_DECO") { deco.Checked = Convert.ToBoolean(currentValue); }
                //if (currentName == "PLAY_TITLE_SHOW") { titles.Checked = Convert.ToBoolean(currentValue); }
                //if (currentName == "PLAY_SELECT_OUTLINE_SHOW") { targetOutline.Checked = Convert.ToBoolean(currentValue); }
            }

            settingDefaults = false;
        }

        void volumeMixerlb_Click(object sender, EventArgs e)
        {
            using (RappelzVolumeGUI volumeMixer = new RappelzVolumeGUI(RappelzSettings.Settings))
            {
                volumeMixer.ShowDialog(this);
            }
        }

        void gfxPreset_ValueChanged(object sender, EventArgs e)
        {
            if (settingDefaults) { return; }

            if (gfxPreset.Value == 0)
            {
                RappelzSettings.Update("GRAPHIC_PRESETOPTION", "0");
                setValues();
            }

            if (gfxPreset.Value == 1) // Minimum
            {
                RappelzSettings.Update("GRAPHIC_PRESETOPTION", "1");
                RappelzSettings.Update("GRAPHIC_REFRESHRATE", "0");
                RappelzSettings.Update("GRAPHIC_BACKGROUNDDIS", "0");
                RappelzSettings.Update("GRAPHIC_TERRAINDIS", "0");
                RappelzSettings.Update("GRAPHIC_PROPDIS", "0");
                RappelzSettings.Update("GRAPHIC_SPEEDDIS", "0");
                RappelzSettings.Update("GRAPHIC_GRASSDIS", "0");
                RappelzSettings.Update("GRAPHIC_AVATARDIS", "0");
                RappelzSettings.Update("GRAPHIC_SHADOWDIS", "0");
                RappelzSettings.Update("GRAPHIC_GLOWQUAL", "0");
                RappelzSettings.Update("GRAPHIC_WATERQUAL", "0");
                RappelzSettings.Update("GRAPHIC_RENDERTREE", "0");
                RappelzSettings.Update("GRAPHIC_TREEALPHA", "0");
                RappelzSettings.Update("GRAPHIC_ENHANCE", "0");
                RappelzSettings.Update("GRAPHIC_RENDERGRASS", "0");
                RappelzSettings.Update("GRAPHIC_LIGHT", "0");
                RappelzSettings.Update("GRAPHIC_SHADOW", "0");
                RappelzSettings.Update("GRAPHIC_LOWSHADOW", "0");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT", "0");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT_CREATURE", "0");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT_PARTY", "0");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT_ENEMY", "0");
                RappelzSettings.Update("GRAPHIC_EFFECT", "0");
                RappelzSettings.Update("GRAPHIC_EFFECT_CREATURE", "0");
                RappelzSettings.Update("GRAPHIC_EFFECT_PARTY", "0");
                RappelzSettings.Update("GRAPHIC_EFFECT_ENEMY", "0");
                RappelzSettings.Update("PLAY_MOBFACE", "1");
                //RappelzSettings.Update("PLAY_SELECT_OUTLINE_SHOW", "0");
                RappelzSettings.Update("PLAY_MANTLE", "0");
                RappelzSettings.Update("PLAY_HELM", "0");
                RappelzSettings.Update("PLAY_AVATAR_DECO", "0");
                //RappelzSettings.Update("PLAY_TITLE_SHOW", "1");
                RappelzSettings.Update("PLAY_NAME", "1");
                RappelzSettings.Update("PLAY_PLAYERNAME", "1");
                RappelzSettings.Update("PLAY_CREATURENAME", "1");
                RappelzSettings.Update("PLAY_MOBNAME", "1");
                RappelzSettings.Update("PLAY_NPCNAME", "1");
                RappelzSettings.Update("PLAY_HPGAGE", "1");
                RappelzSettings.Update("PLAY_PLAYERGAGE", "1");
                RappelzSettings.Update("PLAY_MOBGAGE", "1");
                RappelzSettings.Update("PLAY_TARGETGAGE", "1");
                RappelzSettings.Update("PLAY_DAMAGE", "1");
                RappelzSettings.Update("PLAY_PLAYERDM", "1");
                RappelzSettings.Update("PLAY_CREATUREDM", "1");
                RappelzSettings.Update("GRAPHIC_TOWNLIMIT", "1");
                RappelzSettings.Update("GRAPHIC_RENDER_OTHERPLAYER", "0");
                RappelzSettings.Update("GRAPHIC_MIPBIAS", "0");
                RappelzSettings.Update("GRAPHIC_SPEEDQUAL", "0");
                RappelzSettings.Update("GRAPHIC_SELFSHADOWQUAL", "0");
                RappelzSettings.Update("GRAPHIC_WATERQUAL", "0");
                RappelzSettings.Update("PLAY_WEATHER_QUALITY", "0");

                setValues();
            }

            if (gfxPreset.Value == 2) // Low
            {
                RappelzSettings.Update("GRAPHIC_PRESETOPTION", "2");
                RappelzSettings.Update("GRAPHIC_REFRESHRATE", "0");
                RappelzSettings.Update("GRAPHIC_BACKGROUNDDIS", "1");
                RappelzSettings.Update("GRAPHIC_TERRAINDIS", "1");
                RappelzSettings.Update("GRAPHIC_PROPDIS", "1");
                RappelzSettings.Update("GRAPHIC_SPEEDDIS", "1");
                RappelzSettings.Update("GRAPHIC_GRASSDIS", "1");
                RappelzSettings.Update("GRAPHIC_AVATARDIS", "1");
                RappelzSettings.Update("GRAPHIC_SHADOWDIS", "1");
                RappelzSettings.Update("GRAPHIC_GLOWQUAL", "0");
                RappelzSettings.Update("GRAPHIC_WATERQUAL", "0");
                RappelzSettings.Update("GRAPHIC_RENDERTREE", "1");
                RappelzSettings.Update("GRAPHIC_TREEALPHA", "0");
                RappelzSettings.Update("GRAPHIC_ENHANCE", "1");
                RappelzSettings.Update("GRAPHIC_RENDERGRASS", "1");
                RappelzSettings.Update("GRAPHIC_LIGHT", "0");
                RappelzSettings.Update("GRAPHIC_SHADOW", "0");
                RappelzSettings.Update("GRAPHIC_LOWSHADOW", "0");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT", "1");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT_CREATURE", "1");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT_PARTY", "0");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT_ENEMY", "0");
                RappelzSettings.Update("GRAPHIC_EFFECT", "1");
                RappelzSettings.Update("GRAPHIC_EFFECT_CREATURE", "1");
                RappelzSettings.Update("GRAPHIC_EFFECT_PARTY", "0");
                RappelzSettings.Update("GRAPHIC_EFFECT_ENEMY", "0");
                RappelzSettings.Update("PLAY_MOBFACE", "1");
                //RappelzSettings.Update("PLAY_SELECT_OUTLINE_SHOW", "0");
                RappelzSettings.Update("PLAY_MANTLE", "0");
                RappelzSettings.Update("PLAY_HELM", "0");
                RappelzSettings.Update("PLAY_AVATAR_DECO", "0");
                //RappelzSettings.Update("PLAY_TITLE_SHOW", "1");
                RappelzSettings.Update("PLAY_NAME", "1");
                RappelzSettings.Update("PLAY_PLAYERNAME", "1");
                RappelzSettings.Update("PLAY_CREATURENAME", "1");
                RappelzSettings.Update("PLAY_MOBNAME", "1");
                RappelzSettings.Update("PLAY_NPCNAME", "1");
                RappelzSettings.Update("PLAY_HPGAGE", "1");
                RappelzSettings.Update("PLAY_PLAYERGAGE", "1");
                RappelzSettings.Update("PLAY_MOBGAGE", "1");
                RappelzSettings.Update("PLAY_TARGETGAGE", "1");
                RappelzSettings.Update("PLAY_DAMAGE", "1");
                RappelzSettings.Update("PLAY_PLAYERDM", "1");
                RappelzSettings.Update("PLAY_CREATUREDM", "1");
                RappelzSettings.Update("GRAPHIC_TOWNLIMIT", "1");
                RappelzSettings.Update("GRAPHIC_RENDER_OTHERPLAYER", "0");
                RappelzSettings.Update("GRAPHIC_MIPBIAS", "1");
                RappelzSettings.Update("GRAPHIC_SPEEDQUAL", "1");
                RappelzSettings.Update("GRAPHIC_SELFSHADOWQUAL", "1");
                RappelzSettings.Update("GRAPHIC_WATERQUAL", "1");
                RappelzSettings.Update("PLAY_WEATHER_QUALITY", "1");

                setValues();
            }

            if (gfxPreset.Value == 3) //Medium
            {
                RappelzSettings.Update("GRAPHIC_PRESETOPTION", "3");
                RappelzSettings.Update("GRAPHIC_REFRESHRATE", "0");
                RappelzSettings.Update("GRAPHIC_BACKGROUNDDIS", "2");
                RappelzSettings.Update("GRAPHIC_TERRAINDIS", "2");
                RappelzSettings.Update("GRAPHIC_PROPDIS", "2");
                RappelzSettings.Update("GRAPHIC_SPEEDDIS", "2");
                RappelzSettings.Update("GRAPHIC_GRASSDIS", "2");
                RappelzSettings.Update("GRAPHIC_AVATARDIS", "2");
                RappelzSettings.Update("GRAPHIC_SHADOWDIS", "2");
                RappelzSettings.Update("GRAPHIC_GLOWQUAL", "0");
                RappelzSettings.Update("GRAPHIC_WATERQUAL", "1");
                RappelzSettings.Update("GRAPHIC_RENDERTREE", "1");
                RappelzSettings.Update("GRAPHIC_TREEALPHA", "0");
                RappelzSettings.Update("GRAPHIC_ENHANCE", "1");
                RappelzSettings.Update("GRAPHIC_RENDERGRASS", "1");
                RappelzSettings.Update("GRAPHIC_LIGHT", "0");
                RappelzSettings.Update("GRAPHIC_SHADOW", "1");
                RappelzSettings.Update("GRAPHIC_LOWSHADOW", "1");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT", "1");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT_CREATURE", "1");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT_PARTY", "1");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT_ENEMY", "0");
                RappelzSettings.Update("GRAPHIC_EFFECT", "1");
                RappelzSettings.Update("GRAPHIC_EFFECT_CREATURE", "1");
                RappelzSettings.Update("GRAPHIC_EFFECT_PARTY", "1");
                RappelzSettings.Update("GRAPHIC_EFFECT_ENEMY", "0");
                RappelzSettings.Update("PLAY_MOBFACE", "1");
                //RappelzSettings.Update("PLAY_SELECT_OUTLINE_SHOW", "0");
                RappelzSettings.Update("PLAY_MANTLE", "1");
                RappelzSettings.Update("PLAY_HELM", "1");
                RappelzSettings.Update("PLAY_AVATAR_DECO", "1");
                //RappelzSettings.Update("PLAY_TITLE_SHOW", "1");
                RappelzSettings.Update("PLAY_NAME", "1");
                RappelzSettings.Update("PLAY_PLAYERNAME", "1");
                RappelzSettings.Update("PLAY_CREATURENAME", "1");
                RappelzSettings.Update("PLAY_MOBNAME", "1");
                RappelzSettings.Update("PLAY_NPCNAME", "1");
                RappelzSettings.Update("PLAY_HPGAGE", "1");
                RappelzSettings.Update("PLAY_PLAYERGAGE", "1");
                RappelzSettings.Update("PLAY_MOBGAGE", "1");
                RappelzSettings.Update("PLAY_TARGETGAGE", "1");
                RappelzSettings.Update("PLAY_DAMAGE", "1");
                RappelzSettings.Update("PLAY_PLAYERDM", "1");
                RappelzSettings.Update("PLAY_CREATUREDM", "1");
                RappelzSettings.Update("GRAPHIC_TOWNLIMIT", "1");
                RappelzSettings.Update("GRAPHIC_RENDER_OTHERPLAYER", "1");
                RappelzSettings.Update("GRAPHIC_MIPBIAS", "1");
                RappelzSettings.Update("GRAPHIC_SPEEDQUAL", "1");
                RappelzSettings.Update("GRAPHIC_SELFSHADOWQUAL", "1");
                RappelzSettings.Update("GRAPHIC_WATERQUAL", "1");
                RappelzSettings.Update("PLAY_WEATHER_QUALITY", "1");
                
                setValues();
            }

            if (gfxPreset.Value == 4) //High
            {
                RappelzSettings.Update("GRAPHIC_PRESETOPTION", "4");
                RappelzSettings.Update("GRAPHIC_REFRESHRATE", "0");
                RappelzSettings.Update("GRAPHIC_BACKGROUNDDIS", "3");
                RappelzSettings.Update("GRAPHIC_TERRAINDIS", "3");
                RappelzSettings.Update("GRAPHIC_PROPDIS", "3");
                RappelzSettings.Update("GRAPHIC_SPEEDDIS", "3");
                RappelzSettings.Update("GRAPHIC_GRASSDIS", "3");
                RappelzSettings.Update("GRAPHIC_AVATARDIS", "3");
                RappelzSettings.Update("GRAPHIC_SHADOWDIS", "3");
                RappelzSettings.Update("GRAPHIC_GLOWQUAL", "0");
                RappelzSettings.Update("GRAPHIC_WATERQUAL", "1");
                RappelzSettings.Update("GRAPHIC_RENDERTREE", "1");
                RappelzSettings.Update("GRAPHIC_TREEALPHA", "1");
                RappelzSettings.Update("GRAPHIC_ENHANCE", "1");
                RappelzSettings.Update("GRAPHIC_RENDERGRASS", "1");
                RappelzSettings.Update("GRAPHIC_LIGHT", "1");
                RappelzSettings.Update("GRAPHIC_SHADOW", "1");
                RappelzSettings.Update("GRAPHIC_LOWSHADOW", "1");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT", "1");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT_CREATURE", "1");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT_PARTY", "1");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT_ENEMY", "0");
                RappelzSettings.Update("GRAPHIC_EFFECT", "1");
                RappelzSettings.Update("GRAPHIC_EFFECT_CREATURE", "1");
                RappelzSettings.Update("GRAPHIC_EFFECT_PARTY", "1");
                RappelzSettings.Update("GRAPHIC_EFFECT_ENEMY", "0");
                RappelzSettings.Update("PLAY_MOBFACE", "1");
                //RappelzSettings.Update("PLAY_SELECT_OUTLINE_SHOW", "0");
                RappelzSettings.Update("PLAY_MANTLE", "1");
                RappelzSettings.Update("PLAY_HELM", "1");
                RappelzSettings.Update("PLAY_AVATAR_DECO", "1");
                //RappelzSettings.Update("PLAY_TITLE_SHOW", "1");
                RappelzSettings.Update("PLAY_NAME", "1");
                RappelzSettings.Update("PLAY_PLAYERNAME", "1");
                RappelzSettings.Update("PLAY_CREATURENAME", "1");
                RappelzSettings.Update("PLAY_MOBNAME", "1");
                RappelzSettings.Update("PLAY_NPCNAME", "1");
                RappelzSettings.Update("PLAY_HPGAGE", "1");
                RappelzSettings.Update("PLAY_PLAYERGAGE", "1");
                RappelzSettings.Update("PLAY_MOBGAGE", "1");
                RappelzSettings.Update("PLAY_TARGETGAGE", "1");
                RappelzSettings.Update("PLAY_DAMAGE", "1");
                RappelzSettings.Update("PLAY_PLAYERDM", "1");
                RappelzSettings.Update("PLAY_CREATUREDM", "1");
                RappelzSettings.Update("GRAPHIC_TOWNLIMIT", "0");
                RappelzSettings.Update("GRAPHIC_RENDER_OTHERPLAYER", "1");
                RappelzSettings.Update("GRAPHIC_MIPBIAS", "2");
                RappelzSettings.Update("GRAPHIC_SPEEDQUAL", "2");
                RappelzSettings.Update("GRAPHIC_SELFSHADOWQUAL", "2");
                RappelzSettings.Update("GRAPHIC_WATERQUAL", "2");
                RappelzSettings.Update("PLAY_WEATHER_QUALITY", "2");

                setValues();
            }

            if (gfxPreset.Value == 5) //Maximum
            {
                RappelzSettings.Update("GRAPHIC_PRESETOPTION", "5");
                RappelzSettings.Update("GRAPHIC_REFRESHRATE", "0");
                RappelzSettings.Update("GRAPHIC_BACKGROUNDDIS", "4");
                RappelzSettings.Update("GRAPHIC_TERRAINDIS", "4");
                RappelzSettings.Update("GRAPHIC_PROPDIS", "4");
                RappelzSettings.Update("GRAPHIC_SPEEDDIS", "4");
                RappelzSettings.Update("GRAPHIC_GRASSDIS", "4");
                RappelzSettings.Update("GRAPHIC_AVATARDIS", "4");
                RappelzSettings.Update("GRAPHIC_SHADOWDIS", "4");
                RappelzSettings.Update("GRAPHIC_GLOWQUAL", "1");
                RappelzSettings.Update("GRAPHIC_WATERQUAL", "1");
                RappelzSettings.Update("GRAPHIC_RENDERTREE", "1");
                RappelzSettings.Update("GRAPHIC_TREEALPHA", "1");
                RappelzSettings.Update("GRAPHIC_ENHANCE", "1");
                RappelzSettings.Update("GRAPHIC_RENDERGRASS", "1");
                RappelzSettings.Update("GRAPHIC_LIGHT", "1");
                RappelzSettings.Update("GRAPHIC_SHADOW", "1");
                RappelzSettings.Update("GRAPHIC_LOWSHADOW", "1");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT", "1");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT_CREATURE", "1");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT_PARTY", "1");
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT_ENEMY", "1");
                RappelzSettings.Update("GRAPHIC_EFFECT", "1");
                RappelzSettings.Update("GRAPHIC_EFFECT_CREATURE", "1");
                RappelzSettings.Update("GRAPHIC_EFFECT_PARTY", "1");
                RappelzSettings.Update("GRAPHIC_EFFECT_ENEMY", "0");
                RappelzSettings.Update("PLAY_MOBFACE", "1");
                //RappelzSettings.Update("PLAY_SELECT_OUTLINE_SHOW", "1");
                RappelzSettings.Update("PLAY_MANTLE", "1");
                RappelzSettings.Update("PLAY_HELM", "1");
                RappelzSettings.Update("PLAY_AVATAR_DECO", "1");
                //RappelzSettings.Update("PLAY_TITLE_SHOW", "1");
                RappelzSettings.Update("PLAY_NAME", "1");
                RappelzSettings.Update("PLAY_PLAYERNAME", "1");
                RappelzSettings.Update("PLAY_CREATURENAME", "1");
                RappelzSettings.Update("PLAY_MOBNAME", "1");
                RappelzSettings.Update("PLAY_NPCNAME", "1");
                RappelzSettings.Update("PLAY_HPGAGE", "1");
                RappelzSettings.Update("PLAY_PLAYERGAGE", "1");
                RappelzSettings.Update("PLAY_MOBGAGE", "1");
                RappelzSettings.Update("PLAY_TARGETGAGE", "1");
                RappelzSettings.Update("PLAY_DAMAGE", "1");
                RappelzSettings.Update("PLAY_PLAYERDM", "1");
                RappelzSettings.Update("PLAY_CREATUREDM", "1");
                RappelzSettings.Update("GRAPHIC_TOWNLIMIT", "0");
                RappelzSettings.Update("GRAPHIC_RENDER_OTHERPLAYER", "1");
                RappelzSettings.Update("GRAPHIC_MIPBIAS", "3");
                RappelzSettings.Update("GRAPHIC_SPEEDQUAL", "3");
                RappelzSettings.Update("GRAPHIC_SELFSHADOWQUAL", "3");
                RappelzSettings.Update("GRAPHIC_WATERQUAL", "3");
                RappelzSettings.Update("PLAY_WEATHER_QUALITY", "3");

                setValues();
            }

            lockOptions();
        }

        void lockOptions()
        {
            if (gfxPreset.Value > 0)
            {
                resolutionWidth.Enabled = false;
                resolutionHeight.Enabled = false;
                refreshRate.Enabled = false;
                enviromentDistance.Enabled = false;
                terrainDistance.Enabled = false;
                propDistance.Enabled = false;
                speedDistance.Enabled = false;
                grassDistance.Enabled = false;
                characterDistance.Enabled = false;
                shadowDistance.Enabled = false;
                lowQualityShadows.Enabled = false;
                speedQuality.Enabled = false;
                textureQuality.Enabled = false;
                shadowSelfQuality.Enabled = false;
                weatherQuality.Enabled = false;
                bloom.Enabled = false;
                waterReflections.Enabled = false;
                minStructures.Enabled = false;
                betterTrees.Enabled = false;
                enchantmentEffects.Enabled = false;
                lastingEffects.Enabled = false;
                lastingEffects_cs.Enabled = false;
                lastingEffects_p.Enabled = false;
                lastingEffects_o.Enabled = false;
                shadows.Enabled = false;
                trees.Enabled = false;
                grass.Enabled = false;
                lightMap.Enabled = false;
                effect.Enabled = false;
                effect_cs.Enabled = false;
                effect_p.Enabled = false;
                effect_o.Enabled = false;
                showOthers.Enabled = false;
                chatBalloons.Enabled = false;
                monsterAvatar.Enabled = false;
                cloaks.Enabled = false;
                hpGauges.Enabled = false;
                playerHPGauge.Enabled = false;
                mobHPGauge.Enabled = false;
                targetHPGauge.Enabled = false;
                names.Enabled = false;
                playerNames.Enabled = false;
                creatureNames.Enabled = false;
                monsterNames.Enabled = false;
                npcNames.Enabled = false;
                damage.Enabled = false;
                playerDamage.Enabled = false;
                creatureDamage.Enabled = false;
                cameraCriticalShake.Enabled = false;
                helmets.Enabled = false;
                deco.Enabled = false;
                //titles.Enabled = false;
                //targetOutline.Enabled = false;

            }
            else
            {
                resolutionWidth.Enabled = true;
                resolutionHeight.Enabled = true;
                refreshRate.Enabled = true;
                enviromentDistance.Enabled = true;
                terrainDistance.Enabled = true;
                propDistance.Enabled = true;
                speedDistance.Enabled = true;
                grassDistance.Enabled = true;
                characterDistance.Enabled = true;
                shadowDistance.Enabled = true;
                lowQualityShadows.Enabled = true;
                speedQuality.Enabled = true;
                textureQuality.Enabled = true;
                shadowSelfQuality.Enabled = true;
                weatherQuality.Enabled = true;
                bloom.Enabled = true;
                waterReflections.Enabled = true;
                minStructures.Enabled = true;
                betterTrees.Enabled = true;
                enchantmentEffects.Enabled = true;
                lastingEffects.Enabled = true;
                lastingEffects_cs.Enabled = true;
                lastingEffects_p.Enabled = true;
                lastingEffects_o.Enabled = true;
                shadows.Enabled = true;
                trees.Enabled = true;
                grass.Enabled = true;
                lightMap.Enabled = true;
                effect.Enabled = true;
                effect_cs.Enabled = true;
                effect_p.Enabled = true;
                effect_o.Enabled = true;
                showOthers.Enabled = true;
                chatBalloons.Enabled = true;
                monsterAvatar.Enabled = true;
                cloaks.Enabled = true;
                hpGauges.Enabled = true;
                playerHPGauge.Enabled = true;
                mobHPGauge.Enabled = true;
                targetHPGauge.Enabled = true;
                names.Enabled = true;
                playerNames.Enabled = true;
                creatureNames.Enabled = true;
                monsterNames.Enabled = true;
                npcNames.Enabled = true;
                damage.Enabled = true;
                playerDamage.Enabled = true;
                creatureDamage.Enabled = true;
                cameraCriticalShake.Enabled = true;
                helmets.Enabled = true;
                deco.Enabled = true;
                //titles.Enabled = true;
                //targetOutline.Enabled = true;
            }
        }

        void useDesktopBrightness_CheckedChanged(object sender, EventArgs e)
        {
            if (useDesktopBrightness.Checked) { brightness.Enabled = false; }
            else { brightness.Enabled = true; }
            RappelzSettings.Update("GRAPHIC_WINBRIGHT", Convert.ToInt32(useDesktopBrightness.Checked).ToString());
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            RappelzSettings.SaveSettings(Settings);
            this.Close();
        }

        private void resolutionWidth_ValueChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_RESOLUTION_WIDTH", resolutionWidth.Value.ToString());
        }

        private void resolutionHeight_ValueChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_RESOLUTION_HEIGHT", resolutionHeight.Value.ToString());
        }

        private void refreshRate_TextChanged(object sender, EventArgs e)
        {
            // TODO: Actually implement me
            RappelzSettings.Update("GRAPHIC_REFRESHRATE", "0");
        }

        private void windowed_CheckStateChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_WINMODE", Convert.ToInt32(windowed.Checked).ToString());
        }

        private void brightness_ValueChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_BRIGHT", brightness.Value.ToString());
        }

        private void cameraCollisions_CheckStateChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("PLAY_CAMERA_COLLISION", Convert.ToInt32(cameraCollisions.Checked).ToString());
        }

        private void cameraCriticalShake_CheckStateChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("PLAY_CRITICAL_CAMERA", Convert.ToInt32(cameraCriticalShake.Checked).ToString());
        }

        private void enviromentDistance_ValueChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_BACKGROUNDDIS", enviromentDistance.Value.ToString());
        }

        private void terrainDistance_ValueChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_TERRAINDIS", terrainDistance.Value.ToString());
        }

        private void propDistance_ValueChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_PROPDIS", propDistance.Value.ToString());
        }

        private void speedDistance_ValueChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_SPEEDDIS", speedDistance.Value.ToString());
        }

        private void grassDistance_ValueChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_GRASSDIS", grassDistance.Value.ToString());
        }

        private void characterDistance_ValueChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_AVATARDIS", characterDistance.Value.ToString());
        }

        private void shadowDistance_ValueChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_SHADOWDIS", shadowDistance.Value.ToString());
        }

        private void lowQualityShadows_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_LOWSHADOW", Convert.ToInt32(lowQualityShadows.Checked).ToString());
            RappelzSettings.Update("GRAPHIC_SHADOW", "0");

            if (lowQualityShadows.Checked)
            {
                shadows.Checked = false;
                shadows.Enabled = false;
            }
            else
            {
                shadows.Enabled = true;
            }
        }

        private void speedQuality_ValueChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_SPEEDQUAL", speedQuality.Value.ToString());
        }

        private void textureQuality_ValueChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_MIPBIAS", textureQuality.Value.ToString());
        }

        private void bloom_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_GLOWQUAL", Convert.ToInt32(bloom.CheckState).ToString());
        }

        private void windowed_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_WINMODE", Convert.ToInt32(windowed.CheckState).ToString());
            RappelzSettings.Update("GRAPHIC_WINBRIGHT", Convert.ToInt32(useDesktopBrightness.CheckState).ToString());

            if (windowed.Checked)
            {
                useDesktopBrightness.Checked = true;
                useDesktopBrightness.Enabled = false;
            }
            else
            {
                useDesktopBrightness.Enabled = true;
            }
        }

        private void waterReflections_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_WATERQUAL", Convert.ToInt32(waterReflections.CheckState).ToString());
        }

        private void trees_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_RENDERTREE", Convert.ToInt32(trees.CheckState).ToString());
        }

        private void betterTrees_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_TREEALPHA", Convert.ToInt32(betterTrees.CheckState).ToString());
        }

        private void enchantmentEffects_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_ENHANCE", Convert.ToInt32(enchantmentEffects.CheckState).ToString());
        }

        private void grass_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_RENDERGRASS", Convert.ToInt32(grass.CheckState).ToString());
        }

        private void lightMap_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_LIGHTMAP", Convert.ToInt32(lightMap.CheckState).ToString());
        }

        private void shadows_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_SHADOW", Convert.ToInt32(shadows.CheckState).ToString());

            if (shadows.Checked)
            {
                RappelzSettings.Update("GRAPHIC_LOWSHADOW", "0");
                lowQualityShadows.Checked = false;
                lowQualityShadows.Enabled = false;
            }
            else { lowQualityShadows.Enabled = true; }
        }

        private void lastingEffects_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_STATE_EFFECT", Convert.ToInt32(lastingEffects.CheckState).ToString());

            if (!lastingEffects.Checked)
            {
                lastingEffects_cs.Checked = false;
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT_CREATURE", "0");
                lastingEffects_p.Checked = false;
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT_PARTY", "0");
                lastingEffects_o.Checked = false;
                RappelzSettings.Update("GRAPHIC_STATE_EFFECT_PARTY", "0");
            }
        }

        private void lastingEffects_cs_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_STATE_EFFECT_CREATURE", Convert.ToInt32(lastingEffects_cs.CheckState).ToString());
        }

        private void lastingEffects_p_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_STATE_EFFECT_PARTY", Convert.ToInt32(lastingEffects_p.CheckState).ToString());
        }

        private void lastingEffects_o_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_STATE_EFFECT_PARTY", Convert.ToInt32(lastingEffects_o.CheckState).ToString());
        }

        private void effect_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_EFFECT", Convert.ToInt32(effect.CheckState).ToString());

            if (!effect.Checked)
            {
                effect_cs.Checked = false;
                RappelzSettings.Update("GRAPHIC_EFFECT_CREATURE", "0");
                effect_p.Checked = false;
                RappelzSettings.Update("GRAPHIC_EFFECT_PARTY", "0");
                effect_o.Checked = false;
                RappelzSettings.Update("GRAPHIC_EFFECT_ENEMY", "0");
            }
        }

        private void effect_cs_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_EFFECT_CREATURE", Convert.ToInt32(effect_cs.CheckState).ToString());
        }

        private void effect_p_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_EFFECT_PARTY", Convert.ToInt32(effect_p.CheckState).ToString());
        }

        private void effect_o_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_EFFECT_ENEMY", Convert.ToInt32(effect_o.CheckState).ToString());
        }

        private void cloaks_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("PLAY_MANTLE", Convert.ToInt32(helmets.CheckState).ToString());
        }

        private void cameraCollisions_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("PLAY_CAMERA_COLLISION", Convert.ToInt32(cameraCollisions.CheckState).ToString());
        }

        private void shadowSelfQuality_Scroll(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_SELFSHADOWQUAL", shadowSelfQuality.Value.ToString());
        }

        private void weatherQuality_Scroll(object sender, EventArgs e)
        {
            RappelzSettings.Update("PLAY_WEATHER_QUALITY", weatherQuality.Value.ToString());
        }

        private void helmets_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("PLAY_HELM", Convert.ToInt32(helmets.CheckState).ToString());
        }

        private void deco_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("PLAY_AVATAR_DECO", Convert.ToInt32(deco.CheckState).ToString());
        }

        private void chatBalloons_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("PLAY_CHATBALLOON", Convert.ToInt32(chatBalloons.CheckState).ToString());
        }

        private void monsterAvatar_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("PLAY_MOBFACE", Convert.ToInt32(monsterAvatar.CheckState).ToString());
        }

        private void names_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("PLAY_NAME", Convert.ToInt32(names.CheckState).ToString());
        }

        private void playerNames_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("PLAY_PLAYERNAME", Convert.ToInt32(playerNames.CheckState).ToString());
        }

        private void creatureNames_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("PLAY_CREATURENAME", Convert.ToInt32(creatureNames.CheckState).ToString());
        }

        private void monsterNames_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("PLAY_MOBNAME", Convert.ToInt32(monsterNames.CheckState).ToString());
        }

        private void npcNames_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("PLAY_NPCNAME", Convert.ToInt32(npcNames.CheckState).ToString());
        }

        private void hpGauges_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void damage_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("PLAY_DAMAGE", Convert.ToInt32(damage.CheckState).ToString());
        }

        private void playerDamage_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("PLAY_PLAYERDM", Convert.ToInt32(playerDamage.CheckState).ToString());
        }

        private void creatureDamage_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("PLAY_CREATUREDM", Convert.ToInt32(creatureDamage.CheckState).ToString());
        }

        private void minStructures_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_TOWNLIMIT", Convert.ToInt32(minStructures.CheckState).ToString());
        }

        private void showOthers_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("GRAPHIC_RENDER_OTHERPLAYER", Convert.ToInt32(showOthers.CheckState).ToString());
        }

        private void enterToChat_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("PLAY_ENTERCHAT", Convert.ToInt32(enterToChat.CheckState).ToString());
        }
    }
}
