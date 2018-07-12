using System;
using System.Diagnostics;
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
            for (int i = 0; i < built_in_profiles.Items.Count; i++)
            {
                App.settingsManager.SetOption(built_in_profiles.Items[i].ToString().Replace(" ", "_").ToUpper(), built_in_profiles.GetItemChecked(i).ToString());
            }
            App.settingsManager.SaveConfig();
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
            
            App.profileEditForm.Show();
            
        }
        
        private void DblClickTick(object sender, EventArgs e)
        {
            LastClickedItem = -1;
        }

        private void custom_profiles_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            LastClickedItem = e.Index;
            GameProfileManager.SwitchState(e.Index, !custom_profiles.GetItemChecked(e.Index));
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
            for(int i = 0; i < built_in_profiles.Items.Count; i++)
            {
                built_in_profiles.SetItemChecked(i, App.settingsManager.GetOption(built_in_profiles.Items[i].ToString().Replace(" ", "_").ToUpper()) == "True");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Двойной клик, что бы отредактировать профиль. Dell - для удаления.");
        }

        private void custom_profiles_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode.ToString() == "Delete")
            {
                GameProfileManager.RemoveProfile(custom_profiles.SelectedIndex);
                custom_profiles.Items.RemoveAt(custom_profiles.SelectedIndex);

            }
        }
    }
}
