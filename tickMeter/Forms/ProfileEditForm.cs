using System;
using System.Diagnostics;
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
            App.profilesForm.LoadProfiles();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://bitbucket.org/dvman8bit/tickmeter/wiki/Filter");

        }
    }
}
