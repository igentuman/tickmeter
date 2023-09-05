using IniParser;
using IniParser.Model;
using PcapDotNet.Packets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace tickMeter.Classes
{
    public static class GameProfileManager
    {
        public static List<GameProfile> gameProfs = new List<GameProfile>();
        static FileIniDataParser parser;
        static IniData data;
        public static DbdStatsManager DbdMngr = new DbdStatsManager();
        public static PubgStatsManager PubgMngr = new PubgStatsManager();

        public static void CallBuitInProfiles(Packet packet)
        {
            DbdMngr.ProcessPacket(packet);
            PubgMngr.ProcessPacket(packet);
        }

        public static void CallCustomProfiles(Packet packet)
        {
            foreach(GameProfile gProf in gameProfs)
            {
                gProf.ProcessPacket(packet);
            }
        }

        static GameProfileManager()
        {
            if(gameProfs.Count == 0)
            {
                LoadProfiles();
            }
        }

        public static void SwitchState(int profileIndex, bool state)
        {
            Debug.Print(state.ToString());
            try
            {
                gameProfs[profileIndex].isEnabled = state;
                gameProfs[profileIndex].Save();
            }
            catch (Exception) { }
        }

        public static void LoadProfiles()
        {
            gameProfs.Clear();
            if(! Directory.Exists("profiles"))
            {
                Directory.CreateDirectory("profiles");
            }
            string[] files = Directory.GetFiles("profiles", "*.ini");
            foreach (string pFile in files)
            {
                GameProfile profile = GameProfile.InitFile(pFile);
                gameProfs.Add(profile);
            }
        }

        public static void ProfileToEditForm(int profileId)
        {
            PacketFilter pcktF = gameProfs[profileId].profileFilter;
            try
            {
                App.profileEditForm.from_ip_filter.Text = pcktF.SourceIpFilter;
                App.profileEditForm.from_port_filter.Text = pcktF.SourcePortFilter;
                App.profileEditForm.to_ip_filter.Text = pcktF.DestIpFilter;
                App.profileEditForm.to_port_filter.Text = pcktF.DestPortFilter;
                App.profileEditForm.packet_size_filter.Text = pcktF.PacketSizeFilter;
                if (pcktF.ProtocolFilter != "")
                    App.profileEditForm.protocol_filter.SelectedItem = pcktF.ProtocolFilter;
                App.profileEditForm.is_active.Checked = gameProfs[profileId].isEnabled;
                App.profileEditForm.require_process.Text = gameProfs[profileId].RequireProcess;
                App.profileEditForm.process_filter.Text = pcktF.ProcessFilter;
                App.profileEditForm.profile_name.Text = gameProfs[profileId].GameName;
            }
            catch (Exception err) { Debug.Print(err.Message); }
        }

        public static void FormToProfile()
        {
            parser = new FileIniDataParser();
            if (!Directory.Exists("profiles"))
            {
                Directory.CreateDirectory("profiles");
            }
            string ProfileName = App.profileEditForm.profile_name.Text;

            try
            {
                File.WriteAllText("profiles/" + ProfileName + ".ini", "[PROFILE_CONFIG]" + Environment.NewLine);
            }
            catch (Exception) { MessageBox.Show(string.Format("Не могу сохранить профиль: {0}.ini", ProfileName)); }

            data = parser.ReadFile("profiles/" + ProfileName + ".ini");
            SetOption("from_port_filter", App.profileEditForm.from_port_filter.Text);
            SetOption("to_port_filter", App.profileEditForm.to_port_filter.Text);
            SetOption("from_ip_filter", App.profileEditForm.from_ip_filter.Text);
            SetOption("to_ip_filter", App.profileEditForm.to_ip_filter.Text);
            SetOption("packet_size_filter", App.profileEditForm.packet_size_filter.Text);
            SetOption("process_filter", App.profileEditForm.process_filter.Text);
            SetOption("protocol_filter", App.profileEditForm.protocol_filter.Items[App.profileEditForm.protocol_filter.SelectedIndex].ToString());
            SetOption("require_process", App.profileEditForm.require_process.Text);
            SetOption("is_active", App.profileEditForm.is_active.Checked.ToString());
            SetOption("profile_name", App.profileEditForm.profile_name.Text.ToUpper());

            SaveConfig(ProfileName);
            MessageBox.Show(string.Format("Профиль успешно сохранен: {0}.ini", ProfileName));
        }

        public static void RemoveProfile(int index)
        {
            if(File.Exists(@"profiles\" + gameProfs[index].GameName + ".ini"))
            {
                File.Delete(@"profiles\" + gameProfs[index].GameName + ".ini");
            }
            gameProfs.RemoveAt(index);
        }

        public static string GetOption(string optionName, string scope = "PROFILE_CONFIG")
        {

            if (data[scope] != null && data[scope][optionName] != "")
            {
                return data[scope][optionName];
            }
            return "";
        }

        public static void SetOption(string optionName, string value, string scope = "PROFILE_CONFIG")
        {
            if (data[scope] == null) return;
            data[scope][optionName] = value;
        }

        public static void SaveConfig(string ProfileName)
        {
            try
            {
                parser.WriteFile("profiles/" + ProfileName + ".ini", data);
            }
            catch (Exception) { MessageBox.Show("Не могу сохранить профиль. Не хватает прав на запись."); }
        }
    }
}
