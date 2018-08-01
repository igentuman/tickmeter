using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tickMeter.Forms
{
    
    public partial class TickrateStatistics : Form
    {


        public TickrateStatistics()
        {
            InitializeComponent();
            
        }

        private void TickrateStatistics_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
