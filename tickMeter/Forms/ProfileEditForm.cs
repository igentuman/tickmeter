using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tickMeter.Classes;

namespace tickMeter.Forms
{
    public partial class ProfileEditForm : Form
    {

        public ProfileEditForm()
        {
            InitializeComponent();
        }

        private void ProfileEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            Hide();
            GameProfileManager.FormToProfile();
        }

        
    }
}
