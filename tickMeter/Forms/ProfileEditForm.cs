using IniParser;
using IniParser.Model;
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

namespace tickMeter.Forms
{
    public partial class ProfileEditForm : Form
    {
        FileIniDataParser parser;
        IniData data;

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
            parser = new FileIniDataParser();
            if (!Directory.Exists("profiles"))
            {
                Directory.CreateDirectory("profiles");
            }
            string ProfileName = profile_name.Text;
            if (!File.Exists("profiles/"+ProfileName +".ini"))
            {
                File.Create("profiles/" + ProfileName + ".ini");
                File.WriteAllText("profiles/" + ProfileName + ".ini", "[PROFILE_CONFIG]" + Environment.NewLine);
                
            }
            data = parser.ReadFile("profiles/" + ProfileName + ".ini");
            SetOption("from_port_filter", from_port_filter.Text);
            SetOption("to_port_filter", to_port_filter.Text);
            SetOption("from_ip_filter", from_ip_filter.Text);
            SetOption("to_ip_filter", to_ip_filter.Text);
            SetOption("packet_size_filter", packet_size_filter.Text);
            SetOption("process_filter", process_filter.Text);
            SetOption("protocol_filter", protocol_filter.SelectedIndex.ToString());
            SetOption("require_process", require_process.Text);
            SaveConfig();
            MessageBox.Show("Профиль успешно сохранен: " + ProfileName + ".ini");

        }

        public string GetOption(string optionName, string scope = "PROFILE_CONFIG")
        {

            if (data[scope] != null && data[scope][optionName] != "")
            {
                return data[scope][optionName];
            }
            return "";
        }

        public void SetOption(string optionName, string value, string scope = "PROFILE_CONFIG")
        {
            if (data[scope] == null) return;
            data[scope][optionName] = value;

        }

        public void SaveConfig()
        {
            parser.WriteFile("profiles/" + profile_name.Text+".ini", data);
        }
    }
}
