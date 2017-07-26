using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Server.Functions;
using Server.Structures;

// TODO: Make saving Settings Threaded
namespace Server
{
    public partial class settingsGUI : Form
    {
        bool save = false;

        object properties = new GridProperties();

        public settingsGUI()
        {
            InitializeComponent();
        }

        public void Initialize() { propertyGrid.SelectedObject = properties; }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) { save = true; }

        private void settingsGUI_FormClosing(object sender, FormClosingEventArgs e) { if (save) { OPT.SaveSettings(); } }
    }
}
