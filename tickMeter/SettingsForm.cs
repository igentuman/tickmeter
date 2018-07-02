using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tickMeter
{
    public partial class SettingsForm : Form
    {
        public const string CURRENT_VERSION = "1.5";

        public SettingsManager settings;
        public GUI gui;
        public string verInfo;
        TagCollection TagsInfo;
        public SettingsForm()
        {
            InitializeComponent();
            settings = new SettingsManager();
            
            
        }
        private class TagInfo
        {
            [JsonProperty("name")]
            public string Version { get; set; }
        }

        private class TagCollection
        {
            [JsonProperty("values")]
            public List<TagInfo> Tags { get; set; }
        }

        public async void CheckNewVersion()
        {
            await Task.Run(() =>
            {
                try
                {
                    verInfo = new WebClient().DownloadString("https://api.bitbucket.org/2.0/repositories/dvman8bit/tickmeter/refs/tags");
                    TagsInfo = JsonConvert.DeserializeObject<TagCollection>(verInfo);
                    if(CURRENT_VERSION != TagsInfo.Tags[TagsInfo.Tags.Count - 1].Version)
                    {
                        updateLbl.Text += TagsInfo.Tags[TagsInfo.Tags.Count - 1].Version;
                        updateLbl.Visible = true;
                        if(settings.GetOption("last_checked_version") != TagsInfo.Tags[TagsInfo.Tags.Count - 1].Version)
                        {
                            MessageBox.Show(updateLbl.Text, "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Process.Start("https://bitbucket.org/dvman8bit/tickmeter/downloads/");
                        }
                        settings.SetOption("last_checked_version", TagsInfo.Tags[TagsInfo.Tags.Count - 1].Version);
                    }
                }
                catch (Exception) { }
                
            });
        }

        private static String HexConverter(Color c)
        {
            return c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        public void ApplyFromConfig()
        {
            settings_chart_checkbox.Checked = settings.GetOption("chart") == "True";
            settings_ip_checkbox.Checked = settings.GetOption("ip") == "True";
            settings_ping_checkbox.Checked = settings.GetOption("ping") == "True";
            settings_netstats_checkbox.Checked = settings.GetOption("netstats") == "True";
            settings_traffic_checkbox.Checked = settings.GetOption("traffic") == "True";
            settings_rtss_output.Checked = settings.GetOption("rtss") == "True";
            rememberAdapter.Checked = settings.GetOption("remember_adapter") == "True";
            if(rememberAdapter.Checked)
            {
                adapters_list.SelectedIndex = int.Parse(settings.GetOption("last_selected_adapter_id"));
            }
            ColorLabel.ForeColor = ColorTranslator.FromHtml("#"+ settings.GetOption("color_label"));
            ColorBad.ForeColor = ColorTranslator.FromHtml("#"+ settings.GetOption("color_bad"));
            ColorMid.ForeColor = ColorTranslator.FromHtml("#"+ settings.GetOption("color_mid"));
            ColorGood.ForeColor = ColorTranslator.FromHtml("#"+ settings.GetOption("color_good"));
            RivaTuner.LabelColor = settings.GetOption("color_label");
            RivaTuner.ColorBad = settings.GetOption("color_bad");
            RivaTuner.ColorMid = settings.GetOption("color_mid");
            RivaTuner.ColorGood = settings.GetOption("color_good");
            gui.label3.ForeColor =
                gui.label4.ForeColor =
                gui.label5.ForeColor =
                gui.label9.ForeColor =
                ColorLabel.ForeColor;

            if (File.Exists(settings.GetOption("rtss_exe_path")))
            {
                RivaTuner.rtss_exe = settings.GetOption("rtss_exe_path");
            }
        }

        public void SaveToConfig()
        {
            settings.SetOption("chart", settings_chart_checkbox.Checked.ToString());
            settings.SetOption("ip", settings_ip_checkbox.Checked.ToString());
            settings.SetOption("ping", settings_ping_checkbox.Checked.ToString());
            settings.SetOption("netstats", settings_netstats_checkbox.Checked.ToString());
            settings.SetOption("traffic", settings_traffic_checkbox.Checked.ToString());
            settings.SetOption("color_label", HexConverter(ColorLabel.ForeColor));
            settings.SetOption("color_bad", HexConverter(ColorBad.ForeColor));
            settings.SetOption("color_mid", HexConverter(ColorMid.ForeColor));
            settings.SetOption("color_good", HexConverter(ColorGood.ForeColor));
            settings.SetOption("rtss", settings_rtss_output.Checked.ToString());
            settings.SetOption("remember_adapter", rememberAdapter.Checked.ToString());
            settings.SetOption("last_selected_adapter_id", adapters_list.SelectedIndex.ToString());
            settings.SaveConfig();
        }

        public void SwitchToEnglish()
        {
            Text = Resources.en.ResourceManager.GetString("settings");
            settings_rtss_output.Text = Resources.en.ResourceManager.GetString(settings_rtss_output.Name);
            settings_log_checkbox.Text = Resources.en.ResourceManager.GetString(settings_log_checkbox.Name);
            settings_ip_checkbox.Text = Resources.en.ResourceManager.GetString(settings_ip_checkbox.Name);
            settings_ping_checkbox.Text = Resources.en.ResourceManager.GetString(settings_ping_checkbox.Name);
            settings_traffic_checkbox.Text = Resources.en.ResourceManager.GetString(settings_traffic_checkbox.Name);
            settings_netstats_checkbox.Text = Resources.en.ResourceManager.GetString(settings_netstats_checkbox.Name);
            settings_chart_checkbox.Text = Resources.en.ResourceManager.GetString(settings_chart_checkbox.Name);
            possible_risks_lbl.Text = Resources.en.ResourceManager.GetString(possible_risks_lbl.Name);
            network_connection_lbl.Text = Resources.en.ResourceManager.GetString(network_connection_lbl.Name);
            ColorLabel.Text = Resources.en.ResourceManager.GetString(ColorLabel.Name);
            ColorBad.Text = Resources.en.ResourceManager.GetString(ColorBad.Name);
            ColorMid.Text = Resources.en.ResourceManager.GetString(ColorMid.Name);
            ColorGood.Text = Resources.en.ResourceManager.GetString(ColorGood.Name);
            rememberAdapter.Text = Resources.en.ResourceManager.GetString(rememberAdapter.Name);
            updateLbl.Text = Resources.en.ResourceManager.GetString(updateLbl.Name);
        }

        private void LabelsColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            ColorLabel.ForeColor = colorDialog1.Color;
            gui.label3.ForeColor =
                gui.label4.ForeColor =
                gui.label5.ForeColor =
                gui.label9.ForeColor =
                ColorLabel.ForeColor;
            SaveToConfig();
            ApplyFromConfig();
        }

        private void ColorBad_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            ColorBad.ForeColor = colorDialog1.Color;
            SaveToConfig();
            ApplyFromConfig();
        }

        private void ColorMid_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            ColorMid.ForeColor = colorDialog1.Color;
            SaveToConfig();
            ApplyFromConfig();
        }

        private void ColorGood_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            ColorGood.ForeColor = colorDialog1.Color;
            SaveToConfig();
            ApplyFromConfig();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void possible_risks_lbl_Click(object sender, EventArgs e)
        {
            Process.Start("https://bitbucket.org/dvman8bit/tickmeter/wiki/%D0%92%D0%BE%D0%B7%D0%BC%D0%BE%D0%B6%D0%BD%D1%8B%D0%B5%20%D1%80%D0%B8%D1%81%D0%BA%D0%B8%20%7C%20Possible%20risks");
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UConzx4k6IVXSs9PsY9Snkbg");
        }

        private async void settings_rtss_output_CheckedChanged(object sender, EventArgs e)
        {
            await Task.Run(() => { try { RivaTuner.PrintData(""); } catch (Exception exc) { MessageBox.Show(exc.Message); } });
            gui.UpdateStyle(settings_rtss_output.Checked);
        }

        private void settings_netstats_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (gui.NetworkConnectionsMngr == null)
                {
                    gui.NetworkConnectionsMngr = new ConnectionsManager();
                    gui.meterState.ConnMngr = gui.NetworkConnectionsMngr;
                    gui.NetworkConnectionsMngr.meterState = gui.meterState;
                    gui.PubgMngr.ConnMngr = gui.NetworkConnectionsMngr;
                }
                gui.meterState.ConnectionsManagerFlag = !settings_netstats_checkbox.Checked;

            }
            catch (Exception)
            {
                settings_netstats_checkbox.Checked = true;
                gui.meterState.ConnectionsManagerFlag = false;
                MessageBox.Show("Connections Manager internal error");
            }
        }

        private void adapters_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (adapters_list.SelectedIndex >= 0)
            {
                gui.StopTracking();
                gui.StartTracking();
            }
        }

        private void netInfo_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Отключить если вылетает PUBG или tickMeter. Отключение ухудшит качество данных.", "Help",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void updateLbl_Click(object sender, EventArgs e)
        {
            Process.Start("https://bitbucket.org/dvman8bit/tickmeter/downloads/");
        }
    }
}
