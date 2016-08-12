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
    public partial class RappelzSettingsGUI : Form
    {
        public bool Save = false;

        public ClientSettings[] Settings;

        public RappelzSettingsGUI(ClientSettings[] inSettings)
        {
            InitializeComponent();

            Settings = inSettings;

            setValues();
        }

        void setValues()
        {
            for (int i = 1; i < 81; i++)
            {
                string currentName = Settings[i].Name;
                int currentValue = Convert.ToInt32(Settings[i].Value);

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
                if (currentName == "GRAPHIC_RENDER_OTHERPLAYER") { showOthers.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_CHATBALLOON") { chatBalloons.Checked = Convert.ToBoolean(currentValue); }
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
                if (currentName == "PLAYER_DAMAGE") { damage.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_PLAYERDM") { playerDamage.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_CREATUREDM") { creatureDamage.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_WEATHER_QUALITY") { weatherQuality.Value = currentValue; }
                if (currentName == "PLAY_CRITICAL_CAMERA") { cameraCriticalShake.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_HELM") { helmets.Checked = Convert.ToBoolean(currentValue); }
                if (currentName == "PLAY_AVATAR_DECO") { deco.Checked = Convert.ToBoolean(currentValue); }
                //if (currentName == "PLAY_TITLE_SHOW") { titles.Checked = Convert.ToBoolean(currentValue); }
                //if (currentName == "PLAY_SELECT_OUTLINE_SHOW") { targetOutline.Checked = Convert.ToBoolean(currentValue); }
            }
        }

        void updateSetting(string name, object value)
        {
            ClientSettings setting = Settings.First(s => s.Name == name);
            if (setting != null) { setting.Value = value; Save = true; }
            else { MessageBox.Show("Setting is null", "Settings Exception #1", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        void volumeMixerlb_Click(object sender, EventArgs e)
        {
            //VolumeGUI volumeMixer = new VolumeGUI(ref settings);
            //volumeMixer.StartPosition = FormStartPosition.CenterScreen;
            //volumeMixer.ShowDialog();
        }

        void gfxPreset_ValueChanged(object sender, EventArgs e)
        {
            if (gfxPreset.Value == 0)
            {
                updateSetting("GRAPHIC_PRESETOPTION", 0);
                setValues();
            }

            if (gfxPreset.Value == 1) // Minimum
            {
                updateSetting("GRAPHIC_PRESETOPTION", 1);
                updateSetting("GRAPHIC_REFRESHRATE", 0);
                updateSetting("GRAPHIC_BACKGROUNDDIS", 0);
                updateSetting("GRAPHIC_TERRAINDIS", 0);
                updateSetting("GRAPHIC_PROPDIS", 0);
                updateSetting("GRAPHIC_SPEEDDIS", 0);
                updateSetting("GRAPHIC_GRASSDIS", 0);
                updateSetting("GRAPHIC_AVATARDIS", 0);
                updateSetting("GRAPHIC_SHADOWDIS", 0);
                updateSetting("GRAPHIC_GLOWQUAL", 0);
                updateSetting("GRAPHIC_WATERQUAL", 0);
                updateSetting("GRAPHIC_RENDERTREE", 0);
                updateSetting("GRAPHIC_TREEALPHA", 0);
                updateSetting("GRAPHIC_ENHANCE", 0);
                updateSetting("GRAPHIC_RENDERGRASS", 0);
                updateSetting("GRAPHIC_LIGHT", 0);
                updateSetting("GRAPHIC_SHADOW", 0);
                updateSetting("GRAPHIC_LOWSHADOW", 0);
                updateSetting("GRAPHIC_STATE_EFFECT", 0);
                updateSetting("GRAPHIC_STATE_EFFECT_CREATURE", 0);
                updateSetting("GRAPHIC_STATE_EFFECT_PARTY", 0);
                updateSetting("GRAPHIC_STATE_EFFECT_ENEMY", 0);
                updateSetting("GRAPHIC_EFFECT", 0);
                updateSetting("GRAPHIC_EFFECT_CREATURE", 0);
                updateSetting("GRAPHIC_EFFECT_PARTY", 0);
                updateSetting("GRAPHIC_EFFECT_ENEMY", 0);
                //updateSetting("PLAY_SELECT_OUTLINE_SHOW", 0);
                updateSetting("PLAY_MANTLE", 0);
                updateSetting("PLAY_HELM", 0);
                updateSetting("PLAY_AVATAR_DECO", 0);
                //updateSetting("PLAY_TITLE_SHOW", 1);
                updateSetting("PLAY_NAME", 1);
                updateSetting("PLAY_PLAYERNAME", 1);
                updateSetting("PLAY_CREATURENAME", 1);
                updateSetting("PLAY_MOBNAME", 1);
                updateSetting("PLAY_NPCNAME", 1);
                updateSetting("PLAY_HPGAGE", 1);
                updateSetting("PLAY_PLAYERGAGE", 1);
                updateSetting("PLAY_MOBGAGE", 1);
                updateSetting("PLAY_TARGETGAGE", 1);
                updateSetting("PLAY_DAMAGE", 1);
                updateSetting("PLAY_PLAYERDM", 1);
                updateSetting("PLAY_CREATUREDM", 1);
                updateSetting("GRAPHIC_TOWNLIMIT", 1);
                updateSetting("GRAPHIC_RENDER_OTHERPLAYER", 0);
                updateSetting("GRAPHIC_MIPBIAS", 0);
                updateSetting("GRAPHIC_SPEEDQUAL", 0);
                updateSetting("GRAPHIC_SELFSHADOWQUAL", 0);
                updateSetting("GRAPHIC_WATERQUAL", 0);
                updateSetting("PLAY_WEATHER_QUALITY", 0);

                setValues();
            }

            if (gfxPreset.Value == 2) // Low
            {
                updateSetting("GRAPHIC_PRESETOPTION", 2);
                updateSetting("GRAPHIC_REFRESHRATE", 0);
                updateSetting("GRAPHIC_BACKGROUNDDIS", 1);
                updateSetting("GRAPHIC_TERRAINDIS", 1);
                updateSetting("GRAPHIC_PROPDIS", 1);
                updateSetting("GRAPHIC_SPEEDDIS", 1);
                updateSetting("GRAPHIC_GRASSDIS", 1);
                updateSetting("GRAPHIC_AVATARDIS", 1);
                updateSetting("GRAPHIC_SHADOWDIS", 1);
                updateSetting("GRAPHIC_GLOWQUAL", 0);
                updateSetting("GRAPHIC_WATERQUAL", 0);
                updateSetting("GRAPHIC_RENDERTREE", 1);
                updateSetting("GRAPHIC_TREEALPHA", 0);
                updateSetting("GRAPHIC_ENHANCE", 1);
                updateSetting("GRAPHIC_RENDERGRASS", 1);
                updateSetting("GRAPHIC_LIGHT", 0);
                updateSetting("GRAPHIC_SHADOW", 0);
                updateSetting("GRAPHIC_LOWSHADOW", 0);
                updateSetting("GRAPHIC_STATE_EFFECT", 1);
                updateSetting("GRAPHIC_STATE_EFFECT_CREATURE", 1);
                updateSetting("GRAPHIC_STATE_EFFECT_PARTY", 0);
                updateSetting("GRAPHIC_STATE_EFFECT_ENEMY", 0);
                updateSetting("GRAPHIC_EFFECT", 1);
                updateSetting("GRAPHIC_EFFECT_CREATURE", 1);
                updateSetting("GRAPHIC_EFFECT_PARTY", 0);
                updateSetting("GRAPHIC_EFFECT_ENEMY", 0);
                //updateSetting("PLAY_SELECT_OUTLINE_SHOW", 0);
                updateSetting("PLAY_MANTLE", 0);
                updateSetting("PLAY_HELM", 0);
                updateSetting("PLAY_AVATAR_DECO", 0);
                //updateSetting("PLAY_TITLE_SHOW", 1);
                updateSetting("PLAY_NAME", 1);
                updateSetting("PLAY_PLAYERNAME", 1);
                updateSetting("PLAY_CREATURENAME", 1);
                updateSetting("PLAY_MOBNAME", 1);
                updateSetting("PLAY_NPCNAME", 1);
                updateSetting("PLAY_HPGAGE", 1);
                updateSetting("PLAY_PLAYERGAGE", 1);
                updateSetting("PLAY_MOBGAGE", 1);
                updateSetting("PLAY_TARGETGAGE", 1);
                updateSetting("PLAY_DAMAGE", 1);
                updateSetting("PLAY_PLAYERDM", 1);
                updateSetting("PLAY_CREATUREDM", 1);
                updateSetting("GRAPHIC_TOWNLIMIT", 1);
                updateSetting("GRAPHIC_RENDER_OTHERPLAYER", 0);
                updateSetting("GRAPHIC_MIPBIAS", 1);
                updateSetting("GRAPHIC_SPEEDQUAL", 1);
                updateSetting("GRAPHIC_SELFSHADOWQUAL", 1);
                updateSetting("GRAPHIC_WATERQUAL", 1);
                updateSetting("PLAY_WEATHER_QUALITY", 1);

                setValues();
            }

            if (gfxPreset.Value == 3) //Medium
            {
                updateSetting("GRAPHIC_PRESETOPTION", 3);
                updateSetting("GRAPHIC_REFRESHRATE", 0);
                updateSetting("GRAPHIC_BACKGROUNDDIS", 2);
                updateSetting("GRAPHIC_TERRAINDIS", 2);
                updateSetting("GRAPHIC_PROPDIS", 2);
                updateSetting("GRAPHIC_SPEEDDIS", 2);
                updateSetting("GRAPHIC_GRASSDIS", 2);
                updateSetting("GRAPHIC_AVATARDIS", 2);
                updateSetting("GRAPHIC_SHADOWDIS", 2);
                updateSetting("GRAPHIC_GLOWQUAL", 0);
                updateSetting("GRAPHIC_WATERQUAL", 1);
                updateSetting("GRAPHIC_RENDERTREE", 1);
                updateSetting("GRAPHIC_TREEALPHA", 0);
                updateSetting("GRAPHIC_ENHANCE", 1);
                updateSetting("GRAPHIC_RENDERGRASS", 1);
                updateSetting("GRAPHIC_LIGHT", 0);
                updateSetting("GRAPHIC_SHADOW", 1);
                updateSetting("GRAPHIC_LOWSHADOW", 1);
                updateSetting("GRAPHIC_STATE_EFFECT", 1);
                updateSetting("GRAPHIC_STATE_EFFECT_CREATURE", 1);
                updateSetting("GRAPHIC_STATE_EFFECT_PARTY", 1);
                updateSetting("GRAPHIC_STATE_EFFECT_ENEMY", 0);
                updateSetting("GRAPHIC_EFFECT", 1);
                updateSetting("GRAPHIC_EFFECT_CREATURE", 1);
                updateSetting("GRAPHIC_EFFECT_PARTY", 1);
                updateSetting("GRAPHIC_EFFECT_ENEMY", 0);
                //updateSetting("PLAY_SELECT_OUTLINE_SHOW", 0);
                updateSetting("PLAY_MANTLE", 1);
                updateSetting("PLAY_HELM", 1);
                updateSetting("PLAY_AVATAR_DECO", 1);
                //updateSetting("PLAY_TITLE_SHOW", 1);
                updateSetting("PLAY_NAME", 1);
                updateSetting("PLAY_PLAYERNAME", 1);
                updateSetting("PLAY_CREATURENAME", 1);
                updateSetting("PLAY_MOBNAME", 1);
                updateSetting("PLAY_NPCNAME", 1);
                updateSetting("PLAY_HPGAGE", 1);
                updateSetting("PLAY_PLAYERGAGE", 1);
                updateSetting("PLAY_MOBGAGE", 1);
                updateSetting("PLAY_TARGETGAGE", 1);
                updateSetting("PLAY_DAMAGE", 1);
                updateSetting("PLAY_PLAYERDM", 1);
                updateSetting("PLAY_CREATUREDM", 1);
                updateSetting("GRAPHIC_TOWNLIMIT", 1);
                updateSetting("GRAPHIC_RENDER_OTHERPLAYER", 1);
                updateSetting("GRAPHIC_MIPBIAS", 1);
                updateSetting("GRAPHIC_SPEEDQUAL", 1);
                updateSetting("GRAPHIC_SELFSHADOWQUAL", 1);
                updateSetting("GRAPHIC_WATERQUAL", 1);
                updateSetting("PLAY_WEATHER_QUALITY", 1);
                
                setValues();
            }

            if (gfxPreset.Value == 4) //High
            {
                updateSetting("GRAPHIC_PRESETOPTION", 4);
                updateSetting("GRAPHIC_REFRESHRATE", 0);
                updateSetting("GRAPHIC_BACKGROUNDDIS", 3);
                updateSetting("GRAPHIC_TERRAINDIS", 3);
                updateSetting("GRAPHIC_PROPDIS", 3);
                updateSetting("GRAPHIC_SPEEDDIS", 3);
                updateSetting("GRAPHIC_GRASSDIS", 3);
                updateSetting("GRAPHIC_AVATARDIS", 3);
                updateSetting("GRAPHIC_SHADOWDIS", 3);
                updateSetting("GRAPHIC_GLOWQUAL", 0);
                updateSetting("GRAPHIC_WATERQUAL", 1);
                updateSetting("GRAPHIC_RENDERTREE", 1);
                updateSetting("GRAPHIC_TREEALPHA", 1);
                updateSetting("GRAPHIC_ENHANCE", 1);
                updateSetting("GRAPHIC_RENDERGRASS", 1);
                updateSetting("GRAPHIC_LIGHT", 1);
                updateSetting("GRAPHIC_SHADOW", 1);
                updateSetting("GRAPHIC_LOWSHADOW", 1);
                updateSetting("GRAPHIC_STATE_EFFECT", 1);
                updateSetting("GRAPHIC_STATE_EFFECT_CREATURE", 1);
                updateSetting("GRAPHIC_STATE_EFFECT_PARTY", 1);
                updateSetting("GRAPHIC_STATE_EFFECT_ENEMY", 0);
                updateSetting("GRAPHIC_EFFECT", 1);
                updateSetting("GRAPHIC_EFFECT_CREATURE", 1);
                updateSetting("GRAPHIC_EFFECT_PARTY", 1);
                updateSetting("GRAPHIC_EFFECT_ENEMY", 0);
                //updateSetting("PLAY_SELECT_OUTLINE_SHOW", 0);
                updateSetting("PLAY_MANTLE", 1);
                updateSetting("PLAY_HELM", 1);
                updateSetting("PLAY_AVATAR_DECO", 1);
                //updateSetting("PLAY_TITLE_SHOW", 1);
                updateSetting("PLAY_NAME", 1);
                updateSetting("PLAY_PLAYERNAME", 1);
                updateSetting("PLAY_CREATURENAME", 1);
                updateSetting("PLAY_MOBNAME", 1);
                updateSetting("PLAY_NPCNAME", 1);
                updateSetting("PLAY_HPGAGE", 1);
                updateSetting("PLAY_PLAYERGAGE", 1);
                updateSetting("PLAY_MOBGAGE", 1);
                updateSetting("PLAY_TARGETGAGE", 1);
                updateSetting("PLAY_DAMAGE", 1);
                updateSetting("PLAY_PLAYERDM", 1);
                updateSetting("PLAY_CREATUREDM", 1);
                updateSetting("GRAPHIC_TOWNLIMIT", 0);
                updateSetting("GRAPHIC_RENDER_OTHERPLAYER", 1);
                updateSetting("GRAPHIC_MIPBIAS", 2);
                updateSetting("GRAPHIC_SPEEDQUAL", 2);
                updateSetting("GRAPHIC_SELFSHADOWQUAL", 2);
                updateSetting("GRAPHIC_WATERQUAL", 2);
                updateSetting("PLAY_WEATHER_QUALITY", 2);

                setValues();
            }

            if (gfxPreset.Value == 5) //Maximum
            {
                updateSetting("GRAPHIC_PRESETOPTION", 5);
                updateSetting("GRAPHIC_REFRESHRATE", 0);
                updateSetting("GRAPHIC_BACKGROUNDDIS", 4);
                updateSetting("GRAPHIC_TERRAINDIS", 4);
                updateSetting("GRAPHIC_PROPDIS", 4);
                updateSetting("GRAPHIC_SPEEDDIS", 4);
                updateSetting("GRAPHIC_GRASSDIS", 4);
                updateSetting("GRAPHIC_AVATARDIS", 4);
                updateSetting("GRAPHIC_SHADOWDIS", 4);
                updateSetting("GRAPHIC_GLOWQUAL", 1);
                updateSetting("GRAPHIC_WATERQUAL", 1);
                updateSetting("GRAPHIC_RENDERTREE", 1);
                updateSetting("GRAPHIC_TREEALPHA", 1);
                updateSetting("GRAPHIC_ENHANCE", 1);
                updateSetting("GRAPHIC_RENDERGRASS", 1);
                updateSetting("GRAPHIC_LIGHT", 1);
                updateSetting("GRAPHIC_SHADOW", 1);
                updateSetting("GRAPHIC_LOWSHADOW", 1);
                updateSetting("GRAPHIC_STATE_EFFECT", 1);
                updateSetting("GRAPHIC_STATE_EFFECT_CREATURE", 1);
                updateSetting("GRAPHIC_STATE_EFFECT_PARTY", 1);
                updateSetting("GRAPHIC_STATE_EFFECT_ENEMY", 1);
                updateSetting("GRAPHIC_EFFECT", 1);
                updateSetting("GRAPHIC_EFFECT_CREATURE", 1);
                updateSetting("GRAPHIC_EFFECT_PARTY", 1);
                updateSetting("GRAPHIC_EFFECT_ENEMY", 0);
                //updateSetting("PLAY_SELECT_OUTLINE_SHOW", 1);
                updateSetting("PLAY_MANTLE", 1);
                updateSetting("PLAY_HELM", 1);
                updateSetting("PLAY_AVATAR_DECO", 1);
                //updateSetting("PLAY_TITLE_SHOW", 1);
                updateSetting("PLAY_NAME", 1);
                updateSetting("PLAY_PLAYERNAME", 1);
                updateSetting("PLAY_CREATURENAME", 1);
                updateSetting("PLAY_MOBNAME", 1);
                updateSetting("PLAY_NPCNAME", 1);
                updateSetting("PLAY_HPGAGE", 1);
                updateSetting("PLAY_PLAYERGAGE", 1);
                updateSetting("PLAY_MOBGAGE", 1);
                updateSetting("PLAY_TARGETGAGE", 1);
                updateSetting("PLAY_DAMAGE", 1);
                updateSetting("PLAY_PLAYERDM", 1);
                updateSetting("PLAY_CREATUREDM", 1);
                updateSetting("GRAPHIC_TOWNLIMIT", 0);
                updateSetting("GRAPHIC_RENDER_OTHERPLAYER", 1);
                updateSetting("GRAPHIC_MIPBIAS", 3);
                updateSetting("GRAPHIC_SPEEDQUAL", 3);
                updateSetting("GRAPHIC_SELFSHADOWQUAL", 3);
                updateSetting("GRAPHIC_WATERQUAL", 3);
                updateSetting("PLAY_WEATHER_QUALITY", 3);

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
            else { brightness.Enabled = false; }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
