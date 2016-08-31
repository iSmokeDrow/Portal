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
using LBSoft.IndustrialCtrls.Knobs;

namespace Client
{
    public partial class RappelzVolumeGUI : Form
    {
        List<ClientSettings> settings = null;

        public RappelzVolumeGUI(List<ClientSettings> inSettings)
        {
            InitializeComponent();

            settings = inSettings;

            setValues();
        }

        private void setValues()
        {
            List<ClientSettings> soundSettings = settings.FindAll(s => s.Name.StartsWith("SOUND_"));
            foreach (ClientSettings setting in soundSettings)
            {
                int value = Convert.ToInt32(setting.Value);

                if (setting.Name == "SOUND_ALL_MUTE") { overallMute.Checked = Convert.ToBoolean(value); }
                else if (setting.Name == "SOUND_REPEAT") { musicRepeat.Checked = Convert.ToBoolean(value); }
                else if (setting.Name == "SOUND_BGM_MUTE") { musicMute.Checked = Convert.ToBoolean(value); }
                else if (setting.Name == "SOUND_SFX_MUTE") { sfxMute.Checked = Convert.ToBoolean(value); }
                else if (setting.Name == "SOUND_ENVS_MUTE") { environmentalMute.Checked = Convert.ToBoolean(value); }
                else if (setting.Name == "SOUND_BGM_VOLUME") { musicVolume.Value = Convert.ToInt32(value); }
                else if (setting.Name == "SOUND_SFX_VOLUME") { sfxVolume.Value = Convert.ToInt32(value); }
                else if (setting.Name == "SOUND_ENVS_VOLUME") { environmentalVolume.Value = Convert.ToInt32(value); }
                else if (setting.Name == "SOUND_MASTER_VOLUME") { overallVolume.Value = Convert.ToInt32(value); }
            }

            disableLobbyTheme.Checked = Convert.ToBoolean(Convert.ToInt32(settings.Find(s => s.Name == "LOBBY_THEME").Value));
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void overallVolume_KnobChangeValue(object sender, LBKnobEventArgs e)
        {
            RappelzSettings.Update("SOUND_MASTER_VOLUME", Convert.ToInt32(overallVolume.Value).ToString());
        }

        private void sfxVolume_KnobChangeValue(object sender, LBKnobEventArgs e)
        {
            RappelzSettings.Update("SOUND_SFX_VOLUME", Convert.ToInt32(sfxVolume.Value).ToString());
        }

        private void musicVolume_KnobChangeValue(object sender, LBKnobEventArgs e)
        {
            RappelzSettings.Update("SOUND_BGM_VOLUME", Convert.ToInt32(musicVolume.Value).ToString());
        }

        private void environmentalVolume_KnobChangeValue(object sender, LBKnobEventArgs e)
        {
            RappelzSettings.Update("SOUND_ENVS_VOLUME", Convert.ToInt32(environmentalVolume.Value).ToString());
        }

        private void overallMute_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("SOUND_ALL_MUTE", Convert.ToInt32(overallMute.CheckState).ToString());

            if (overallMute.Checked)
            {
                overallVolume.Value = 0;
                RappelzSettings.Update("SOUND_MASTER_VOLUME", "0");
                sfxVolume.Value = 0;
                RappelzSettings.Update("SOUND_SFX_VOLUME", "0");
                musicVolume.Value = 0;
                RappelzSettings.Update("SOUND_BGM_VOLUME", "0");
                environmentalVolume.Value = 0;
                RappelzSettings.Update("SOUND_ENVS_VOLUME", "0");
            }
        }

        private void sfxMute_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("SOUND_SFX_MUTE", Convert.ToInt32(sfxMute.CheckState).ToString());
            sfxVolume.Value = 0;
            RappelzSettings.Update("SOUND_SFX_VOLUME", "0");
        }

        private void musicMute_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("SOUND_BGM_MUTE", Convert.ToInt32(musicMute.CheckState).ToString());
            musicVolume.Value = 0;
            RappelzSettings.Update("SOUND_BGM_VOLUME", "0");
        }

        private void environmentalMute_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("SOUND_ENVS_MUTE", Convert.ToInt32(environmentalMute.CheckState).ToString());
            environmentalVolume.Value = 0;
            RappelzSettings.Update("SOUND_ENVS_VOLUME", "0");
        }

        private void musicRepeat_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("SOUND_REPEAT", Convert.ToInt32(musicRepeat.CheckState).ToString());
        }

        private void disableLobbyTheme_CheckedChanged(object sender, EventArgs e)
        {
            RappelzSettings.Update("LOBBY_THEME", Convert.ToInt32(disableLobbyTheme.CheckState).ToString());
        }
    }
}
