using System;
using System.Windows.Forms;
using tickMeter.Classes;

namespace tickMeter.Forms
{
   
    public partial class ProfilesForm : Form
    {

        public ProfilesForm()
        {
            InitializeComponent(); 
        }

        public void LoadProfiles()
        {
            GameProfileManager.LoadProfiles();
            custom_profiles.Items.Clear();
            foreach (GameProfile gProf in GameProfileManager.gameProfs)
            {
                custom_profiles.Items.Add(gProf.GameName, gProf.isEnabled);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            App.profileEditForm.Show();
        }

        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            App.packetStatsForm.Show();
        }

        public int LastClickedItem = -1;
        private void custom_profiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (LastClickedItem == -1) return;
            GameProfileManager.ProfileToEditForm(LastClickedItem);
            Hide();
            App.profileEditForm.Show();
            
        }
        
        private void DblClickTick(object sender, EventArgs e)
        {
            LastClickedItem = -1;
        }

        private void custom_profiles_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            LastClickedItem = e.Index;
            System.Timers.Timer DblClick = new System.Timers.Timer()
            {
                Interval = 200,
                Enabled = true,
            };
            DblClick.AutoReset = false;
            DblClick.Elapsed += DblClickTick;
        }

        private void profileForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void profileForm_Shown(object sender, EventArgs e)
        {
            LoadProfiles();
        }
    }
}
