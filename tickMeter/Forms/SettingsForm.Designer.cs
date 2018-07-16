using System.Windows.Forms;

namespace tickMeter.Forms
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.settings_netstats_checkbox = new System.Windows.Forms.CheckBox();
            this.settings_traffic_checkbox = new System.Windows.Forms.CheckBox();
            this.settings_ping_checkbox = new System.Windows.Forms.CheckBox();
            this.settings_ip_checkbox = new System.Windows.Forms.CheckBox();
            this.settings_chart_checkbox = new System.Windows.Forms.CheckBox();
            this.settings_rtss_output = new System.Windows.Forms.CheckBox();
            this.settings_log_checkbox = new System.Windows.Forms.CheckBox();
            this.network_connection_lbl = new System.Windows.Forms.Label();
            this.adapters_list = new System.Windows.Forms.ComboBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.ColorLabel = new System.Windows.Forms.Label();
            this.ColorBad = new System.Windows.Forms.Label();
            this.ColorMid = new System.Windows.Forms.Label();
            this.ColorGood = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.possible_risks_lbl = new System.Windows.Forms.Label();
            this.netInfo = new System.Windows.Forms.Label();
            this.rememberAdapter = new System.Windows.Forms.CheckBox();
            this.updateLbl = new System.Windows.Forms.Label();
            this.rtss_dialog = new System.Windows.Forms.OpenFileDialog();
            this.settings_session_time_checkbox = new System.Windows.Forms.CheckBox();
            this.game_profiles = new System.Windows.Forms.Label();
            this.settings_ticktime_chart = new System.Windows.Forms.CheckBox();
            this.ColorChart = new System.Windows.Forms.Label();
            this.settings_tickrate_show = new System.Windows.Forms.CheckBox();
            this.donate_lbl = new System.Windows.Forms.Label();
            this.settings_data_send = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // settings_netstats_checkbox
            // 
            resources.ApplyResources(this.settings_netstats_checkbox, "settings_netstats_checkbox");
            this.settings_netstats_checkbox.ForeColor = System.Drawing.Color.Black;
            this.settings_netstats_checkbox.Name = "settings_netstats_checkbox";
            this.settings_netstats_checkbox.UseVisualStyleBackColor = true;
            this.settings_netstats_checkbox.CheckedChanged += new System.EventHandler(this.settings_netstats_checkbox_CheckedChanged);
            // 
            // settings_traffic_checkbox
            // 
            resources.ApplyResources(this.settings_traffic_checkbox, "settings_traffic_checkbox");
            this.settings_traffic_checkbox.Checked = true;
            this.settings_traffic_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settings_traffic_checkbox.ForeColor = System.Drawing.Color.Black;
            this.settings_traffic_checkbox.Name = "settings_traffic_checkbox";
            this.settings_traffic_checkbox.UseVisualStyleBackColor = true;
            // 
            // settings_ping_checkbox
            // 
            resources.ApplyResources(this.settings_ping_checkbox, "settings_ping_checkbox");
            this.settings_ping_checkbox.Checked = true;
            this.settings_ping_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settings_ping_checkbox.ForeColor = System.Drawing.Color.Black;
            this.settings_ping_checkbox.Name = "settings_ping_checkbox";
            this.settings_ping_checkbox.UseVisualStyleBackColor = true;
            // 
            // settings_ip_checkbox
            // 
            resources.ApplyResources(this.settings_ip_checkbox, "settings_ip_checkbox");
            this.settings_ip_checkbox.Checked = true;
            this.settings_ip_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settings_ip_checkbox.ForeColor = System.Drawing.Color.Black;
            this.settings_ip_checkbox.Name = "settings_ip_checkbox";
            this.settings_ip_checkbox.UseVisualStyleBackColor = true;
            // 
            // settings_chart_checkbox
            // 
            resources.ApplyResources(this.settings_chart_checkbox, "settings_chart_checkbox");
            this.settings_chart_checkbox.Checked = true;
            this.settings_chart_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settings_chart_checkbox.ForeColor = System.Drawing.Color.Black;
            this.settings_chart_checkbox.Name = "settings_chart_checkbox";
            this.settings_chart_checkbox.UseVisualStyleBackColor = true;
            // 
            // settings_rtss_output
            // 
            resources.ApplyResources(this.settings_rtss_output, "settings_rtss_output");
            this.settings_rtss_output.Checked = true;
            this.settings_rtss_output.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settings_rtss_output.ForeColor = System.Drawing.Color.Black;
            this.settings_rtss_output.Name = "settings_rtss_output";
            this.settings_rtss_output.UseVisualStyleBackColor = true;
            this.settings_rtss_output.CheckedChanged += new System.EventHandler(this.settings_rtss_output_CheckedChanged);
            // 
            // settings_log_checkbox
            // 
            resources.ApplyResources(this.settings_log_checkbox, "settings_log_checkbox");
            this.settings_log_checkbox.ForeColor = System.Drawing.Color.Black;
            this.settings_log_checkbox.Name = "settings_log_checkbox";
            this.settings_log_checkbox.UseVisualStyleBackColor = true;
            // 
            // network_connection_lbl
            // 
            resources.ApplyResources(this.network_connection_lbl, "network_connection_lbl");
            this.network_connection_lbl.BackColor = System.Drawing.Color.Transparent;
            this.network_connection_lbl.ForeColor = System.Drawing.Color.Black;
            this.network_connection_lbl.Name = "network_connection_lbl";
            // 
            // adapters_list
            // 
            this.adapters_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.adapters_list, "adapters_list");
            this.adapters_list.FormattingEnabled = true;
            this.adapters_list.Name = "adapters_list";
            this.adapters_list.SelectedIndexChanged += new System.EventHandler(this.adapters_list_SelectedIndexChanged);
            // 
            // ColorLabel
            // 
            resources.ApplyResources(this.ColorLabel, "ColorLabel");
            this.ColorLabel.BackColor = System.Drawing.Color.Transparent;
            this.ColorLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ColorLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.ColorLabel.Name = "ColorLabel";
            this.ColorLabel.Click += new System.EventHandler(this.LabelsColor_Click);
            // 
            // ColorBad
            // 
            resources.ApplyResources(this.ColorBad, "ColorBad");
            this.ColorBad.BackColor = System.Drawing.Color.Transparent;
            this.ColorBad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ColorBad.ForeColor = System.Drawing.Color.DarkRed;
            this.ColorBad.Name = "ColorBad";
            this.ColorBad.Click += new System.EventHandler(this.ColorBad_Click);
            // 
            // ColorMid
            // 
            resources.ApplyResources(this.ColorMid, "ColorMid");
            this.ColorMid.BackColor = System.Drawing.Color.Transparent;
            this.ColorMid.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ColorMid.ForeColor = System.Drawing.Color.DarkOrange;
            this.ColorMid.Name = "ColorMid";
            this.ColorMid.Click += new System.EventHandler(this.ColorMid_Click);
            // 
            // ColorGood
            // 
            resources.ApplyResources(this.ColorGood, "ColorGood");
            this.ColorGood.BackColor = System.Drawing.Color.Transparent;
            this.ColorGood.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ColorGood.ForeColor = System.Drawing.Color.DarkGreen;
            this.ColorGood.Name = "ColorGood";
            this.ColorGood.Click += new System.EventHandler(this.ColorGood_Click);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Name = "label8";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // possible_risks_lbl
            // 
            resources.ApplyResources(this.possible_risks_lbl, "possible_risks_lbl");
            this.possible_risks_lbl.BackColor = System.Drawing.Color.Transparent;
            this.possible_risks_lbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.possible_risks_lbl.ForeColor = System.Drawing.Color.DarkRed;
            this.possible_risks_lbl.Name = "possible_risks_lbl";
            this.possible_risks_lbl.Click += new System.EventHandler(this.possible_risks_lbl_Click);
            // 
            // netInfo
            // 
            resources.ApplyResources(this.netInfo, "netInfo");
            this.netInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.netInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.netInfo.Name = "netInfo";
            this.netInfo.Click += new System.EventHandler(this.netInfo_Click);
            // 
            // rememberAdapter
            // 
            resources.ApplyResources(this.rememberAdapter, "rememberAdapter");
            this.rememberAdapter.ForeColor = System.Drawing.Color.Black;
            this.rememberAdapter.Name = "rememberAdapter";
            this.rememberAdapter.UseVisualStyleBackColor = true;
            // 
            // updateLbl
            // 
            resources.ApplyResources(this.updateLbl, "updateLbl");
            this.updateLbl.BackColor = System.Drawing.Color.Transparent;
            this.updateLbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.updateLbl.ForeColor = System.Drawing.Color.Blue;
            this.updateLbl.Name = "updateLbl";
            this.updateLbl.Click += new System.EventHandler(this.updateLbl_Click);
            // 
            // rtss_dialog
            // 
            this.rtss_dialog.DefaultExt = "exe";
            this.rtss_dialog.FileName = "RTSS.exe";
            resources.ApplyResources(this.rtss_dialog, "rtss_dialog");
            this.rtss_dialog.InitialDirectory = "C:\\";
            // 
            // settings_session_time_checkbox
            // 
            resources.ApplyResources(this.settings_session_time_checkbox, "settings_session_time_checkbox");
            this.settings_session_time_checkbox.Checked = true;
            this.settings_session_time_checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settings_session_time_checkbox.ForeColor = System.Drawing.Color.Black;
            this.settings_session_time_checkbox.Name = "settings_session_time_checkbox";
            this.settings_session_time_checkbox.UseVisualStyleBackColor = true;
            // 
            // game_profiles
            // 
            resources.ApplyResources(this.game_profiles, "game_profiles");
            this.game_profiles.BackColor = System.Drawing.Color.Transparent;
            this.game_profiles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.game_profiles.ForeColor = System.Drawing.Color.Blue;
            this.game_profiles.Name = "game_profiles";
            this.game_profiles.Click += new System.EventHandler(this.label1_Click);
            // 
            // settings_ticktime_chart
            // 
            resources.ApplyResources(this.settings_ticktime_chart, "settings_ticktime_chart");
            this.settings_ticktime_chart.Checked = true;
            this.settings_ticktime_chart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settings_ticktime_chart.ForeColor = System.Drawing.Color.Black;
            this.settings_ticktime_chart.Name = "settings_ticktime_chart";
            this.settings_ticktime_chart.UseVisualStyleBackColor = true;
            // 
            // ColorChart
            // 
            resources.ApplyResources(this.ColorChart, "ColorChart");
            this.ColorChart.BackColor = System.Drawing.Color.Transparent;
            this.ColorChart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ColorChart.ForeColor = System.Drawing.Color.DarkRed;
            this.ColorChart.Name = "ColorChart";
            this.ColorChart.Click += new System.EventHandler(this.ColorChart_Click);
            // 
            // settings_tickrate_show
            // 
            resources.ApplyResources(this.settings_tickrate_show, "settings_tickrate_show");
            this.settings_tickrate_show.Checked = true;
            this.settings_tickrate_show.CheckState = System.Windows.Forms.CheckState.Checked;
            this.settings_tickrate_show.ForeColor = System.Drawing.Color.Black;
            this.settings_tickrate_show.Name = "settings_tickrate_show";
            this.settings_tickrate_show.UseVisualStyleBackColor = true;
            // 
            // donate_lbl
            // 
            resources.ApplyResources(this.donate_lbl, "donate_lbl");
            this.donate_lbl.BackColor = System.Drawing.Color.Transparent;
            this.donate_lbl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.donate_lbl.ForeColor = System.Drawing.Color.Blue;
            this.donate_lbl.Name = "donate_lbl";
            this.donate_lbl.Click += new System.EventHandler(this.donate_lbl_Click);
            // 
            // settings_data_send
            // 
            resources.ApplyResources(this.settings_data_send, "settings_data_send");
            this.settings_data_send.ForeColor = System.Drawing.Color.Black;
            this.settings_data_send.Name = "settings_data_send";
            this.settings_data_send.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.settings_data_send);
            this.Controls.Add(this.donate_lbl);
            this.Controls.Add(this.settings_tickrate_show);
            this.Controls.Add(this.ColorChart);
            this.Controls.Add(this.settings_ticktime_chart);
            this.Controls.Add(this.game_profiles);
            this.Controls.Add(this.settings_session_time_checkbox);
            this.Controls.Add(this.updateLbl);
            this.Controls.Add(this.rememberAdapter);
            this.Controls.Add(this.netInfo);
            this.Controls.Add(this.ColorGood);
            this.Controls.Add(this.ColorMid);
            this.Controls.Add(this.ColorBad);
            this.Controls.Add(this.ColorLabel);
            this.Controls.Add(this.possible_risks_lbl);
            this.Controls.Add(this.settings_netstats_checkbox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.settings_traffic_checkbox);
            this.Controls.Add(this.settings_ping_checkbox);
            this.Controls.Add(this.settings_ip_checkbox);
            this.Controls.Add(this.settings_chart_checkbox);
            this.Controls.Add(this.settings_rtss_output);
            this.Controls.Add(this.settings_log_checkbox);
            this.Controls.Add(this.network_connection_lbl);
            this.Controls.Add(this.adapters_list);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.CheckBox settings_traffic_checkbox;
        public System.Windows.Forms.CheckBox settings_ping_checkbox;
        public System.Windows.Forms.CheckBox settings_ip_checkbox;
        public System.Windows.Forms.ComboBox adapters_list;
        private System.Windows.Forms.ColorDialog colorDialog1;
        public System.Windows.Forms.CheckBox settings_netstats_checkbox;
        public System.Windows.Forms.CheckBox settings_rtss_output;
        public System.Windows.Forms.CheckBox settings_chart_checkbox;
        public System.Windows.Forms.CheckBox settings_log_checkbox;
        public System.Windows.Forms.Label network_connection_lbl;
        public System.Windows.Forms.Label ColorLabel;
        public System.Windows.Forms.Label ColorBad;
        public System.Windows.Forms.Label ColorMid;
        public System.Windows.Forms.Label ColorGood;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label possible_risks_lbl;
        private System.Windows.Forms.Label netInfo;
        public System.Windows.Forms.CheckBox rememberAdapter;
        public System.Windows.Forms.Label updateLbl;
        private System.Windows.Forms.OpenFileDialog rtss_dialog;
        public CheckBox settings_session_time_checkbox;
        public Label game_profiles;
        public CheckBox settings_ticktime_chart;
        public Label ColorChart;
        public CheckBox settings_tickrate_show;
        public Label donate_lbl;
        public CheckBox settings_data_send;
    }
}