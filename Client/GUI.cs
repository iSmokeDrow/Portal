using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class GUI : Form
    {
        internal readonly Network network;

        public GUI()
        {
            InitializeComponent();
            network = new Network("127.0.0.1", 13545);
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            network.WriteCredentials("itoki", "shadows1", "5D61-1131-B2D5-3619-EF76-0885-57D3-1972");
        }
    }
}
